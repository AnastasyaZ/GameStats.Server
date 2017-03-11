﻿using System.Configuration;
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

    [SetUp]
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