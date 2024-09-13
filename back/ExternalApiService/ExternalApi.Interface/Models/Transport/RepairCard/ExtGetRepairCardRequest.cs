using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.RepairCard
{
    public class ExtGetRepairCardRequest : CommonHttpContract
    {
        public IList<int> RepairCardIds { get; set; }
    }
}
