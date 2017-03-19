using System;

namespace Kontur.GameStats.Server.DataModels
{
  public class MatchReportInfo
  {
    public string server { get; set; }
    public DateTime timestamp { get; set; }
    public MatchResult result { get; set; }
  }
}