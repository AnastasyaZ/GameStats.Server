using FluentAssertions;
using Kontur.GameStats.Server.Modules;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.Modules
{
  public class PutDataModuleShould
  {
    private Browser browser;

    [SetUp]
    public void SetUp()
    {
      browser = new Browser(new BootstrapperForSingletoneDbAdapter());
    }

    [Test]
    public void ReturnOK_OnCorrectAdverticeRequest()
    {
      var endpoint = TestData.Server.endpoint;
      var server = TestData.Server.gameServer;

      var responce = browser.Put($"/servers/{endpoint}/info", with => with.JsonBody(server));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
    }

    [Test]
    public void ReturnBadRequest_OnMatchesRequestFromUnknownServer()
    {
      var endpoint = TestData.Match.endpoint;
      var timestamp = TestData.Match.timestamp.ToUniversalTime().ToString("s");
      var match = TestData.Match.result;


      var responce = browser.Put($"/servers/{endpoint}/matches/{timestamp}Z",
          with => with.JsonBody(match));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadGateway);
    }

    [Test]
    public void ReturnOK_OnMatchesRequestFromKnownServer()
    {
      var endpoint = TestData.Match.endpoint;
      var server = TestData.Server.gameServer;
      var timestamp = TestData.Match.timestamp.ToUniversalTime().ToString("s");
      var match = TestData.Match.result;

      browser.Put($"/servers/{endpoint}/info", with => with.JsonBody(server));

      var responce = browser.Put($"/servers/{endpoint}/matches/{timestamp}Z",
          with => with.JsonBody(match));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
    }
  }
}