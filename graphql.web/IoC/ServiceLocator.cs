using StructureMap;

namespace Graphql.Web.IoC
{
    public static class ServiceLocator
    {
        static ServiceLocator()
        {
            Container = new Container();
        }

        public static IContainer Container { get; }
    }
}