using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.DataModels.Utility;

namespace Kontur.GameStats.Server.RequestHandlers
{
  public class ReportsHandler
  {
    private readonly IDatabaseAdapter database;

    public ReportsHandler(IDatabaseAdapter database)
    {
      this.database = database;
    }

    public IEnumerable<MatchInfo> GetRecentMatches(int count)
    {
      if (count < 0 || count > Constants.MaxCount) throw new ArgumentOutOfRangeException(nameof(count));

      return database.GetRecentMatches(count);
    }

    public IEnumerable<PlayerReportInfo> GetBestPlayers(int count)
    {
      if (count < 0 || count > Constants.MaxCount) throw new ArgumentOutOfRangeException(nameof(count));

      return database.GetMatches()
        .SelectMany(x => x.result.scoreboard)
        .GroupBy(x => x.name.ToLower())
        .Where(group => group.Count() >= 10) //TODO const
        .Select(group =>
        {
          var totalKills = group.Sum(x => x.kills);
          var totalDeaths = group.Sum(x => x.deaths);
          if (totalDeaths == 0) return null;
          return new PlayerReportInfo
          {
            name = group.Key,
            killToDeathRatio = (double) totalKills/totalDeaths
          };
        })
        .SkipNulls()
        .Take(count);
    }

    public IEnumerable<ServerReportInfo> GetPopularServers(int count)
    {
      if (count < 0 || count > Constants.MaxCount) throw new ArgumentOutOfRangeException(nameof(count));

      var servers = database.GetServers();
      var lastMatch = database.GetRecentMatches(1).FirstOrDefault();
      var last = lastMatch?.timestamp ?? DateTime.MinValue;
      return database.GetMatches()
        .GroupBy(x => x.endpoint)
        .Select(g =>
        {
          var endpoint = g.Key;

          var first = g.Min(x => x.timestamp);
          var totalDays = (last - first).Days + 1;
          var averageMatchParDay = (double)g.Count()/totalDays;

          var name = servers.First(x => x.endpoint.Equals(endpoint)).gameServer.name;

          return new ServerReportInfo
          {
            endpoint = endpoint,
            name = name,
            averageMatchesPerDay = averageMatchParDay
          };
        })
        .OrderByDescending(x=>x.averageMatchesPerDay)
        .Take(count);
    }
  }
}