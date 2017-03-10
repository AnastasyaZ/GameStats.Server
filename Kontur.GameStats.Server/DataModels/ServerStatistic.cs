using System;

namespace Kontur.GameStats.Server.DataModels
{
  public class ServerStatistic:IEquatable<ServerStatistic>
  {
    public int totalMatchesPlayed { get; set; }
    public int maximumMatchesPerDay { get; set; }
    public double averageMatchesPerDay { get; set; }
    public int maximumPopulation { get; set; }
    public double averagePopulation { get; set; }
    public string[] Top5GameModels { get; set; }
    public string[] top5Maps { get; set; }

    #region equality members

    public bool Equals(ServerStatistic other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return totalMatchesPlayed == other.totalMatchesPlayed
        && maximumMatchesPerDay == other.maximumMatchesPerDay
        && averageMatchesPerDay.Equals(other.averageMatchesPerDay)
        && maximumPopulation == other.maximumPopulation 
        && averagePopulation.Equals(other.averagePopulation) 
        && Helpers.CompareLists(Top5GameModels, other.Top5GameModels)
        && Helpers.CompareLists(top5Maps, other.top5Maps);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((ServerStatistic) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = totalMatchesPlayed;
        hashCode = (hashCode*397) ^ maximumMatchesPerDay;
        hashCode = (hashCode*397) ^ averageMatchesPerDay.GetHashCode();
        hashCode = (hashCode*397) ^ maximumPopulation;
        hashCode = (hashCode*397) ^ averagePopulation.GetHashCode();
        hashCode = (hashCode*397) ^ (Top5GameModels != null ? Top5GameModels.GetHashCode() : 0);
        hashCode = (hashCode*397) ^ (top5Maps != null ? top5Maps.GetHashCode() : 0);
        return hashCode;
      }
    }

    #endregion
  }
}