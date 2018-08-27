using System.Web.Http;
using StructureMap;

namespace Graphql.Web.IoC
{
    public static class IoC
    {
        public static IContainer Container
        {
            get { return ServiceLocator.Container; }
        }

        public static void BuildUp(object target)
        {
            Container.BuildUp(target);
        }

        public static void Configure()
        {
            Container.Configure(cfg =>
            {
                cfg.AddRegistry<WebRegistry>();
            });
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver();
        }
    }
}
