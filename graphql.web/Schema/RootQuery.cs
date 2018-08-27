using GraphQL.Types;
using StructureMap;

namespace Graphql.Web.Schema
{
    public class RootQuery : ObjectGraphType<object>
    {
        public RootQuery(IContainer container)
        {
            Field<OrdersType>("orders", resolve: c => container.GetInstance<OrdersType>());;
            Field<DebugType>("debug", resolve: c => new DebugType());
        }
    }
}