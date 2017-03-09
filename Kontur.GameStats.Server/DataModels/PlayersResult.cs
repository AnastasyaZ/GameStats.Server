using System;

namespace Kontur.GameStats.Server.DataModels
{
  public class PlayersResult : IEquatable<PlayersResult>
  {
    public string name { get; set; }
    public int frags { get; set; }
    public int kills { get; set; }
    public int deaths { get; set; }

    #region equality members

    public bool Equals(PlayersResult other)
    {
      return string.Equals(name, other.name) && frags == other.frags && kills == other.kills && deaths == other.deaths;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((PlayersResult)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = (name != null ? name.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ frags;
        hashCode = (hashCode * 397) ^ kills;
        hashCode = (hashCode * 397) ^ deaths;
        return hashCode;
      }
    }
    #endregion
  }
}