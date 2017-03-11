using System;
using System.Threading.Tasks;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.RequestHandlers;
using Nancy;
using Nancy.ModelBinding;

namespace Kontur.GameStats.Server.Modules
{
  public class PutDataModule : NancyModule
  {
    private readonly PutDataHandler handler;

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

        try
        {
          await AddServerInThread(server);
        }
        catch (Exception e)
        {
          //TODO Log
          return HttpStatusCode.InternalServerError;
        }
        return HttpStatusCode.OK;
      };

      Put["/servers/{endpoint}/matches/{timestamp}"] = _ =>
      {
        throw new NotImplementedException();
      };
    }

    private Task AddServerInThread(GameServerInfo server)
    {
      var task = new Task(s =>
        {
          handler.PutServer(server);
          throw new Exception();
        }, server);
      task.Start();
      return task;
    }
  }
}