using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace SDContracts.DataContracts.MissionInfo
{
    public class GetFilteredMissionInfoContract : CommonHttpContract
    {
        public IList<int> ContractClientIds { get; set; }

        public IList<int> StandIds { get; set; }

        public IList<int> ContainerIds { get; set; }

        public IList<int> ContainerTypeIds { get; set; }

        public IList<int> ContractIds { get; set; }

        public DateTime? Date { get; set; }
    }
}
