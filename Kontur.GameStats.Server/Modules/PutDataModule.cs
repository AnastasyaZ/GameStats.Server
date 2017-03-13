using System;
using System.Threading.Tasks;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.DataModels.Utility;
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

    public PutDataModule(PutDataHandler handler)
    {
      this.handler = handler;

      Put["/servers/{endpoint}/info", true] = async (x, _) =>
      {
        var gameServer = this.Bind<GameServer>();
        var server = new GameServerInfo
        {
          endpoint = x.endpoint,
          gameServer = gameServer
        };
        var validationResult = this.Validate(server);
        if (!validationResult.IsValid)
        {
          logger.Error($"Cannot bind to GameServerInfo");
          return HttpStatusCode.BadRequest;
        }

        return await AddServerInThread(server);
      };

      Put["/servers/{endpoint}/matches/{timestamp}", true] = async (x, _) =>
       {
         var matchResult = this.Bind<MatchResult>();
         string timestamp = x.timestamp;
         var matchInfo = new MatchInfo
         {
           endpoint = x.endpoint,
           timestamp = timestamp.ParseInUts(),
           result = matchResult
         };
         var validationResult = this.Validate(matchInfo);
         if (!validationResult.IsValid)
         {
           return HttpStatusCode.BadRequest;
         }

         return await AddMatchInfoInThread(matchInfo);
       };
    }

    private Task<HttpStatusCode> AddServerInThread(GameServerInfo server)
    {
      var task = new Task<HttpStatusCode>(() =>
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
        });
      task.Start();
      return task;
    }

    private Task<HttpStatusCode> AddMatchInfoInThread(MatchInfo match)
    {
      var task = new Task<HttpStatusCode>(() => 
      {
        try
        {
          var success = handler.TryPutMatch(match);
          return success ? HttpStatusCode.OK : HttpStatusCode.NotAcceptable;
        }
        catch (Exception e)
        {
          logger.Error(e.Message);
          return HttpStatusCode.InternalServerError;
        }
      });
      task.Start();
      return task;
    }
  }
}