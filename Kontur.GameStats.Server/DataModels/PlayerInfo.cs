using System;

namespace Kontur.GameStats.Server.DataModels
{
  public class PlayerInfo : IEquatable<PlayerInfo>
  {
    public string name { get; set; }
    public int frags { get; set; }
    public int kills { get; set; }
    public int deaths { get; set; }

    #region equality members

    public bool Equals(PlayerInfo other)
    {
      return EqualByNameIgnoreCase(other)
        && frags == other.frags
        && kills == other.kills
        && deaths == other.deaths;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((PlayerInfo)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = name?.GetHashCode() ?? 0;
        hashCode = (hashCode * 397) ^ frags;
        hashCode = (hashCode * 397) ^ kills;
        hashCode = (hashCode * 397) ^ deaths;
        return hashCode;
      }
    }

    public bool EqualByNameIgnoreCase(string otherName)
    {
      return name.Equals(otherName, StringComparison.InvariantCultureIgnoreCase);
    }

    public bool EqualByNameIgnoreCase(PlayerInfo other)
    {
      return EqualByNameIgnoreCase(other.name);
    }

    #endregion
  }
}