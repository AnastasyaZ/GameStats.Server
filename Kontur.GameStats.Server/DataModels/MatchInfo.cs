using System;

namespace Kontur.GameStats.Server.DataModels
{
  public class MatchInfo
  {
    public string endpoint { get; set; }
    public DateTime timestamp { get; set; }
    public MatchResult result { get; set; }
  }
}