using Graphql.Web.Backend;
using GraphQL.Types;

namespace Graphql.Web.Schema
{
    public class DealerGraphType : ObjectGraphType<Dealer>
    {
        public DealerGraphType()
        {
            Field(x => x.DealerId);
            Field(x => x.DealerName);
            Field<DebugType>("debug", resolve: c => new DebugType());
        }
    }
}