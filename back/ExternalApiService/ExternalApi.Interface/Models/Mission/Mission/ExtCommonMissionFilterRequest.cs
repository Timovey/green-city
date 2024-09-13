using ExternalApi.Interface.Models.Mission.App;
using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Mission.Mission
{
    public class ExtCommonMissionFilterRequest : CommonHttpFilter
    {
        public DateTime Date { get; set; }

        public IList<int> ContractClientIds { get; set; }

        public IList<int> ContractIds { get; set; }

        public IList<ExtMissionStatus> Statuses { get; set; }

        public IList<int> StandIds { get; set; }

        public IList<int> ContainerIds { get; set; }
    }
}
