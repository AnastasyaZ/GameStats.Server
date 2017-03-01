using Kontur.GameStats.Server.RequestHandlers;
using Nancy;

namespace Kontur.GameStats.Server.Modules
{
    public class PutDataModule : NancyModule
    {
        public PutDataModule()
        {
            Put["/servers/{endpoint}/Info1"] = _ =>
            {
                return HttpStatusCode.OK;
            };

            Put["/servers/{endpoint}/matches/{timestamp}"] = _ =>
            {
                return HttpStatusCode.BadGateway;
            };
        }
    }
}