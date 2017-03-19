using System;
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
  public class WhenWorkWithMatchInfo_DbAdapterShould
  {
    private IList<MatchInfo> matches;

    [SetUp]
    public void SetUp()
    {
      matches = TestData.Matches
        .GroupBy(x => $"{x.endpoint} {x.timestamp}")
        .Select(x => x.First())
        .ToArray();
    }


    [Test]
    public void AddAllRecords()
    {
      using (var file = new TempFile())
      using (var db = new LiteDbAdapter(file.Filename))
      {
        foreach (var match in matches)
          db.AddMatchInfo(match);

        foreach (var match in matches)
        {
          var x = db.GetMatches().First(m => m.endpoint == match.endpoint && m.timestamp == match.timestamp);
          var y = db.GetMatches(match.endpoint).First(m => m.timestamp == match.timestamp);
          var z = db.GetMatchInfo(match.endpoint, match.timestamp);
          match.ShouldBeEquivalentTo(x);
          match.ShouldBeEquivalentTo(y);
          match.ShouldBeEquivalentTo(z);
        }
      }
    }

    [Test]
    public void AddAllRecords_Parallel()
    {
      for (var i = 0; i < 100; i++)
      {
        using (var file = new TempFile())
        using (var db = new LiteDbAdapter(file.Filename))
        {
          Parallel.ForEach(matches, match =>
          {
            var rnd = new Random();
            Thread.Sleep(rnd.Next(100));
            db.AddMatchInfo(match);
            Thread.Sleep(rnd.Next(100));
            var x = db.GetMatches().First(m => m.endpoint == match.endpoint && m.timestamp == match.timestamp);
            var y = db.GetMatches(match.endpoint).First(m => m.timestamp == match.timestamp);
            var z = db.GetMatchInfo(match.endpoint, match.timestamp);
            match.ShouldBeEquivalentTo(x);
            match.ShouldBeEquivalentTo(y);
            match.ShouldBeEquivalentTo(z);
          });
        }
      }
    }

    [Test]
    public void ReturnRecentMatches_InCorrectOrder()
    {
      using (var file = new TempFile())
      using (var db = new LiteDbAdapter(file.Filename))
      {
        foreach (var match in TestData.Matches)
          db.AddMatchInfo(match);

        var sortedMatches = db.GetRecentMatches(TestData.Matches.Length);
        sortedMatches.Select(x => x.timestamp).Should().Equal(TestData.Timestamps);
      }
    }

    [Test]
    public void ReturnCorrectMatchesCount()
    {
      using (var file = new TempFile())
      using (var db = new LiteDbAdapter(file.Filename))
      {
        foreach (var match in TestData.Matches)
          db.AddMatchInfo(match);

        var sortedMatches = db.GetRecentMatches(3);
        sortedMatches.Select(x => x.timestamp).Should().Equal(TestData.Timestamps.Take(3));
      }
    }

    [Test]
    public void ReturnsEmptyArrayIfCountIsZero()
    {
      using (var file = new TempFile())
      using (var db = new LiteDbAdapter(file.Filename))
      {
        foreach (var match in TestData.Matches)
          db.AddMatchInfo(match);

        db.GetRecentMatches(0).Should().BeEmpty().And.BeAssignableTo<IList<MatchInfo>>();
      }
    }
  }
}