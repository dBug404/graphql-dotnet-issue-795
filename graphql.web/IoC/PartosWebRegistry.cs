using System.Collections;
using System.Reflection;
using System.Web;
using Graphql.Web.Controllers;
using Graphql.Web.Schema;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Execution;
using GraphQL.Http;
using GraphQL.Validation;
using GraphQL.Validation.Complexity;
using StructureMap;
using StructureMap.Pipeline;

namespace Graphql.Web.IoC
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            IncludeRegistry<GraphQLRegistry>();
        }
    }

    public class GraphQLRegistry : Registry
    {
        public GraphQLRegistry()
        {
            ForSingletonOf<IDocumentBuilder>().Use<GraphQLDocumentBuilder>();
            ForSingletonOf<IDocumentValidator>().Use<DocumentValidator>();
            ForSingletonOf<IDocumentExecuter>().Use<DocumentExecuter>();
            ForSingletonOf<IDocumentWriter>().Use<DocumentWriter>();
            For<IComplexityAnalyzer>().Use(() => new ComplexityAnalyzer(250));
            For<GraphQLQuery>().LifecycleIs<PartOSUserContextLifecycle>().Use<GraphQLQuery>();
            For<TestSchema>().LifecycleIs<PartOSUserContextLifecycle>().Use<TestSchema>();
            For<IDependencyResolver>().Use<StructureMapGraphQlDependencyResolver>();

            ForSingletonOf<IDataLoaderContextAccessor>().Use<DataLoaderContextAccessor>();
            ForSingletonOf<DataLoaderDocumentListener>().Use<DataLoaderDocumentListener>();
        }
    }

    public class PartOSUserContextLifecycle : LifecycleBase
    {
        public override void EjectAll(ILifecycleContext context)
        {
            this.FindCache(context).DisposeAndClear();
        }

        public override IObjectCache FindCache(ILifecycleContext context)
        {
            //var user = ServiceLocator.Container.GetInstance<IUserIdentity>();
            string user = "143928";
            var _itemName = $"STRUCTUREMAP-INSTANCES-{(object) Assembly.GetExecutingAssembly().GetName().Version}-{user}";
            IDictionary httpDictionary = this.findHttpDictionary();
            if (!httpDictionary.Contains(_itemName))
            {
                lock (httpDictionary.SyncRoot)
                {
                    if (!httpDictionary.Contains(_itemName))
                    {
                        LifecycleObjectCache lifecycleObjectCache = new LifecycleObjectCache();
                        httpDictionary.Add(_itemName, lifecycleObjectCache);
                        return (IObjectCache) lifecycleObjectCache;
                    }
                }
            }
            return (IObjectCache) httpDictionary[_itemName];
        }

        public static bool HasContext()
        {
            return HttpContext.Current != null;
        }

        protected virtual IDictionary findHttpDictionary()
        {
            if (!PartOSUserContextLifecycle.HasContext())
                throw new StructureMapException("You cannot use the HttpContextLifecycle outside of a web request. Try the HybridLifecycle instead.");
            return HttpContext.Current.Items;
        }
    }
}