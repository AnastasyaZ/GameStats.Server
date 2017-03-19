using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.GameStats.Server.DataModels;
using Nancy.Testing;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.Modules
{
  public class GetStatisticModuleShould : AbstractModuleTest
  {

    [SetUp]
    public void SetUp()
    {
      Browser.Put($"/servers/{Endpoint}/info",
        with => with.JsonBody(Server));

      for (var i = 0; i < 60; i++)
      {
        Browser.Put($"/servers/{Endpoint}/matches/{Timestamp}",
          with => with.JsonBody(Match));
      }
    }

    #region test for count of items

    [TestCase("", 5)]
    [TestCase("0", 0)]
    [TestCase("1", 1)]
    [TestCase("50", 50)]
    [TestCase("51", 50)]
    public void ReturnCorrectCountOfRecentMatches(string requested, int expectedCount)
    {
      var items = Browser.Get($"/reports/recent-matches/{requested}")
        .Body.DeserializeJson<IEnumerable<MatchReportInfo>>();
      items.Count().Should().Be(expectedCount);
    }

    [TestCase("", 5)]
    [TestCase("0", 0)]
    [TestCase("1", 1)]
    [TestCase("50", 50)]
    [TestCase("51", 50)]
    public void ReturnCorrectCountOfBestPlayers(string requested, int expectedCount)
    {
      var items = Browser.Get($"/reports/best-players/{requested}")
        .Body.DeserializeJson<IEnumerable<PlayerReportInfo>>();
      items.Count().Should().BeGreaterOrEqualTo(0).And.BeLessOrEqualTo(expectedCount);
    }

    [TestCase("", 5)]
    [TestCase("0", 0)]
    [TestCase("1", 1)]
    [TestCase("50", 50)]
    [TestCase("51", 50)]
    public void ReturnCorrectCountOfPopularservers(string requested, int expectedCount)
    {
      var items = Browser.Get($"/reports/popular-servers/{requested}")
        .Body.DeserializeJson<IEnumerable<ServerReportInfo>>();
      items.Count().Should().BeGreaterOrEqualTo(0).And.BeLessOrEqualTo(expectedCount);
    }

    #endregion
    
  }
}