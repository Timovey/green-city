using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System.Collections.Generic;

namespace SDContracts.DataContracts.MissionInfo
{
    public class GetContractsWithContractClientsShortInfoContract : CommonHttpContract
    {
        [GCRequired]
        public IList<int> ContractClientIds { get; set; }

        [GCRequired]
        public IList<int> ContractIds { get; set; }
    }
}
