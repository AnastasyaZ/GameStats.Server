using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.DataModels;

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
      var matches = database.GetMatches(endpoint).ToArray();
      var lastMatchData = database.GetLastMatchDateTime();

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
      return matches.GetTotalMatchesPlayed();
    }

    private static int GetMaximumMatchesPerDay(IList<MatchInfo> matches)
    {
      return matches.GetMaximumMatchesPerDay();
    }

    private static double GetAverageMatchesPerDay(IList<MatchInfo> matches, DateTime lastMatch)
    {
      return matches.GetAverageMatchesPerDay(lastMatch);
    }

    private static int GetMaximumPopulation(IList<MatchInfo> matches)
    {
      return matches.Max(x => x.Population);
    }

    private static double GetAveragePopulation(IList<MatchInfo> matches)
    {
      return matches.Average(x=>x.Population);
    }

    private static IList<string> GetTop5GameModes(IList<MatchInfo> matches)
    {
      return matches.GetTop5(x => x.result.gameMode);
    }

    private static IList<string> GetTop5Maps(IList<MatchInfo> matches)
    {
      return matches.GetTop5(x => x.result.map);
    }
  }
}