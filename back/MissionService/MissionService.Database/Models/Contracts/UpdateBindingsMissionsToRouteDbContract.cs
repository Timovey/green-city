using System;
using System.Collections.Generic;

namespace MissionService.Database.Models.Contracts
{
    public class UpdateBindingsMissionsToRouteDbContract
    {
        public int RouteId { get; set; }

        public DateTime Date { get; set; }

        public IList<(int Priority, IList<int> ContainerIds)> UpdateDatas { get; set; }
    }
}
