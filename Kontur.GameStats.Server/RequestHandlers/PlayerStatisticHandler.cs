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

    public Dictionary<string, dynamic> GetStatistic(string playerName)
    {
      var matches = database.GetMatchesWithPlayer(playerName);
      var lastMatchData = database.GetLastMatchDateTime();

      var statistic = new Dictionary<string, dynamic>
      {
        {"totalMatchesPlayed", GetTotalMatchesPlayed(matches) },
        {"totalMatchesWon", GetTotalMatchesWon(matches, playerName) },
        {"favoriteServer", GetFavoriteServer(matches) },
        {"uniqueServers", GetUniqueServersCount(matches) },
        {"favoriteGameMode", GetFavoriteGameMode(matches) },
        {"averageScoreboardPercent", GetAverageScoreboardPercent(matches,playerName) },
        {"maximumMatchesPerDay", GetMaximumMatchesPerDay(matches) },
        {"averageMatchesPerDay", GetAverageMatchesPerDay(matches, lastMatchData) },
        {"lastMatchPlayed", GetLastMatchDateTime(matches) },
        {"killToDeathRatio", GetKillToDeathRatio(matches, playerName) }
      };

      return statistic;
    }

    private static int GetTotalMatchesPlayed(IList<MatchInfo> matches)
    {
      return matches.GetTotalMatchesPlayed();
    }

    private static int GetTotalMatchesWon(IList<MatchInfo> matches, string playerName)
    {
      Func<MatchInfo, PlayerInfo> winner = match => match.result.scoreboard[0];
      return matches.Count(match =>winner(match).EqualByNameIgnoreCase(playerName));
    }

    private static string GetFavoriteServer(IList<MatchInfo> matches)
    {
      return matches.GetFavorite(x => x.endpoint);
    }

    private static int GetUniqueServersCount(IList<MatchInfo> matches)
    {
      return matches.UniqueCount(x => x.endpoint);
    }

    private static string GetFavoriteGameMode(IList<MatchInfo> matches)
    {
      return matches.GetFavorite(x => x.result.gameMode);
    }

    private static double GetAverageScoreboardPercent(IList<MatchInfo> matches, string playerName)
    {
      return matches.Average(x =>
      {
        var scoreboard = x.result.scoreboard.ToList();
        var total = scoreboard.Count;
        if (total < 2) return 0;

        var above = scoreboard.FindIndex(p => p.EqualByNameIgnoreCase(playerName));
        var below = total - above - 1;
        return (double)below / (total - 1) * 100;
      });
    }

    private static int GetMaximumMatchesPerDay(IList<MatchInfo> matches)
    {
      return matches.GetMaximumMatchesPerDay();
    }

    private static double GetAverageMatchesPerDay(IList<MatchInfo> matches, DateTime lastMatch)
    {
      return matches.GetAverageMatchesPerDay(lastMatch);
    }

    private static DateTime GetLastMatchDateTime(IList<MatchInfo> matches)
    {
      return matches.GetLastMatchDateTime();
    }

    private static double GetKillToDeathRatio(IList<MatchInfo> matches, string playerName)
    {
      var results = matches
        .SelectMany(x => x.result.scoreboard)
        .Where(x => x.EqualByNameIgnoreCase(playerName));

      return results.GetKillsToDeathRatio() ?? 0;
    }
  }
}