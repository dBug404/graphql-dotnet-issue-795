using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graphql.Web.Backend;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace Graphql.Web.Schema
{
    public class OrderGraphType : ObjectGraphType<Order>
    {
        public OrderGraphType(IDataLoaderContextAccessor accessor, DealerService dealerService)
        {
            Field(x => x.Id);
            Field(x => x.DealerId);
            Field(x => x.Number);
            Field<DebugType>("debug", resolve: c => new DebugType());


            Field<DealerGraphType>("dealer", resolve:
                context =>
                {
                    //return dealerService.GetDealer(context.Source.DealerId);
                    var loader = accessor.Context.GetOrAddBatchLoader<string, Dealer>("getDealerByDealerId", dealerIds =>
                    {
                        IDictionary<string, Dealer> dictionary = dealerService
                            .GetDealers(dealerIds.ToList())
                            .ToDictionary(dealer => dealer.DealerId);

                        return Task.FromResult(dictionary);
                    });
                    return loader.LoadAsync(context.Source.DealerId);
                });
        }
    }
}