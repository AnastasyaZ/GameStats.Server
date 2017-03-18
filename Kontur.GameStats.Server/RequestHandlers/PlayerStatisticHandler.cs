using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.DataModels.Utility;

namespace Kontur.GameStats.Server.RequestHandlers
{
  public class PlayerStatisticHandler
  {
    private readonly IDatabaseAdapter database;

    public PlayerStatisticHandler(IDatabaseAdapter database)
    {
      this.database = database;
    }

    public Dictionary<string, dynamic> GetStatistic(string name)
    {
      var matches = database.GetMatches()
        .Where(x => x.result.scoreboard.Any(p => p.name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
        .ToArray();

      var lastMatchData = database.GetMatches().Max(x => x.timestamp);//TODO DRY

      var statistic=new Dictionary<string, dynamic>
      {
        {"totalMatchesPlayed", GetTotalMatchesPlayed(matches) },
        {"totalMatchesWon", GetTotalMatchesWon(matches, name) },
        {"favoriteServer", GetFavoriteServer(matches) },
        {"uniqueServers", GetUniqueServersCount(matches) },
        {"favoriteGameMode", GetFavoriteGameMode(matches) },
        {"averageScoreboardPercent", GetAverageScoreboardPercent(matches,name) },
        {"maximumMatchesPerDay", GetMaximumMatchesPerDay(matches) },
        {"averageMatchesPerDay", GetAverageMatchesPerDay(matches, lastMatchData) },
        {"lastMatchPlayed", GetLastMatchDateTime(matches) },
        {"killToDeathRatio", GetKillToDeathRatio(matches, name) }
      };

      return statistic;
    }

    private static int GetTotalMatchesPlayed(IList<MatchInfo> matches)
    {
      return matches.Count;
    }

    private static int GetTotalMatchesWon(IList<MatchInfo> matches, string playerName)
    {
      return matches.Count(match => 
        match.result.winner.name.Equals(playerName, StringComparison.InvariantCultureIgnoreCase));
    }

    private static string GetFavoriteServer(IList<MatchInfo> matches)
    {
      return matches.Select(x => x.endpoint).GetMostPopular(1).FirstOrDefault();
    }

    private static int GetUniqueServersCount(IList<MatchInfo> matches)
    {
      return matches.Select(x => x.endpoint).Distinct().Count();
    }

    private static string GetFavoriteGameMode(IList<MatchInfo> matches)
    {
      return matches.Select(x => x.result.gameMode).GetMostPopular(1).FirstOrDefault();
    }

    private static double GetAverageScoreboardPercent(IList<MatchInfo> matches, string playerName)
    {
      return matches.Average(x =>
      {
        var sc = x.result.scoreboard.ToList(); //TODO
        var total = sc.Count;
        var below = total-1 - sc.FindIndex(p => p.name.Equals(playerName, StringComparison.InvariantCultureIgnoreCase));
        if (total < 2) return 0;
        return (double)below / (total - 1) * 100;
      });
    }

    private static int GetMaximumMatchesPerDay(IList<MatchInfo> matches)
    {
      return matches.GroupBy(x => x.timestamp.Date).Max(x => x.Count());//TODO DRY
    }

    private static double GetAverageMatchesPerDay(IList<MatchInfo> matches, DateTime lastMatch)
    {
      var first = matches.Min(x => x.timestamp);
      var last = lastMatch;
      var total = matches.Count;
      return (double)total / ((last - first).Days + 1);//TODO DRY
    }

    private static DateTime GetLastMatchDateTime(IList<MatchInfo> matches)
    {
      return matches.Select(x => x.timestamp).Max();
    }

    private static double GetKillToDeathRatio(IList<MatchInfo> matches, string playerName)
    {
      var results = matches
        .SelectMany(x => x.result.scoreboard)
        .Where(x => x.name.Equals(playerName, StringComparison.InvariantCultureIgnoreCase))
        .ToArray();

      var totalKills = results.Sum(x => x.kills);
      var totalDeaths = results.Sum(x => x.deaths);

      return (double)totalKills/totalDeaths;
    }
  }
}