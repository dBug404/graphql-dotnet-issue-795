using GraphQL;

namespace Graphql.Web.Schema
{
    public class TestSchema : GraphQL.Types.Schema
    {
        public TestSchema(IDependencyResolver resolver) 
            : base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
        }
    }
}