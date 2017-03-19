using System.Linq;
using FluentAssertions;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.DataModels.Utility;
using Kontur.GameStats.Server.RequestHandlers;
using Kontur.GameStats.Server.Tests.Database;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.RequestHandlers
{
  [TestFixture]
  public class PutDataHandlerShould
  {
    private PutDataHandler handler;
    private IDatabaseAdapter db;
    private TempFile file;

    private readonly GameServer server = TestData.Server;
    private readonly MatchInfo match = TestData.Match;

    [SetUp]
    public void SetUp()
    {
      file = new TempFile();
      db = new LiteDbAdapter(file.Filename);
      handler = new PutDataHandler(db);
    }

    [TearDown]
    public void TearDown()
    {
      db.Dispose();
      file.Dispose();
    }

    [Test]
    public void SaveServerInfo()
    {
      handler.PutServer(server);

      var savedData = db.GetServerInfo(server.endpoint);

      savedData.ShouldBeEquivalentTo(server);
    }

    [Test]
    public void UpdateServerInfo()
    {
      db.UpsertServerInfo(server);

      var updatedServer = new GameServer
      {
        endpoint = server.endpoint,
        info = new ServerInfo
        {
          name = "New servername",
          gameModes = new[] { "TDM" }
        }
      };
      handler.PutServer(updatedServer);

      var savedData = db.GetServerInfo(server.endpoint);

      savedData.ShouldBeEquivalentTo(updatedServer);
    }

    [Test]
    public void NotSaveMatchResultFromUnknownServer()
    {
      var result = handler.TryPutMatch(match);

      result.Should().BeFalse();
      db.GetMatches(match.endpoint).Count().Should().Be(0);
    }

    [Test]
    public void SaveMatchResult()
    {
      db.UpsertServerInfo(server);
      var result = handler.TryPutMatch(match);

      result.Should().BeTrue();
      var r = db.GetMatchInfo(match.endpoint, match.timestamp);
      r.ShouldBeEquivalentTo(match);
    }

    [TestCase("2017-01-22T15:17:00Z")]
    [TestCase("2010-01-01T00:00:00Z")]
    [TestCase("2033-12-31T23:59:59Z")]
    public void WorkWithMatchesFromFutureAndPast(string timestamp)
    {
      var datetime = timestamp.ParseInUts();
      var anomalyMatch = new MatchInfo
      {
        endpoint = match.endpoint,
        timestamp = datetime,
        result = match.result
      };
      db.UpsertServerInfo(server);

      var result = handler.TryPutMatch(anomalyMatch);

      result.Should().BeTrue();
      db.GetMatchInfo(anomalyMatch.endpoint, anomalyMatch.timestamp).ShouldBeEquivalentTo(anomalyMatch);
    }
  }
}