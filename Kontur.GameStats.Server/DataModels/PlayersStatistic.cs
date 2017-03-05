using System;

namespace Kontur.GameStats.Server.DataModels
{
  public class PlayersStatistic
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
  }
}