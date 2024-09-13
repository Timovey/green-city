using GreenCity.Http;
using MissionService.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace MissionService.Contracts.Filters.Mission
{
    public class CommonMissionFilterModel : CommonHttpFilter
    {
        public DateTime Date { get; set; }

        public IList<int> ContractClientIds { get; set; }

        public IList<int> ContractIds { get; set; }

        public IList<MissionStatus> Statuses { get; set; }

        public IList<int> StandIds { get; set; }

        public IList<int> ContainerIds { get; set; }
    }
}
