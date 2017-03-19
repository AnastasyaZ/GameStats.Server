using System;
using System.Threading.Tasks;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.RequestHandlers;
using Nancy;
using NLog;

namespace Kontur.GameStats.Server.NancyModules
{
  public class GetDataModule : NancyModule
  {
    private readonly GetDataHandler handler;
    private readonly Logger logger = LogManager.GetCurrentClassLogger();

    public GetDataModule(GetDataHandler handler)
    {
      this.handler = handler;

      Get["/servers/{endpoint:url}/Info", true] = async (x, _)
        => await GetServerAsync(x.endpoint);

      Get["/servers/{endpoint:url}/matches/{timestamp:utc_timestamp}", true] = async (x, _)
        => await GetMatchAsync(x.endpoint, x.timestamp);

      Get["/servers/Info", true] = async (x, _)
        => await GetServersAsync();
    }

    private Task<Response> GetServerAsync(string endpoint)
    {
      var task = new Task<Response>(() =>
      {
        ServerInfo server;
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

    private Task<Response> GetMatchAsync(string endpoint, DateTime timestamp)
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

    private Task<Response> GetServersAsync()
    {
      var task = new Task<Response>(() =>
      {
        GameServer[] servers;
        try
        {
          servers = handler.GetGameServers();
        }
        catch (Exception e)
        {
          logger.Error(e.Message);
          return HttpStatusCode.InternalServerError;
        }
        return Response.AsJson(servers);
      });
      task.Start();
      return task;
    }
  }
}