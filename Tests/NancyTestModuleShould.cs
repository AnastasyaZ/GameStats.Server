using FluentAssertions;
using Kontur.GameStats.Server;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace Tests
{
    [TestFixture]
    [Ignore("Example of Tests")]
    public class NancyTestModuleShould
    {
        private Browser browser;

        [SetUp]
        public void SetUp()
        {
            browser = new Browser(with => with.Module<NancyTestModule>());
        }

        [Test]
        public void Return_200OK_OnRequest()
        {
            var response = browser.Get("/", with => with.HttpRequest());

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            response.Body.AsString().ShouldBeEquivalentTo("Hello, Nancy is working.");
        }

        [Test]
        public void Return_JsonModel()
        {
            var response = browser.Get("/get_json", with => with.HttpRequest());

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            var model = response.Body.DeserializeJson<NancyTestModule.JsonModel>();
            model.intField.ShouldBeEquivalentTo(5);
            model.stringField.ShouldBeEquivalentTo("fgds");
        }

        [Test]
        public void Get_JsonModel()
        {
            var model = new NancyTestModule.JsonModel { stringField = "ewq", intField = 85 };

            var response = browser.Post("/post_json", with =>
            {
                with.HttpRequest();
                with.Body(JSON.ToJSON(model));
                with.Header("content-type", "application/json");
            });

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            response.Body.AsString().ShouldBeEquivalentTo(
            $"Get model: stringField={model.stringField}, intField={model.intField}");
        }
    }
}
