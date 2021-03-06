using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Graphql.Web.IoC
{
    /// <summary>
    /// Klasse die StructureMap als DependencsResolver verwendet. Wird in GlobalConfiguration entsprechend gesetzt
    /// </summary>
    public class StructureMapDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }

            var container = StructureMapContainerPerRequestModule.Container;

            return serviceType.IsAbstract || serviceType.IsInterface
                ? container.TryGetInstance(serviceType)
                : container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return StructureMapContainerPerRequestModule.Container.GetAllInstances(serviceType).Cast<object>();
        }

        //For WebAPI
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            //Nothing to do here.  It's handled by the HTTP Module.
        }
    }
}