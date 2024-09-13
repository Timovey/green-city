using GreenCity.Http;
using SDContracts.Enums;
using System;
using System.Collections.Generic;

namespace SDContracts.Filter
{
    public class Container2ScheduleFilterModel : CommonHttpFilter
    {
        public DateTime? Date { get; set; }

        public IList<int> ContractClientIds { get; set; }

        public IList<int> ContractIds { get; set; }

        public MissionInfoEntityType? EntityType { get; set; }
    }
}
