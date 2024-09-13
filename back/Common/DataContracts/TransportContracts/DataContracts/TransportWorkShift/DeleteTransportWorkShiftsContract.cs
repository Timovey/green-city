using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.TransportWorkShift
{
    public class DeleteTransportWorkShiftsContract : CommonHttpContract
    {
        public IList<int> TransportWorkShiftsIds { get; set; }
    }
}
