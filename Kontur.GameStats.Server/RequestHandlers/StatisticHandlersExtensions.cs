using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.Utility;

namespace Kontur.GameStats.Server.RequestHandlers
{
  public static class StatisticHandlersExtensions
  {
    public static double GetAverageMatchesPerDay(this IList<MatchInfo> matches, DateTime lastMatch)
    {
      var first = matches.Min(x => x.timestamp);
      var last = lastMatch;
      var total = matches.Count;
      return (double)total / ((last - first).Days + 1);
    }

    public static DateTime GetLastMatchDateTime(this IList<MatchInfo> matches)
    {
      return matches.Select(x => x.timestamp).Max();
    }

    public static int GetMaximumMatchesPerDay(this IList<MatchInfo> matches)
    {
      return matches.GroupBy(x => x.timestamp.Date).Max(x => x.Count());
    }

    public static int GetTotalMatchesPlayed(this IList<MatchInfo> matches)
    {
      return matches.Count;
    }

    public static IList<T> GetTop5<T>(this IList<MatchInfo> matches, Func<MatchInfo, T> selector)
      where T : IComparable
    {
      return matches
        .Select(selector)
        .GetMostPopular(5)
        .ToArray();
    }

    public static T GetFavorite<T>(this IList<MatchInfo> matches, Func<MatchInfo, T> selector)
      where T : IComparable
    {
      return matches.Select(selector).GetMostPopular(1).FirstOrDefault();
    }

    public static double? GetKillsToDeathRatio(this IEnumerable<PlayerInfo> playerResults)
    {
      var results = playerResults.ToArray();
      var totalKills = results.Sum(x => x.kills);
      var totalDeaths = results.Sum(x => x.deaths);

      if (totalDeaths == 0) return null;
      return (double)totalKills / totalDeaths;
    }
  }
}