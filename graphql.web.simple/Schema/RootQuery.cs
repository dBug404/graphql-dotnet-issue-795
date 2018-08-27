using GraphQL.Types;
using StructureMap;

namespace Graphql.Web.Schema
{
    public class RootQuery : ObjectGraphType<object>
    {
        public RootQuery()
        {
            Field<OrdersType>("orders", resolve: c => new OrdersType());;
            Field<DebugType>("debug", resolve: c => new DebugType());
        }
    }
}