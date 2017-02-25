using Kontur.GameStats.Server.DataModels;
using Nancy;

namespace Kontur.GameStats.Server.Modules
{
    public class GetDataModule : NancyModule
    {
        public GetDataModule()
        {
            Get["/servers/{endpoint}/info"] = _ =>
            {
                return HttpStatusCode.NotFound;
            };

            Get["/servers/info"] = _ =>
            {
                return Response.AsJson(new[] { new GameServer() });
            };

            Get["/servers/{endpoint}/matches/{timestamp}"] = _ =>
            {
                return HttpStatusCode.NotFound;
            };
        }
    }
}