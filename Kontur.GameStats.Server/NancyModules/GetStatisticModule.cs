using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.RequestHandlers;
using Nancy;
using NLog;

namespace Kontur.GameStats.Server.NancyModules
{
  public class GetStatisticModule : NancyModule
  {
    private readonly ServerStatisticHandler serverStatisticHandler;
    private readonly PlayerStatisticHandler playerStatisticHandler;
    private readonly ReportsHandler reportsHandler;

    private readonly Logger logger = LogManager.GetCurrentClassLogger();

    public GetStatisticModule(
      ServerStatisticHandler serverStatisticHandler,
      PlayerStatisticHandler playerStatisticHandler,
      ReportsHandler reportsHandler
      )
    {
      this.serverStatisticHandler = serverStatisticHandler;
      this.playerStatisticHandler = playerStatisticHandler;
      this.reportsHandler = reportsHandler;

      Get["/servers/{endpoint:url}/stats", true] = async (x, _) => await GetServerStatisticAsync(x.endpoint);
      Get["/players/{name}/stats", true] = async (x, _) => await GetPlayerStatisticAsync(x.name);

      Get["/reports/recent-matches/", true] = async (x, _) => await GetRecentMatchesReportAsync(Constants.DefaultCount);
      Get["/reports/recent-matches/{count:int}", true] = async (x, _) =>  
      {
        var count = NormalizeCount(x.count);
        return await GetRecentMatchesReportAsync(count);
      };

      Get["/reports/best-players/", true] = async (x, _) => await GetBestPlayersReportAsync(Constants.DefaultCount);
      Get["/reports/best-players/{count:int}", true] = async (x, _) =>
      {
        var count = NormalizeCount(x.count);
        return await GetBestPlayersReportAsync(count);
      };

      Get["/reports/popular-servers/", true] = async (x, _) => await GetPopularServersReportAsync(Constants.DefaultCount);
      Get["/reports/popular-servers/{count:int}", true] = async (x, _) =>
      {
        var count = NormalizeCount(x.count);
        return await GetPopularServersReportAsync(count);
      };
    }

    private Task<Response> GetServerStatisticAsync(string endpoint)
    {
      var task = new Task<Response>(() =>
      {
        Dictionary<string, dynamic> statistic;
        try
        {
          statistic = serverStatisticHandler.GetStatistic(endpoint);
        }
        catch (Exception e)
        {
          logger.Error(e.Message);
          return HttpStatusCode.InternalServerError;
        }
        return Response.AsJson(statistic);
      });
      task.Start();
      return task;
    }

    private Task<Response> GetPlayerStatisticAsync(string playerName)
    {
      var task = new Task<Response>(() =>
      {
        Dictionary<string, dynamic> statistic;
        try
        {
          statistic = playerStatisticHandler.GetStatistic(playerName);
        }
        catch (Exception e)
        {
          logger.Error(e.Message);
          return HttpStatusCode.InternalServerError;
        }
        return Response.AsJson(statistic);
      });
      task.Start();
      return task;
    }

    private Task<Response> GetRecentMatchesReportAsync(int count)
    {
      var task = new Task<Response>(() =>
      {
        IList<MatchReportInfo> report;
        try
        {
          report = reportsHandler.GetRecentMatches(count).ToArray();
        }
        catch (Exception e)
        {
          logger.Error(e.Message);
          return HttpStatusCode.InternalServerError;
        }
        return Response.AsJson(report);
      });
      task.Start();
      return task;
    }

    private Task<Response> GetBestPlayersReportAsync(int count)
    {
      var task = new Task<Response>(() =>
      {
        IList<PlayerReportInfo> report;
        try
        {
          report = reportsHandler.GetBestPlayers(count).ToArray();
        }
        catch (Exception e)
        {
          logger.Error(e.Message);
          return HttpStatusCode.InternalServerError;
        }
        return Response.AsJson(report);
      });
      task.Start();
      return task;
    }

    private Task<Response> GetPopularServersReportAsync(int count)
    {
      var task = new Task<Response>(() =>
      {
        IList<ServerReportInfo> report;
        try
        {
          report = reportsHandler.GetPopularServers(count).ToArray();
        }
        catch (Exception e)
        {
          logger.Error(e.Message);
          return HttpStatusCode.InternalServerError;
        }
        return Response.AsJson(report);
      });
      task.Start();
      return task;
    }

    private static int NormalizeCount(int count)
    {
      if (count < 0) return 0;
      if (count > 50) return 50;
      return count;
    }
  }
}