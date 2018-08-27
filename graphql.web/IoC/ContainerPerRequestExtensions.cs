using System.Web;
using StructureMap;

namespace Graphql.Web.IoC
{
    public static class ContainerPerRequestExtensions
    {
        private const string CONTAINER_KEY = "_Container";

        public static IContainer GetContainer(this HttpContextBase context)
        {
            return (IContainer)context.Items[CONTAINER_KEY] ?? IoC.Container;
        }

        public static void SetContainer(this HttpContextBase context, IContainer container)
        {
            context.Items[CONTAINER_KEY] = container;
        }
    }
}