using System;

namespace Kontur.GameStats.Server.DataModels
{
  public class MatchReportInfo
  {
    public string server { get; set; }
    public string timestamp { get; set; }
    public MatchResult results { get; set; }
  }
}