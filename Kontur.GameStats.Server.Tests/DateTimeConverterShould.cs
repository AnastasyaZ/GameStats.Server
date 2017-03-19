using System;
using FluentAssertions;
using Kontur.GameStats.Server.Utility;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests
{
  [TestFixture]
  public class DateTimeConverterShould
  {
    [Test]
    public void ConvertFromString_ToUtcDateTime()
    {
      const string srt = "2017-01-22T15:17:00Z";

      var datetime = srt.ParseInUts();

      datetime.Kind.ShouldBeEquivalentTo(DateTimeKind.Utc);

      datetime.Year.ShouldBeEquivalentTo(2017);
      datetime.Month.ShouldBeEquivalentTo(1);
      datetime.Day.ShouldBeEquivalentTo(22);
      datetime.Hour.ShouldBeEquivalentTo(15);
      datetime.Minute.ShouldBeEquivalentTo(17);
      datetime.Second.ShouldBeEquivalentTo(0);
      datetime.Millisecond.ShouldBeEquivalentTo(0);
    }

    [Test]
    public void ConvertFromDateTime_ToUtcString()
    {
      var datetime = new DateTime(2017, 1, 22, 15, 17, 0, DateTimeKind.Utc);
      var str = datetime.ToUtcString();
      str.ShouldBeEquivalentTo("2017-01-22T15:17:00Z");
    }
  }
}