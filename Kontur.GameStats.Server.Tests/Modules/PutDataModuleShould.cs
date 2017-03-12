using System.Configuration;
using System.IO;
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
    private BootstrapperForSingletoneDbAdapter bootstrapper;

    [OneTimeSetUp]
    public void SetUp()
    {
      bootstrapper = new BootstrapperForSingletoneDbAdapter();
      browser = new Browser(bootstrapper);
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
      bootstrapper.Dispose();
      var directory= ConfigurationManager.AppSettings["database_directory"];
      ClearDirectory(directory);
    }

    private void ClearDirectory(string path)
    {
      var dir = new DirectoryInfo(path);
      foreach (var file in dir.GetFiles())
      {
        file.Delete();
      }
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
    public void ReturnBadRequest_OnIncorrectAdverticeRequestBody()
    {
      var endpoint = TestData.Server.endpoint;
      var notServer = TestData.Match;

      var responce = browser.Put($"/servers/{endpoint}/info", with => with.JsonBody(notServer));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);

    }

    [Test]
    [Ignore("not implemented")]
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
    [Ignore("not implemented")]
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

    [Test]
    [Ignore("not implemented")]
    public void ReturnBadRequest_OnIncorrectTimestamp()
    {
    }

    [Test]
    public void ReturnBadRequest_OnIncorrectMatchResultModel()
    {
      var endpoint = TestData.Match.endpoint;
      var server = TestData.Server.gameServer;
      var timestamp = TestData.Match.timestamp.ToUniversalTime().ToString("s");
      var notMatch = TestData.Server;

      browser.Put($"/servers/{endpoint}/info", with => with.JsonBody(server));

      var responce = browser.Put($"/servers/{endpoint}/matches/{timestamp}Z",
          with => with.JsonBody(notMatch));

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);

    }

    //TODO add test for incorrect only one field in model
    //TODO check elements of arrays also
    //TODO check string on null/empty
  }
}