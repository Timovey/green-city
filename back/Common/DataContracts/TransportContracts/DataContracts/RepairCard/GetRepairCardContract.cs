using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.RepairCard
{
    public class GetRepairCardContract : CommonHttpContract
    {
        public IList<int> RepairCardIds { get; set; }
    }
}
