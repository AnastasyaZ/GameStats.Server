using FluentAssertions;
using Kontur.GameStats.Server.NancyModules;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.Modules
{
  public class GetStatisticModuleShould
  {
    private Browser browser;

    [SetUp]
    public void SetUp()
    {
      browser = new Browser(with => with.Module<GetStatisticModule>());
    }

    [Test]
    public void NotImplemented_OnServerStatistic()
    {
      var responce = browser.Get("/servers/kontur.ru-1024/stats");
      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotImplemented);
    }

    [Test]
    public void NotImplemented_OnPlayerStatistic()
    {
      var responce = browser.Get("/players/MadMax/stats");
      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotImplemented);
    }

    [Test]
    public void NotImplemented_OnRecentMatches()
    {
      var responce = browser.Get("/reports/recent-matches");
      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotImplemented);

      responce = browser.Get("/reports/recent-matches/20");
      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotImplemented);
    }

    [Test]
    public void NotImplemented_OnBestPlayers()
    {
      var responce = browser.Get("/reports/best-players");
      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotImplemented);

      responce = browser.Get("/reports/best-players/30");
      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotImplemented);
    }

    [Test]
    public void NotImplemented_OnPopulatServers()
    {
      var responce = browser.Get("/reports/popular-servers");
      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotImplemented);

      responce = browser.Get("/reports/popular-servers/40");
      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotImplemented);
    }
  }
}