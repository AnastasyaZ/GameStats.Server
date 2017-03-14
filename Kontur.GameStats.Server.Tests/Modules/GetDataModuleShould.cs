using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.GameStats.Server.DataModels;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.Modules
{
  public class GetDataModuleShould : AbstractModuleTest
  {
    [Test]
    public void ReturnNotFound_OnInfoRequestFromUnknownServer()
    {
      var responce = Browser.Get($"/servers/{Endpoint}/Info");

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
    }

    [Test]
    public void ReturnStoredServer()
    {
      Browser.Put($"/servers/{Endpoint}/info",
        with => with.JsonBody(Server));

      var response = Browser.Get($"/servers/{Endpoint}/Info");

      response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
      var server = response.Body.DeserializeJson<GameServer>();
      server.ShouldBeEquivalentTo(Server);
    }

    [Test]
    public void ReturnNotFound_OnUnknownMatch()
    {
      var responce = Browser.Get($"/servers/{Endpoint}/matches/{Timestamp}");

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
    }

    [Test]
    public void ReturnStoredMatch()
    {
      Browser.Put($"/servers/{Endpoint}/info",
        with => with.JsonBody(Server));
      Browser.Put($"/servers/{Endpoint}/matches/{Timestamp}",
          with => with.JsonBody(Match));

      var responce = Browser.Get($"/servers/{Endpoint}/matches/{Timestamp}");

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
      var match = responce.Body.DeserializeJson<MatchResult>();
      match.ShouldBeEquivalentTo(Match);
    }

    [Test]
    public void ReturnFirstMatch_IfTwoOrMoreHaveEqualEndpointAndTimestamp()
    {
      var first = TestData.Matches[0].result;
      var second = TestData.Matches[1].result;

      Browser.Put($"/servers/{Endpoint}/info",
        with => with.JsonBody(Server));

      Browser.Put($"/servers/{Endpoint}/matches/{Timestamp}",
          with => with.JsonBody(first));
      Browser.Put($"/servers/{Endpoint}/matches/{Timestamp}",
          with => with.JsonBody(second));

      var responce = Browser.Get($"/servers/{Endpoint}/matches/{Timestamp}");

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
      var match = responce.Body.DeserializeJson<MatchResult>();
      match.ShouldBeEquivalentTo(first);
    }

    [Test]
    public void ReturnAllAddedServers()
    {
      var servers = TestData.Servers;
      foreach (var s in servers)
      {
        Browser.Put($"/servers/{s.endpoint}/info",
          with => with.JsonBody(s.gameServer));
      }

      var responce = Browser.Get("/servers/Info");

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
      var responceServers = responce.Body.DeserializeJson<IEnumerable<GameServer>>();
      responceServers.Should().BeEquivalentTo(servers.Select(x => x.gameServer));
    }

    [Test]
    public void ReturnEmptyArray_IfNoOneServerWasntAdded()
    {
      var responce = Browser.Get("/servers/Info");

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
      var responceServers = responce.Body.DeserializeJson<IEnumerable<GameServer>>();
      responceServers.Should().BeEmpty();
    }
  }
}