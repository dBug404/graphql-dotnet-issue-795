using System.Collections.Generic;
using System.Linq;

namespace Graphql.Web.Backend
{
    public class DealerService
    {
        public IList<Dealer> GetDealers(IList<string> dealerIdList)
        {
            return dealerIdList
                .Select(dealerId => new Dealer
                {
                    DealerId = dealerId,
                    DealerName = "Name " + dealerId
                })
                .ToList();
        }

        public Dealer GetDealer(string dealerId)
        {
            return new Dealer
            {
                DealerId = dealerId,
                DealerName = "Name " + dealerId
            };
        }
    }
}