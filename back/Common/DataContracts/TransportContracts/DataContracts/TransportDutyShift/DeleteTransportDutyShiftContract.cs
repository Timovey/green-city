using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.TransportDutyShift
{
    public class DeleteTransportDutyShiftContract : CommonHttpContract
    {
        public IList<int> TransportDutyShiftIds { get; set; }
    }
}
