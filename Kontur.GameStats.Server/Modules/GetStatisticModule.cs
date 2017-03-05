using Nancy;

namespace Kontur.GameStats.Server.Modules
{
  public class GetStatisticModule : NancyModule
  {
    public GetStatisticModule()
    {
      Get["/servers/{endpoint}/stats"] = _ => HttpStatusCode.NotImplemented;
      Get["/players/{name}/stats"] = _ => HttpStatusCode.NotImplemented;
      Get["/reports/recent-matches/{count?50}"] = _ => HttpStatusCode.NotImplemented; //TODO значение по умолчанию перенести
      Get["/reports/best-players/{count?50}"] = _ => HttpStatusCode.NotImplemented;
      Get["/reports/popular-servers/{count?50}"] = _ => HttpStatusCode.NotImplemented;

    }
  }
}