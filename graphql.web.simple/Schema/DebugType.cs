using System.Threading;
using System.Web;
using GraphQL.Types;

namespace Graphql.Web.Schema
{
    public class DebugType : ObjectGraphType<object>
    {
        public DebugType()
        {
            Field<BooleanGraphType>("httpContextIsNull", resolve: c => HttpContext.Current == null);
            Field<IntGraphType>("threadId", resolve: c => Thread.CurrentThread.ManagedThreadId);
        }
    }
}