using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.TransportDutyShift
{
    public class ExtGetTransportDutyShiftRequest : CommonHttpContract
    {
        public IList<int> TransportDutyShiftIds { get; set; }
    }
}
