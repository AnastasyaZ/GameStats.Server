using System.Collections.Generic;
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

      var response= Browser.Get($"/servers/{Endpoint}/Info");

      response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
      var server=response.Body.DeserializeJson<GameServer>();
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

    //TODO а если таких два??

    [Test]
    public void ReturnOK_OnInfoRequest()
    {
      var responce = Browser.Get("/servers/Info");

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
      responce.Body.DeserializeJson<IEnumerable<GameServerInfo>>();//TODO
    }
  }
}