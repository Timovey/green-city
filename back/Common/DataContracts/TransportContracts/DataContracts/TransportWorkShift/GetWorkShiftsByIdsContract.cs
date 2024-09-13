using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.TransportWorkShift
{
    public class GetWorkShiftsByIdsContract : CommonHttpContract
    {
        public IList<int> WorkShiftIds { get; set; }

        public bool IsFilterByUser { get; set; }
    }
}
