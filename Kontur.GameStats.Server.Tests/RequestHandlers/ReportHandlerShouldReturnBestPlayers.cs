using System.Linq;
using FakeItEasy;
using FluentAssertions;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.RequestHandlers;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.RequestHandlers
{
  [TestFixture]
  public class ReportHandlerShould
  {
    private readonly IDatabaseAdapter database = A.Fake<IDatabaseAdapter>();
    private ReportsHandler handler;

    [Test]
    public void ReturnBestPlayersInCorrectOrder()
    {
      var scoreboard = TestData.Scoreboard;
      var matchInfo = new MatchInfo { result = new MatchResult { scoreboard = scoreboard } };
      const int matchCount = 10;
      A.CallTo(() => database.GetMatches()).Returns(Enumerable.Repeat(matchInfo, matchCount).ToArray());
      handler = new ReportsHandler(database);

      var bestPlayers = handler.GetBestPlayers(3).ToArray();

      bestPlayers.Should().BeInDescendingOrder(x => x.killToDeathRatio);
      foreach (var player in bestPlayers)
      {
        var expectedPlayer = scoreboard.First(x => x.name == player.name);
        var expectedRatio = (double)expectedPlayer.kills / expectedPlayer.deaths;
        player.killToDeathRatio.Should().Be(expectedRatio);
      }
    }

    [Test]
    public void NotReturnsPlayers_WhichAreNotBeenKilled()
    {
      var matchInfo = new MatchInfo { result = new MatchResult { scoreboard = new[] { new PlayerInfo { name = "player", deaths = 0 } } } };
      const int matchCount = 10;
      A.CallTo(() => database.GetMatches()).Returns(Enumerable.Repeat(matchInfo, matchCount).ToArray());
      handler = new ReportsHandler(database);

      handler.GetBestPlayers(3).Should().BeEmpty();
    }

    [Test]
    public void NotReturnsPlayers_WhichPlaysLessThan10Times()
    {
      var scoreboard = TestData.Scoreboard;
      var matchInfo = new MatchInfo { result = new MatchResult { scoreboard = scoreboard } };
      const int matchCount = 9;
      A.CallTo(() => database.GetMatches()).Returns(Enumerable.Repeat(matchInfo, matchCount).ToArray());
      handler = new ReportsHandler(database);

      handler.GetBestPlayers(3).Should().BeEmpty();
    }

    [Test]
    public void ReturnsNotMorePlayersThanCount()
    {
      var scoreboard = TestData.Scoreboard;
      var matchInfo = new MatchInfo { result = new MatchResult { scoreboard = scoreboard } };
      const int matchCount = 10;
      A.CallTo(() => database.GetMatches()).Returns(Enumerable.Repeat(matchInfo, matchCount).ToArray());
      handler = new ReportsHandler(database);

      handler.GetBestPlayers(2).Count().Should().Be(2);
    }

    [Test]
    public void ReturnsLessPlayersThanCount_IfThereareLacksOfAppropriatePlayes()
    {
      var scoreboard = TestData.Scoreboard;
      var matchInfo = new MatchInfo { result = new MatchResult { scoreboard = scoreboard } };
      const int matchCount = 10;
      A.CallTo(() => database.GetMatches()).Returns(Enumerable.Repeat(matchInfo, matchCount).ToArray());
      handler = new ReportsHandler(database);

      handler.GetBestPlayers(5).Count().Should().Be(3);
    }

    //TODO split ino two classes amd move initialization in ctor

    [Test]
    public void ReturnPopularServers_InCorrectOrder()
    {
      A.CallTo(() => database.GetMatches()).Returns(TestData.Matches);
      A.CallTo(() => database.GetServers()).Returns(TestData.Servers);
      A.CallTo(() => database.GetRecentMatches(1))
        .Returns(TestData.Matches.OrderByDescending(x=>x.timestamp).Take(1).ToArray());
      handler = new ReportsHandler(database);

      var popularServers = handler.GetPopularServers(10).ToArray();
      popularServers.Should().Equal(TestData.BestServers);
    }

    [Test]
    public void ReturnNotMoreServersThanCount()
    {
      A.CallTo(() => database.GetMatches()).Returns(TestData.Matches);
      A.CallTo(() => database.GetServers()).Returns(TestData.Servers);
      A.CallTo(() => database.GetRecentMatches(1))
        .Returns(TestData.Matches.OrderByDescending(x => x.timestamp).Take(1).ToArray());
      handler = new ReportsHandler(database);

      var popularServers = handler.GetPopularServers(2).ToArray();
      popularServers.Length.Should().Be(2);
    }

    [Test]
    public void ReturnsLessServersThenCount_IfThereLackOfServers()
    {
      A.CallTo(() => database.GetMatches()).Returns(TestData.Matches.Take(1).ToArray());
      A.CallTo(() => database.GetServers()).Returns(TestData.Servers);
      A.CallTo(() => database.GetRecentMatches(1))
        .Returns(TestData.Matches.OrderByDescending(x => x.timestamp).Take(1).ToArray());
      handler = new ReportsHandler(database);

      var popularServers = handler.GetPopularServers(2).ToArray();
      popularServers.Length.Should().Be(1);
    }

    [Test]
    public void ReturnsEmptyCollection_IfDbIsEmpty()
    {
      A.CallTo(() => database.GetMatches()).Returns(new MatchInfo[0]);
      A.CallTo(() => database.GetServers()).Returns(new GameServerInfo[0]);
      A.CallTo(() => database.GetRecentMatches(1)).Returns(new MatchInfo[0]);
      handler = new ReportsHandler(database);

      handler.GetPopularServers(2).Should().BeEmpty();
    }
  }
}