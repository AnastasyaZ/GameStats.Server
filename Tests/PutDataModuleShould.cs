using FluentAssertions;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.Modules;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace Tests
{
    public class PutDataModuleShould
    {
        private Browser browser;

        [SetUp]
        public void SetUp()
        {
            browser = new Browser(with => with.Module<PutDataModule>());
        }

        [Test]
        public void ReturnOK_OnCorrectAdverticeRequest()
        {
            var model = new ServerInfo
            {
                name = "] My P3rfect Server [",
                gameModes = new[] { "DM", "TDM" }
            };

            var responce = browser.Put("/servers/kontur.ru-1024/info", with => with.JsonBody(model));

            responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Test]
        public void ReturnBadRequest_OnMatchesRequestFromUnknownServer()
        {
            var model = new MatchResults
            {
                map = "DM-HelloWorld",
                gameModel = "DM",
                fragLimit = 20,
                timeLimit = 20,
                timeElapsed = 12.345678,
                scoreboard = new[]
                {
                    new PlayersResult
                    {
                        name = "Player1",
                        frags = 20,
                        kills = 21,
                        deaths = 3
                    },
                    new PlayersResult
                    {
                        name = "Player2",
                        frags = 2,
                        kills = 2,
                        deaths = 21
                    }
                }
            };

            var responce = browser.Put("/servers/kontur.ru-1024/matches/2017-01-22T15:17:00Z",
                with => with.JsonBody(model));

            responce.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadGateway);
        }
    }
}