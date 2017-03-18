using System;

namespace Kontur.GameStats.Server.DataModels
{
  public class ServerReportInfo:IEquatable<ServerReportInfo>
  {
    public string endpoint { get; set; }
    public string name { get; set; }
    public double averageMatchesPerDay { get; set; }

    public bool Equals(ServerReportInfo other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(endpoint, other.endpoint)
        && string.Equals(name, other.name)
        && Math.Abs(averageMatchesPerDay-other.averageMatchesPerDay)<0.00001;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((ServerReportInfo) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = endpoint?.GetHashCode() ?? 0;
        hashCode = (hashCode*397) ^ (name?.GetHashCode() ?? 0);
        hashCode = (hashCode*397) ^ averageMatchesPerDay.GetHashCode();
        return hashCode;
      }
    }
  }
}