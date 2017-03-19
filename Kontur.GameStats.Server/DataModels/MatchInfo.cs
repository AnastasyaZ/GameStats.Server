using System;
using LiteDB;

namespace Kontur.GameStats.Server.DataModels
{
  public class MatchInfo:IEquatable<MatchInfo>
  {
    [BsonIndex]
    public string endpoint { get; set; }
    [BsonIndex]
    public DateTime timestamp { get; set; }
    public MatchResult result { get; set; }

    public int Population => result.scoreboard.Length;

    #region equality members

    public bool Equals(MatchInfo other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(endpoint, other.endpoint)
        && timestamp.Equals(other.timestamp)
        && Equals(result, other.result);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((MatchInfo) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = (endpoint != null ? endpoint.GetHashCode() : 0);
        hashCode = (hashCode*397) ^ timestamp.GetHashCode();
        hashCode = (hashCode*397) ^ (result != null ? result.GetHashCode() : 0);
        return hashCode;
      }
    }

    #endregion
  }
}