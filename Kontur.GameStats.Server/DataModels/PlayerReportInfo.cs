using System;

namespace Kontur.GameStats.Server.DataModels
{
  public class PlayerReportInfo : IEquatable<PlayerReportInfo>
  {
    public string name { get; set; }
    public double killToDeathRatio { get; set; }

    #region equalityMembers

    public bool Equals(PlayerReportInfo other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(name, other.name, StringComparison.InvariantCultureIgnoreCase)
        && killToDeathRatio.Equals(other.killToDeathRatio);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((PlayerReportInfo)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((name?.GetHashCode() ?? 0) * 397) ^ killToDeathRatio.GetHashCode();
      }
    }
    #endregion
  }
}