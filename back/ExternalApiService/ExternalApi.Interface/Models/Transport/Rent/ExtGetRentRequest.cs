using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.Rent
{
    public class ExtGetRentRequest : CommonHttpContract
    {
        public IList<int> RentIds { get; set; }
    }
}
