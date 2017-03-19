using System;
using System.Globalization;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.Utility
{
  public static class ConvertExtensions
  {
    public static DateTime ParseInUts(this string timestamp)
    {
     return DateTime.Parse(timestamp,CultureInfo.InvariantCulture).ToUniversalTime();
    }

    public static string ToUtcString(this DateTime dateTime)
    {
      return $"{dateTime.ToUniversalTime().ToString("s")}Z";
    }

    public static MatchReportInfo ToReportInfo(this MatchInfo match)
    {
      return new MatchReportInfo
      {
        server = match.endpoint,
        timestamp = match.timestamp.ToUtcString(),
        results = match.result
      };
    }
  }
}