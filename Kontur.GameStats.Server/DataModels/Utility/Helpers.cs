using System;
using System.Collections.Generic;
using System.Linq;

namespace Kontur.GameStats.Server.DataModels.Utility
{
  public class Helpers
  {
    public static bool CompareLists<T>(IList<T> that, IList<T> other)
      where T:IEquatable<T>
    {
      if (that.Count != other.Count) return false;
      return that.OrderBy(x => x)
        .Zip(other.OrderBy(x => x), (a, b) => a.Equals(b))
        .All(x => x);
    }
  }
}