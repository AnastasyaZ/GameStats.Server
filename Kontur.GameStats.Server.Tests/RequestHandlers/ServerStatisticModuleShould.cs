using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.RequestHandlers;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.RequestHandlers
{
  [TestFixture]
  public class ServerStatisticModuleShould
  {
    private IDatabaseAdapter database;
    private ServerStatisticHandler handler;

    private readonly string endpoint = TestData.Match.endpoint;

    [SetUp]
    public void SetUp()
    {
      database = A.Fake<IDatabaseAdapter>();
      A.CallTo(() => database.GetMatches(endpoint))
        .Returns(TestData.Matches);
      handler=new ServerStatisticHandler(database);
    }

    [Test]
    public void ReturnCorrectStatistic()
    {
      var expectation = TestData.ServerStatistic;
      var statistic = handler.GetStatistic(endpoint);

      ((int)statistic["totalMatchesPlayed"]).Should().Be(expectation["totalMatchesPlayed"]);
      ((int)statistic["maximumMatchesPerDay"]).Should().Be(expectation["maximumMatchesPerDay"]);
      //((double)statistic["averageMatchesPerDay"]).Should().BeApproximately((double)expectation["averageMatchesPerDay"], 0.0001);
      ((int)statistic["maximumPopulation"]).Should().Be(expectation["maximumPopulation"]);
      ((double)statistic["averagePopulation"]).Should().BeApproximately((double)expectation["averagePopulation"], 0.0001);
      ((IList<string>)statistic["top5GameModes"]).Should().ContainInOrder((IList<string>) expectation["top5GameModes"]);

      ((IList<string>)statistic["top5Maps"]).Should().StartWith(expectation["top5Maps"][0])
        .And.BeEquivalentTo(expectation["top5Maps"]);
    }
  }
}