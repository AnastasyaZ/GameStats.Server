using System;
using System.Threading.Tasks;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.RequestHandlers;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using NLog;

namespace Kontur.GameStats.Server.Modules
{
  public class PutDataModule : NancyModule
  {
    private readonly PutDataHandler handler;
    private readonly Logger logger = LogManager.GetCurrentClassLogger();

    public PutDataModule(IDatabaseAdapter database)
    {
      

      handler = new PutDataHandler(database);

      Put["/servers/{endpoint}/info", true] = async (x, ct) =>
      {
        var gameServer = this.Bind<GameServer>();
        var server = new GameServerInfo
        {
          endpoint = x.endpoint,
          gameServer = gameServer
        };
        var validationResult = this.Validate(server);
        if (!validationResult.IsValid)
          return HttpStatusCode.BadRequest;

        return await AddServerInThread(server);
      };

      Put["/servers/{endpoint}/matches/{timestamp}"] = _ =>
      {
        throw new NotImplementedException();
      };
    }

    private TResult Parse<TResult>()
      where TResult : class
    {
      TResult result;
      try
      {
        result = this.Bind<TResult>();
      }
      catch (Exception e)
      {
        logger.Error($"Cannot bind to {typeof(TResult)}. {e.Message}");
        return null;
      }
      return result;
    }

    private Task<HttpStatusCode> AddServerInThread(GameServerInfo server)
    {
      var task = new Task<HttpStatusCode>(s =>
        {
          try
          {
            handler.PutServer(server);
          }
          catch (Exception e)
          {
            logger.Error(e.Message);
            return HttpStatusCode.InternalServerError;
          }
          return HttpStatusCode.OK;
        }, server);
      task.Start();
      return task;
    }
  }
}