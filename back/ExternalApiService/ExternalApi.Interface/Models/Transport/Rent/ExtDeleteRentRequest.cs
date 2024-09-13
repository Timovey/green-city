using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.Rent
{
    public class ExtDeleteRentRequest : CommonHttpContract
    {
        public IList<int> RentIds { get; set; }
    }
}
