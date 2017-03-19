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
  public class GetDataHandlerShould
  {
    private GetDataHandler handler;
    private IDatabaseAdapter db;
    private TempFile file;

    private readonly GameServer server = TestData.Server;
    private readonly MatchInfo match = TestData.Match;

    [SetUp]
    public void SetUp()
    {
      file = new TempFile();
      db = new LiteDbAdapter(file.Filename);
      handler = new GetDataHandler(db);
    }

    [TearDown]
    public void TearDown()
    {
      db.Dispose();
      file.Dispose();
    }

    [Test]
    public void ReturnStoredGameServer()
    {
      db.UpsertServerInfo(server);

      var result = handler.GetGameServer(server.endpoint);

      result.ShouldBeEquivalentTo(server.info);
    }

    [Test]
    public void ReturnNull_ForUnknownServer()
    {
      var result = handler.GetGameServer(server.endpoint);

      result.Should().BeNull();
    }

    [Test]
    public void ReturnStoredMatchResult()
    {
      db.AddMatchInfo(match);

      var result = handler.GetMatchResult(match.endpoint, match.timestamp);

      result.ShouldBeEquivalentTo(match.result);
    }

    [Test]
    //TODO убрать потом этот тест надо или заменить на симпатичный
    public void ReturnStoredMatchResult_IfTimestampIsEqualButStoredInOtherObject()
    {
      db.AddMatchInfo(match);
      var equalTimestamp = match.timestamp.ToUtcString().ParseInUts();
      var result = handler.GetMatchResult(match.endpoint, equalTimestamp);

      result.ShouldBeEquivalentTo(match.result);
    }

    [Test]
    public void ReturnNull_ForUnknownMatch()
    {
      var result = handler.GetMatchResult(match.endpoint, match.timestamp);

      result.Should().BeNull();
    }

    [Test]
    public void ReturnAllServers()
    {
      foreach (var serverInfo in TestData.Servers)
        db.UpsertServerInfo(serverInfo);

      var result = handler.GetGameServers();

      result.Should().BeEquivalentTo(TestData.Servers.Select(s=>s.info));
    }

    [Test]
    public void ReturnEmptyArray_IfNoOneServerIsStored()
    {

      var result = handler.GetGameServers();

      result.Should().BeEmpty();
    }
  }
}