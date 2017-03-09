﻿using System.Collections.Generic;
using FluentAssertions;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.Modules;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace Kontur.GameStats.Server.Tests.Modules
{
  public class GetDataModuleShould
  {
    private Browser browser;

    [SetUp]
    public void SetUp()
    {
      browser = new Browser(with => with.Module<GetDataModule>());
    }

    [Test]
    public void ReturnNotFound_OnInfoRequestFromUnknownServer()
    {
      var responce = browser.Get("/servers/kontur.ru-1024/Info1");

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
    }

    [Test]
    public void ReturnBadRequest_OnInfoRequest()
    {
      var responce = browser.Get("/servers/Info1");

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
      responce.Body.DeserializeJson<IEnumerable<GameServerInfo>>();
    }

    [Test]
    public void ReturnNotFound_OnUnknownMatch()
    {
      var responce = browser.Get("/servers/kontur.ru-1024/matches/2017-01-22T15:17:00Z");

      responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
    }
  }
}