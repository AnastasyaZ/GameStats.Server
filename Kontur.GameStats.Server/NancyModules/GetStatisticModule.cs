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
      Get["/reports/recent-matches/{count:int}", true] = async (x, _) => await GetRecentMatchesReportAsync(x.count);//TODO значение по умолчанию перенести
      Get["/reports/best-players/{count:int?5}"] = _ => HttpStatusCode.NotImplemented;
      Get["/reports/popular-servers/{count:int?5}"] = _ => HttpStatusCode.NotImplemented;
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
  }
}