using System.Text.RegularExpressions;
using Nancy.Routing.Constraints;

namespace Kontur.GameStats.Server.NancyModules.NancyConfiguration.RouteConstraints
{
  public class UrlConstraint : RouteSegmentConstraintBase<string>
  {
    protected override bool TryMatch(string constraint, string segment, out string matchedValue)
    {
      if (Regex.IsMatch(segment, "^[\\w.-]+-[0-9]{1,5}$"))
      {
        matchedValue = segment.ToLower();
        return true;
      }
      matchedValue = null;
      return false;
    }

    public override string Name => "url";

  }
}