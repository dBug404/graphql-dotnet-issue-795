using System.Collections.Generic;
using System.Linq;

namespace Graphql.Web.Backend
{
    public class OrderService
    {
        public IList<Order> GetAll()
        {
            return Enumerable.Range(1, 20)
                .Select(i => new Order
                {
                    Id = i,
                    Number = "order-" + i,
                    DealerId = "dealer-" + i,
                })
                .ToList();
        }
    }
}