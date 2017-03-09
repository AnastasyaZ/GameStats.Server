using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.DataModels;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.Database
{
  [TestFixture]
  public class WhenWorkWithServerInfo_DbAdapterShould
  {
    #region models for testing

    private readonly GameServerInfo serverInfo1 = new GameServerInfo
    {
      endpoint = "kontur.ru-1024",
      gameServer = new GameServer
      {
        name = "] My P3rfect GameServer [",
        gameModes = new[] { "DM", "TDM" }
      }
    };

    private readonly GameServerInfo serverInfo2 = new GameServerInfo
    {
      endpoint = "192.168.35.38-5454",
      gameServer = new GameServer
      {
        name = "LocalServer",
        gameModes = new[] { "SM", "OPM", "DM" }
      }
    };

    private readonly GameServerInfo serverInfo3 = new GameServerInfo
    {
      endpoint = "geeks-games.com-2048",
      gameServer = new GameServer
      {
        name = "GG", //todo remove
        gameModes = new[] { "DM" }
      }
    };

    #endregion

    [Test]
    public void ReturnAddedRecords()
    {
      using (var file = new TempFile())
      {
        using (var db = new LiteDbAdapter(file.Filename))
        {
          db.UpsertServerInfo(serverInfo1);
          var result = db.GetServerInfo(serverInfo1.endpoint);

          result.ShouldBeEquivalentTo(serverInfo1);
        }
      }
    }

    [Test]
    public void ReturnAllAddedRecords()
    {
      using (var file = new TempFile())
      {
        using (var db = new LiteDbAdapter(file.Filename))
        {
          IList<GameServerInfo> serverInfos = new[] { serverInfo1, serverInfo2, serverInfo3 };
          foreach (var info in serverInfos)
          {
            db.UpsertServerInfo(info);
          }

          var result = db.GetServers();

          result.Should().BeEquivalentTo(serverInfos);
        }
      }
    }

    [Test]
    public void KeepDataAfterClosingConnection()
    {
      using (var file = new TempFile())
      {
        using (var db = new LiteDbAdapter(file.Filename))
        {
          db.UpsertServerInfo(serverInfo1);
        }

        using (var db = new LiteDbAdapter(file.Filename))
        {
          var result = db.GetServerInfo(serverInfo1.endpoint);
          result.ShouldBeEquivalentTo(serverInfo1);
        }
      }
    }

    [Test]
    public void NotAddNewRecord_IfServerAlreadyAdded()
    {
      using (var file = new TempFile())
      using (var db = new LiteDbAdapter(file.Filename))
      {
        for (var i = 0; i < 10; i++)
        {
          db.UpsertServerInfo(serverInfo1);
          db.UpsertServerInfo(new GameServerInfo { endpoint = serverInfo1.endpoint, gameServer = new GameServer() });
          db.UpsertServerInfo(serverInfo1);
        }
        var result = db.GetServers().ToArray();
        result.Length.ShouldBeEquivalentTo(1);
        result[0].Should().Be(serverInfo1);
      }
    }

    [Test]
    public void DoNotAddNewRecords_IfServerAlreadyAdded_InParallelThreads()
    {
      for (var i = 0; i < 100; i++)
        using (var file = new TempFile())
        using (var db = new LiteDbAdapter(file.Filename))
        {
          var iterations = Parallel.For(0, 1000, _ =>
           {
             db.GetServers().Count(x => x.endpoint == serverInfo1.endpoint).Should().BeLessOrEqualTo(1);
             db.UpsertServerInfo(serverInfo1);
             db.GetServers().Count(x => x.endpoint == serverInfo1.endpoint).Should().Be(1);
           });
          while (!iterations.IsCompleted)
          {
            Thread.Sleep(100);
          }
          var result = db.GetServers().ToArray();
          result.Length.ShouldBeEquivalentTo(1);
          result[0].Should().Be(serverInfo1);
        }
    }

    [Test]
    public void ReadAndWrite_InParallelThreads()
    {
      for (var j = 0; j < 100; j++)
        using (var file = new TempFile())
        {
          using (var db = new LiteDbAdapter(file.Filename))
          {
            var iterations = Parallel.For(0, 1000, i =>
             {
               if (i % 2 == 0)
                 db.UpsertServerInfo(serverInfo1);
               else
                 db.GetServers().Count().Should().BeLessOrEqualTo(1);
             });
            while (!iterations.IsCompleted)
            {
              Thread.Sleep(100);
            }
            var result = db.GetServers().ToArray();
            result.Length.ShouldBeEquivalentTo(1);
            result[0].Should().Be(serverInfo1);
          }
        }
    }
  }

}