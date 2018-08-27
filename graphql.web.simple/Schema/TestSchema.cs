namespace Graphql.Web.Schema
{
    public class TestSchema : GraphQL.Types.Schema
    {
        public TestSchema() 
            : base()
        {
            Query = new RootQuery();
        }
    }
}