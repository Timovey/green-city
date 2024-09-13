using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.Rent
{
    public class DeleteRentContract : CommonHttpContract
    {
        public IList<int> RentIds { get; set; }
    }
}
