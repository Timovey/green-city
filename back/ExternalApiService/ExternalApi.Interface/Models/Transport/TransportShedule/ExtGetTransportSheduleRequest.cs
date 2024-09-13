using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    public class ExtGetTransportSheduleRequest : CommonHttpContract
    {
        public IList<int> TransportSheduleIds { get; set; }
    }
}
