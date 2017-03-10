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
    [Test]
    public void ReturnAddedRecords()
    {
      using (var file = new TempFile())
      {
        using (var db = new LiteDbAdapter(file.Filename))
        {
          db.UpsertServerInfo(TestData.Server);
          var result = db.GetServerInfo(TestData.Server.endpoint);

          result.ShouldBeEquivalentTo(TestData.Server);
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
          IList<GameServerInfo> serverInfos = TestData.Servers;
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
          db.UpsertServerInfo(TestData.Server);
        }

        using (var db = new LiteDbAdapter(file.Filename))
        {
          var result = db.GetServerInfo(TestData.Server.endpoint);
          result.ShouldBeEquivalentTo(TestData.Server);
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
          db.UpsertServerInfo(TestData.Server);
          db.UpsertServerInfo(new GameServerInfo { endpoint = TestData.Server.endpoint, gameServer = new GameServer() });
          db.UpsertServerInfo(TestData.Server);
        }
        var result = db.GetServers().ToArray();
        result.Length.ShouldBeEquivalentTo(1);
        result[0].Should().Be(TestData.Server);
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
             db.GetServers().Count(x => x.endpoint == TestData.Server.endpoint).Should().BeLessOrEqualTo(1);
             db.UpsertServerInfo(TestData.Server);
             db.GetServers().Count(x => x.endpoint == TestData.Server.endpoint).Should().Be(1);
           });
          while (!iterations.IsCompleted)
          {
            Thread.Sleep(100);
          }
          var result = db.GetServers().ToArray();
          result.Length.ShouldBeEquivalentTo(1);
          result[0].Should().Be(TestData.Server);
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
                 db.UpsertServerInfo(TestData.Server);
               else
                 db.GetServers().Count().Should().BeLessOrEqualTo(1);
             });
            while (!iterations.IsCompleted)
            {
              Thread.Sleep(100);
            }
            var result = db.GetServers().ToArray();
            result.Length.ShouldBeEquivalentTo(1);
            result[0].Should().Be(TestData.Server);
          }
        }
    }
  }

}