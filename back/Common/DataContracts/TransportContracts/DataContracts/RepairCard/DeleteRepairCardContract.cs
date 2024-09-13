using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.RepairCard
{
    public class DeleteRepairCardContract : CommonHttpContract
    {
        public IList<int> RepairCardIds { get; set; }
    }
}
