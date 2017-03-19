using System;
using System.Collections.Generic;
using System.Linq;

namespace Kontur.GameStats.Server.Utility
{
  public static class LinqExtensions
  {
    public static bool CompareWithoutOrder<T>(this IList<T> that, IList<T> other)
      where T : IEquatable<T>
    {
      if (that.Count != other.Count) return false;
      return that.OrderBy(x => x)
        .Zip(other.OrderBy(x => x), (a, b) => a.Equals(b))
        .All(x => x);
    }

    public static IEnumerable<T> GetMostPopular<T>(this IEnumerable<T> source, int count)
      where T : IComparable
    {
      return source
        .GroupBy(x => x)
        .OrderByDescending(x => x.Count())
        .Take(count)
        .Select(x => x.Key);
    }

    public static IEnumerable<T> SkipNulls<T>(this IEnumerable<T> source)
    {
      return source.Where(x => x != null);
    }

    public static int UniqueCount<TIn, TOut>(this IEnumerable<TIn> matches, Func<TIn,TOut> selector)
    {
      return matches.Select(selector).Distinct().Count();
    }
  }
}