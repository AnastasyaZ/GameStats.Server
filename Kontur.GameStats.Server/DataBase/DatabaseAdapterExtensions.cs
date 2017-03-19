using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.Database
{
  public static class DatabaseAdapterExtensions
  {
    public static DateTime GetLastMatchDateTime(this IDatabaseAdapter database)
    {
      var lastMatch = database.GetRecentMatches(1).FirstOrDefault();
      return lastMatch?.timestamp ?? DateTime.MinValue;
    }

    public static IList<MatchInfo> GetRecentMatches(this IDatabaseAdapter database, int count)
    {
      return database.GetMatches()
        .OrderByDescending(x => x.timestamp)
        .Take(count)
        .ToArray();
    }

    public static IList<MatchInfo> GetMatchesWithPlayer(this IDatabaseAdapter database, string name)
    {
      return database.GetMatches()
        .Where(x => x.result.scoreboard.Any(player =>
          player.EqualByNameIgnoreCase(name)))
        .ToArray();
    }
  }
}