using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.GameStats.Server.Database;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.Database
{
  [TestFixture]
  public class WhenWorkWithMatchInfo_DbAdapterShould
  {
    [Test]
    public void AddAllRecords()
    {
      var matches = TestData.Matches;
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
      //for (var i = 0; i < 100; i++)
      {
        var matches = TestData.Matches;
        using (var file = new TempFile())
        using (var db = new LiteDbAdapter(file.Filename))
        {
          Parallel.ForEach(matches, x =>
          {
            //var rnd = new Random();
            //Thread.Sleep(rnd.Next(100));
            db.AddMatchInfo(x);
            //Thread.Sleep(rnd.Next(100));
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