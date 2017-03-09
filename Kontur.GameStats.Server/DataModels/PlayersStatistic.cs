using System;

namespace Kontur.GameStats.Server.DataModels
{
  public class PlayersStatistic : IEquatable<PlayersStatistic>
  {
    public int totalMatchesPlayed { get; set; }
    public int totalMatchesWin { get; set; }
    public string favoriteServer { get; set; }
    public int uniqueServers { get; set; }
    public string favoriteGameMode { get; set; }
    public double averageScoreboardPercent { get; set; }
    public int maximumMatchesPerDay { get; set; }
    public double averageMatchesPerDay { get; set; }
    public DateTime lastMatchPlayed { get; set; }
    public double killToDeathRatio { get; set; }

    #region equality members

    public bool Equals(PlayersStatistic other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return totalMatchesPlayed == other.totalMatchesPlayed
        && totalMatchesWin == other.totalMatchesWin 
        && string.Equals(favoriteServer, other.favoriteServer)
        && uniqueServers == other.uniqueServers 
        && string.Equals(favoriteGameMode, other.favoriteGameMode)
        && averageScoreboardPercent.Equals(other.averageScoreboardPercent) 
        && maximumMatchesPerDay == other.maximumMatchesPerDay 
        && averageMatchesPerDay.Equals(other.averageMatchesPerDay)
        && lastMatchPlayed.Equals(other.lastMatchPlayed)
        && killToDeathRatio.Equals(other.killToDeathRatio);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((PlayersStatistic)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = totalMatchesPlayed;
        hashCode = (hashCode * 397) ^ totalMatchesWin;
        hashCode = (hashCode * 397) ^ (favoriteServer != null ? favoriteServer.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ uniqueServers;
        hashCode = (hashCode * 397) ^ (favoriteGameMode != null ? favoriteGameMode.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ averageScoreboardPercent.GetHashCode();
        hashCode = (hashCode * 397) ^ maximumMatchesPerDay;
        hashCode = (hashCode * 397) ^ averageMatchesPerDay.GetHashCode();
        hashCode = (hashCode * 397) ^ lastMatchPlayed.GetHashCode();
        hashCode = (hashCode * 397) ^ killToDeathRatio.GetHashCode();
        return hashCode;
      }
    }

    #endregion
  }
}