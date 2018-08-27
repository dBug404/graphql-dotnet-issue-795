using Graphql.Web.Backend;
using GraphQL.Types;

namespace Graphql.Web.Schema
{
    public class OrdersType : ObjectGraphType<object>
    {
        public OrdersType(OrderService orderService)
        {
            Field<ListGraphType<OrderGraphType>>("all", resolve: c => orderService.GetAll());
            Field<DebugType>("debug", resolve: c => new DebugType());
        }
    }
}