using System;
using System.Text.RegularExpressions;
using Kontur.GameStats.Server.DataModels.Utility;
using Nancy.Routing.Constraints;

namespace Kontur.GameStats.Server.Modules.RouteConstraints
{
  public class TimestampConstraint : RouteSegmentConstraintBase<DateTime>
  {
    protected override bool TryMatch(string constraint, string segment, out DateTime matchedValue)
    {
      if (Regex.IsMatch(segment, "^[0-9]{4}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}:[0-9]{2}Z$"))
      {
        matchedValue = segment.ParseInUts();
        return true;
      }
      matchedValue = new DateTime();
      return false;
    }

    public override string Name => "utc_timestamp";
  }
}