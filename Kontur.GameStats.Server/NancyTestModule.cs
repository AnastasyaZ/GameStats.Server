using Nancy;
using Nancy.ModelBinding;

namespace Kontur.GameStats.Server
{
    public class NancyTestModule : NancyModule
    {
        public class JsonModel
        {
            public string stringField { get; set; }
            public int intField { get; set; }
        }

        public NancyTestModule()
        {
            Get["/"] = _ => "Hello, Nancy is working.";
            Get["/get_json"] = _
                => Response.AsJson(new JsonModel() { intField = 5, stringField = "fgds" });
            Post["/post_json"] = _ =>
            {
                var model = this.Bind<JsonModel>();
                return $"Get model: stringField={model.stringField}, intField={model.intField}";
            };
        }
    }
}