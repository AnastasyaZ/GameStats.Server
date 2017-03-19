using System;
using FakeItEasy;
using FluentAssertions;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.RequestHandlers;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.RequestHandlers
{
  public class PlayerStatisticHandlerShould
  {
    [TestFixture]
    public class ServerStatisticHandlerShould
    {
      private IDatabaseAdapter database;
      private PlayerStatisticHandler handler;

      private const string PlayerName = "Player1";

      [SetUp]
      public void SetUp()
      {
        database = A.Fake<IDatabaseAdapter>();
        A.CallTo(() => database.GetMatches()).Returns(TestData.Matches);
        handler = new PlayerStatisticHandler(database);
      }

      [Test]
      public void ReturnCorrectStatistic()
      {
        var expectation = TestData.PlayerStatistic;
        var statistic = handler.GetStatistic(PlayerName);


        ((int)statistic["totalMatchesPlayed"]).Should().Be(expectation["totalMatchesPlayed"]);
        ((int)statistic["totalMatchesWon"]).Should().Be(expectation["totalMatchesWon"]);
        ((string)statistic["favoriteServer"]).Should().Be(expectation["favoriteServer"]);
        ((int) statistic["uniqueServers"]).Should().Be(expectation["uniqueServers"]);
        ((string) statistic["favoriteGameMode"]).Should().Be(expectation["favoriteGameMode"]);
        ((double) statistic["averageScoreboardPercent"]).Should()
          .BeApproximately((double)expectation["averageScoreboardPercent"],0.0001);
        ((int) statistic["maximumMatchesPerDay"]).Should().Be(expectation["maximumMatchesPerDay"]);
        ((double) statistic["averageMatchesPerDay"]).Should()
          .BeApproximately((double) expectation["averageMatchesPerDay"], 0.0001);
        ((string) statistic["lastMatchPlayed"]).Should().Be(expectation["lastMatchPlayed"]);
        ((double) statistic["killToDeathRatio"]).Should()
          .BeApproximately((double) expectation["killToDeathRatio"], 0.0001);
      }
    }
  }
}