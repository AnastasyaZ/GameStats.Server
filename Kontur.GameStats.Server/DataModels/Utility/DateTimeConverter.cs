using System;
using System.Globalization;

namespace Kontur.GameStats.Server.DataModels.Utility
{
  public static class DateTimeConverter
  {
    public static DateTime ParseInUts(this string timestamp)
    {
     return DateTime.Parse(timestamp,CultureInfo.InvariantCulture).ToUniversalTime();
    }

    public static string ToUtcString(this DateTime dateTime)
    {
      return $"{dateTime.ToUniversalTime().ToString("s")}Z";
    }
  }
}