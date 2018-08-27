using System;
using GraphQL;
using StructureMap;

namespace Graphql.Web.Controllers
{
    public class StructureMapGraphQlDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public StructureMapGraphQlDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public T Resolve<T>()
        {
            return _container.GetInstance<T>();
        }

        public object Resolve(Type type)
        {
            return _container.GetInstance(type);
        }
    }
}