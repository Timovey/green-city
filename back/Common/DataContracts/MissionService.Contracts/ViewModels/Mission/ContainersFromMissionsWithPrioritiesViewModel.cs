using System.Collections.Generic;

namespace MissionService.Contracts.ViewModels.Mission
{
    public class ContainersFromMissionsWithPrioritiesViewModel
    {
        public int Priority { get; set; }

        public IList<int> ContainerIds { get; set; }
    }
}
