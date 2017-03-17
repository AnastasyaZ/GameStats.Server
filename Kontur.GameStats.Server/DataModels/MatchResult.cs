using System;
using Kontur.GameStats.Server.DataModels.Utility;

namespace Kontur.GameStats.Server.DataModels
{
  public class MatchResult:IEquatable<MatchResult>
  {
    public string map { get; set; }
    public string gameMode { get; set; }
    public int fragLimit { get; set; }
    public int timeLimit { get; set; }
    public double timeElapsed { get; set; }
    public PlayerResult[] scoreboard { get; set; }

    public PlayerResult winner => scoreboard[0];

    #region equality members

    public bool Equals(MatchResult other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(map, other.map) 
        && string.Equals(gameMode, other.gameMode)
        && fragLimit == other.fragLimit 
        && timeLimit == other.timeLimit
        && timeElapsed.Equals(other.timeElapsed)
        && scoreboard.CompareWithoutOrder(other.scoreboard);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((MatchResult) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = (map != null ? map.GetHashCode() : 0);
        hashCode = (hashCode*397) ^ (gameMode != null ? gameMode.GetHashCode() : 0);
        hashCode = (hashCode*397) ^ fragLimit;
        hashCode = (hashCode*397) ^ timeLimit;
        hashCode = (hashCode*397) ^ timeElapsed.GetHashCode();
        hashCode = (hashCode*397) ^ (scoreboard != null ? scoreboard.GetHashCode() : 0);
        return hashCode;
      }
    }

    #endregion
  }
}