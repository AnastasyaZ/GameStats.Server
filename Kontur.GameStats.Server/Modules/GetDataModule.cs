using System;
using System.Threading.Tasks;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.DataModels.Utility;
using Kontur.GameStats.Server.RequestHandlers;
using Nancy;
using NLog;

namespace Kontur.GameStats.Server.Modules
{
  public class GetDataModule : NancyModule
  {
    private readonly GetDataHandler handler;
    private readonly Logger logger = LogManager.GetCurrentClassLogger();

    public GetDataModule(GetDataHandler handler)
    {
      this.handler = handler;

      Get["/servers/{endpoint}/Info", true] = async (x, _) =>
      {
        return await GetServerInThread(x.endpoint);
      };

      Get["/servers/{endpoint}/matches/{timestamp}", true] = async (x, _) =>
      {
        string timestamp = x.timestamp;
        return await GetMatchInThread(x.endpoint, timestamp.ParseInUts());
      };

      Get["/servers/Info", true] = async (x, _) =>
      {
        return await GetServersInThread();
      };
    }

    private Task<Response> GetServerInThread(string endpoint)
    {
      var task = new Task<Response>(() =>
      {
        GameServer server;
        try
        {
          server = handler.GetGameServer(endpoint);
        }
        catch (Exception e)
        {
          logger.Error(e.Message);
          return HttpStatusCode.InternalServerError;
        }

        return server != null
        ? Response.AsJson(server)
        : HttpStatusCode.NotFound;

      });
      task.Start();
      return task;
    }

    private Task<Response> GetMatchInThread(string endpoint, DateTime timestamp)
    {
      var task = new Task<Response>(() =>
      {
        MatchResult match;
        try
        {
          match = handler.GetMatchResult(endpoint, timestamp);
        }
        catch (Exception e)
        {
          logger.Error(e.Message);
          return HttpStatusCode.InternalServerError;
        }

        return match != null
          ? Response.AsJson(match)
          : HttpStatusCode.NotFound;
      });
      task.Start();
      return task;
    }

    private Task<Response> GetServersInThread()
    {
      throw new NotImplementedException();
      //return Response.AsJson(new[] { new GameServerInfo() });
    }
  }
}