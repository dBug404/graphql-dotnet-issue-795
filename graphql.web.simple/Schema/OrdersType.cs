using Graphql.Web.Backend;
using GraphQL.Types;

namespace Graphql.Web.Schema
{
    public class OrdersType : ObjectGraphType<object>
    {
        public OrdersType()
        {
            Field<ListGraphType<OrderGraphType>>("all", resolve: c => new OrderService().GetAll());
            Field<DebugType>("debug", resolve: c => new DebugType());
        }
    }
}