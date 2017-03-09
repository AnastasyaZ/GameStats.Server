using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.DataModels;
using Nancy.ModelBinding;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.Database
{
  [TestFixture]
  public class WhenWorkWithMatchInfo_DbAdapterShould
  {
    #region models for testing

    private readonly MatchInfo match1;
    private readonly MatchInfo match2;
    private readonly MatchInfo match3;

    public WhenWorkWithMatchInfo_DbAdapterShould()
    {
      match1 = new MatchInfo()
      {
        endpoint = "kontur.ru-1024",
        timestamp = DateTime.Parse("2017-01-22T15:17:00Z"),
        result = new MatchResult()
        {
          map = "DM-HelloWorld",
          gameModel = "DM",
          fragLimit = 20,
          timeLimit = 20,
          timeElapsed = 12.345678,
          scoreboard = new[]
        {
          new PlayersResult()
          {
            name = "Player1",
            frags = 20,
            kills = 21,
            deaths = 3
          },
          new PlayersResult()
          {
            name = "Player2",
            frags = 2,
            kills = 2,
            deaths = 21
          }
        }
        }
      };

      match2 = new MatchInfo()
      {
        endpoint = "192.168.35.38-5454",
        timestamp = DateTime.Parse("2014-10-30T00:00:00.00Z"),
        result = new MatchResult()
        {
          map = "Fury Road",
          gameModel = "Ex",
          fragLimit = 15,
          timeLimit = 10,
          timeElapsed = 7.4564231,
          scoreboard = new[]
        {
          new PlayersResult()
          {
            name = "Max",
            frags = 14,
            kills = 29,
            deaths = 15
          },
          new PlayersResult()
          {
            name = "Nux",
            frags = 15,
            kills = 29,
            deaths = 14
          }
        }
        }
      };

      match3 = new MatchInfo()
      {
        endpoint = "192.168.35.38-9090",
        timestamp = DateTime.Parse("2014-10-30T00:00:00.00Z"),
        result = match2.result
      };
    }

    #endregion

    [Test]
    public void AddAllRecords()
    {
      var matches = new[] { match1, match2, match3 };
      using (var file = new TempFile())
      using (var db = new LiteDbAdapter(file.Filename))
      {
        foreach (var match in matches)
          db.AddMatchInfo(match);

        foreach (var x in matches)
        {
          var y = db.GetMatches(x.endpoint).First();
          var z = db.GetMatchInfo(x.endpoint, x.timestamp);
          x.ShouldBeEquivalentTo(y);
          x.ShouldBeEquivalentTo(z);
        }
      }
    }

    [Test]
    public void AddAllRecords_Parallel()
    {
      for (var i = 0; i < 100; i++)
      {
        var matches = new[] {match1, match2, match3};
        using (var file = new TempFile())
        using (var db = new LiteDbAdapter(file.Filename))
        {
          Parallel.ForEach(matches, x =>
          {
            var rnd = new Random();
            Thread.Sleep(rnd.Next(100));
            db.AddMatchInfo(x);
            Thread.Sleep(rnd.Next(100));
            var y = db.GetMatches(x.endpoint).First();
            var z = db.GetMatchInfo(x.endpoint, x.timestamp);
            x.ShouldBeEquivalentTo(y);
            x.ShouldBeEquivalentTo(z);
          });
        }
      }
    }
  }
}