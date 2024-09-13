using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.Rent
{
    public class GetRentContract : CommonHttpContract
    {
        public IList<int> RentIds { get; set; }
    }
}
