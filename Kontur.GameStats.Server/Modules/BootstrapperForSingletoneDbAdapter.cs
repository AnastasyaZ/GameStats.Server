using Kontur.GameStats.Server.Database;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Kontur.GameStats.Server.Modules
{
  public class BootstrapperForSingletoneDbAdapter:DefaultNancyBootstrapper
  {
    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
      base.ApplicationStartup(container, pipelines);
      container.Register<IDatabaseAdapter, LiteDbAdapter>().AsSingleton();
    }
  }
}