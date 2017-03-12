using Kontur.GameStats.Server.DataModels;
using Nancy;

namespace Kontur.GameStats.Server.Modules
{
  public class GetDataModule : NancyModule
  {
    public GetDataModule()
    {
      Get["/servers/{endpoint}/Info"] = _ =>
      {
        return HttpStatusCode.NotFound;
      };

      Get["/servers/Info"] = _ =>
      {
        return Response.AsJson(new[] { new GameServerInfo() });
      };

      Get["/servers/{endpoint}/matches/{timestamp}"] = _ =>
      {
        return HttpStatusCode.NotFound;
      };
    }
  }
}