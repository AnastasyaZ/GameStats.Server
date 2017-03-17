using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.DataModels.Utility;

namespace Kontur.GameStats.Server.RequestHandlers
{
  public class ServerStatisticHandler
  {
    private readonly IDatabaseAdapter database;

    public ServerStatisticHandler(IDatabaseAdapter database)
    {
      this.database = database;
    }

    public Dictionary<string, dynamic> GetStatistic(string endpoint)
    {
      var matches = database.GetMatches(endpoint).ToList();

      var statistic = new Dictionary<string, dynamic>
      {
        {"totalMatchesPlayed", GetTotalMatchesPlayed(matches)},
        {"maximumMatchesPerDay", GetMaximumMatchesPerDay(matches)},
        {"averageMatchesPerDay", GetAverageMatchesPerDay(matches)},
        {"maximumPopulation", GetMaximumPopulation(matches)},
        {"averagePopulation", GetAveragePopulation(matches)},
        {"top5GameModes", GetTop5GameModes(matches)},
        {"top5Maps", GetTop5Maps(matches)}
      };

      return statistic;
    }

    private int GetTotalMatchesPlayed(IList<MatchInfo> matches)
    {
      return matches.Count;
    }

    private int GetMaximumMatchesPerDay(IList<MatchInfo> matches)
    {
      return matches.GroupBy(x => x.timestamp.Date).Max(x => x.Count());
    }

    public double GetAverageMatchesPerDay(IList<MatchInfo> matches)
    {
      //var first = matches.Min(x => x.timestamp);
      //var last = matches.Max(x => x.timestamp);//TODO последний матч из всех серверов
      //var total = matches.Count;
      //return (double)total/(last.Day-first.Day+1);
      return 0;
    }

    public int GetMaximumPopulation(IList<MatchInfo> matches)
    {
      return matches.Max(x => x.result.scoreboard.Length);
    }

    public double GetAveragePopulation(IList<MatchInfo> matches)
    {
      return (double)matches.Sum(x => x.result.scoreboard.Length) / matches.Count;
    }

    public IList<string> GetTop5GameModes(IList<MatchInfo> matches)
    {
      return matches.Select(x => x.result.gameMode).GetMostPopular(5).ToArray();
    }

    public IList<string> GetTop5Maps(IList<MatchInfo> matches)
    {
      return matches.Select(x => x.result.map).GetMostPopular(5).ToArray();
    }
  }
}