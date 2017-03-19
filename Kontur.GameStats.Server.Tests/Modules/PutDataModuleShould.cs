using FluentAssertions;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.Modules
{
  public class PutDataModuleShould : AbstractModuleTest
  {
    [Test]
    public void ReturnOK_OnCorrectAdverticeRequest()
    {
      var responce = Browser.Put($"/servers/{Endpoint}/info",
        with => with.JsonBody(Server));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
    }

    [TestCase("kontur.ru-1024")]
    [TestCase("name-with-dashes.com-5454")]
    [TestCase("H0stNameWithCapitalLe44ersAndNumb3rs.shop-15314")]
    [TestCase("192.168.35.38-80")]
    public void ReturnOK_ForDifferentEndpointFormats(string endpoint)
    {
      var responce = Browser.Put($"/servers/{endpoint}/info",
        with => with.JsonBody(Server));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
    }

    [TestCase("2009-06-15T13:45:30")]
    [TestCase("6/15/2009")]
    [TestCase("2009-06-15T13:45:30.0000000-07:00")]
    [TestCase("2009-06-15T13:45:30.0000000Z")]
    [TestCase("2009-06-15T13:45:30")]
    public void ReturnNotFound_OnUnsupportedTimestamp(string timestamp)
    {
      var responce = Browser.Put($"/servers/{Endpoint}/matches/{timestamp}",
        with => with.JsonBody(Match));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
    }

    [Test]
    public void ReturnBadRequest_OnIncorrectAdverticeRequestBody()
    {
      var notServer = Match;
      var responce = Browser.Put($"/servers/{Endpoint}/info",
        with => with.JsonBody(notServer));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
    }

    [Test]
    public void ReturnOK_OnMatchesRequestFromKnownServer()
    {
      Browser.Put($"/servers/{Endpoint}/info",
        with => with.JsonBody(Server));

      var responce = Browser.Put($"/servers/{Endpoint}/matches/{Timestamp}",
        with => with.JsonBody(Match));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
    }

    [Test]
    public void ReturnBadRequest_OnMatchesRequestFromUnknownServer()
    {
      var responce = Browser.Put($"/servers/{Endpoint}/matches/{Timestamp}",
          with => with.JsonBody(Match));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotAcceptable);
    }

    [Test]
    public void ReturnBadRequest_OnIncorrectMatchResultModel()
    {
      var notMatch = TestData.Server;

      Browser.Put($"/servers/{Endpoint}/info",
        with => with.JsonBody(Server));

      var responce = Browser.Put($"/servers/{Endpoint}/matches/{Timestamp}",
          with => with.JsonBody(notMatch));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
    }
  }
}