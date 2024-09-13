using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.TransportDutyShift
{
    public class ExtDeleteTransportDutyShiftRequest : CommonHttpContract
    {
        public IList<int> TransportDutyShiftIds { get; set; }
    }
}
