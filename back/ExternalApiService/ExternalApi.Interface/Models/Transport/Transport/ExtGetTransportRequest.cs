using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.Transport
{
    public class ExtGetTransportRequest : CommonHttpContract
    {
        public IList<int> TransportIds { get; set; }
    }
}
