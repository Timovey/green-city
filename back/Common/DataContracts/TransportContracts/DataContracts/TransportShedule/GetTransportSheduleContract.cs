using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.TransportShedule
{
    public class GetTransportSheduleContract : CommonHttpContract
    {
        public IList<int> TransportSheduleIds { get; set; }
    }
}
