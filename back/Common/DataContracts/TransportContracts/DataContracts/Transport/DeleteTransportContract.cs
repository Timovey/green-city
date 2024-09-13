using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.Transport
{
    public class DeleteTransportContract : CommonHttpContract
    {
        public IList<int> TransportIds { get; set; }
    }
}
