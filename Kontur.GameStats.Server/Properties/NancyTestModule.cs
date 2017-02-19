using Nancy;

namespace Kontur.GameStats.Server.Properties
{
    public class NancyTestModule:NancyModule
    {
        public NancyTestModule()
        {
            Get["/"] = _ => "Hello, Nancy is working.";
        }
    }
}