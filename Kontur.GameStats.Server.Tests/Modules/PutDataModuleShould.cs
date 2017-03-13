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

    [Ignore("not implemented")]
    [TestCase("2009-06-15T13:45:30")]
    [TestCase("6/15/2009")]
    [TestCase("2009-06-15T13:45:30.0000000-07:00")]
    [TestCase("2009-06-15T13:45:30.0000000Z")]
    [TestCase(" 2009-06-15T13:45:30")]
    [TestCase("2009-06-15 13:45:30Z")]
    public void ReturnBadRequest_OnIncorrectTimestamp(string timestamp)
    {
      var responce = Browser.Put($"/servers/{Endpoint}/matches/{Timestamp}",
        with => with.JsonBody(Match));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
    }

    [Test]
    [Ignore("not implemented")]
    public void ReturnBadRequest_OnIncorrectEndpoint()
    {
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

    //TODO add test for incorrect only one field in model
    //TODO check elements of arrays also
    //TODO check string on null/empty
    //TODO add check fir incorrect endpoint and timestamp
    //TODO DRY in testcases
  }
}