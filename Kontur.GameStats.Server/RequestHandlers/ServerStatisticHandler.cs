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
      var lastMatchData = database.GetMatches().Max(x => x.timestamp);//todo

      var statistic = new Dictionary<string, dynamic>
      {
        {"totalMatchesPlayed", GetTotalMatchesPlayed(matches)},
        {"maximumMatchesPerDay", GetMaximumMatchesPerDay(matches)},
        {"averageMatchesPerDay", GetAverageMatchesPerDay(matches, lastMatchData)},
        {"maximumPopulation", GetMaximumPopulation(matches)},
        {"averagePopulation", GetAveragePopulation(matches)},
        {"top5GameModes", GetTop5GameModes(matches)},
        {"top5Maps", GetTop5Maps(matches)}
      };

      return statistic;
    }

    private static int GetTotalMatchesPlayed(IList<MatchInfo> matches)
    {
      return matches.Count;
    }

    private static int GetMaximumMatchesPerDay(IList<MatchInfo> matches)
    {
      return matches.GroupBy(x => x.timestamp.Date).Max(x => x.Count());
    }

    public static double GetAverageMatchesPerDay(IList<MatchInfo> matches, DateTime lastMatch)
    {
      var first = matches.Min(x => x.timestamp);
      var last = lastMatch;
      var total = matches.Count;
      return (double) total/((last - first).Days + 1);
    }

    public static int GetMaximumPopulation(IList<MatchInfo> matches)
    {
      return matches.Max(x => x.result.scoreboard.Length);
    }

    public static double GetAveragePopulation(IList<MatchInfo> matches)
    {
      return (double) matches.Sum(x => x.result.scoreboard.Length)/matches.Count;
    }

    public static IList<string> GetTop5GameModes(IList<MatchInfo> matches)
    {
      return matches.Select(x => x.result.gameMode).GetMostPopular(5).ToArray();
    }

    public static IList<string> GetTop5Maps(IList<MatchInfo> matches)
    {
      return matches.Select(x => x.result.map).GetMostPopular(5).ToArray();
    }
  }
}