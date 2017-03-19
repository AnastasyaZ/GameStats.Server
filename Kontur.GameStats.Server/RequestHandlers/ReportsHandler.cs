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

    public IEnumerable<MatchReportInfo> GetRecentMatches(int count)
    {
      if (count < 0 || count > Constants.MaxCount) throw new ArgumentOutOfRangeException(nameof(count));

      return database.GetRecentMatches(count).Select(x => x.ToReportInfo());
    }

    public IEnumerable<PlayerReportInfo> GetBestPlayers(int count)
    {
      if (count < 0 || count > Constants.MaxCount) throw new ArgumentOutOfRangeException(nameof(count));

      return database.GetMatches()
        .SelectMany(x => x.result.scoreboard)
        .GroupBy(x => x.name.ToLower())
        .Where(x => x.Count() >= Constants.MinMatchPlayed)
        .Select(x =>
        {
          var killToDeathRatio = x.GetKillsToDeathRatio();
          return killToDeathRatio == null
            ? null
            : new PlayerReportInfo
            {
              name = x.Key,
              killToDeathRatio = killToDeathRatio.Value
            };
        })
        .SkipNulls()
        .Take(count);
    }

    public IEnumerable<ServerReportInfo> GetPopularServers(int count)
    {
      if (count < 0 || count > Constants.MaxCount) throw new ArgumentOutOfRangeException(nameof(count));

      var servers = database.GetServers();
      var last = database.GetLastMatchDateTime();
      return database.GetMatches()
        .GroupBy(x => x.endpoint)
        .Select(g =>
        {
          var endpoint = g.Key;

          var first = g.Min(x => x.timestamp);
          var totalDays = (last - first).Days + 1;
          var averageMatchParDay = (double)g.Count() / totalDays;

          var name = servers.First(x => x.endpoint.Equals(endpoint)).info.name;

          return new ServerReportInfo
          {
            endpoint = endpoint,
            name = name,
            averageMatchesPerDay = averageMatchParDay
          };
        })
        .OrderByDescending(x => x.averageMatchesPerDay)
        .Take(count);
    }
  }
}