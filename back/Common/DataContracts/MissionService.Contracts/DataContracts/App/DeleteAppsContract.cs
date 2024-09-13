using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using MissionService.Contracts.Enums;
using System.Collections.Generic;

namespace MissionService.Contracts.DataContracts.App
{
    public class DeleteAppsContract : CommonHttpContract
    {
        [GCRequired]
        public IList<int> AppIds { get; set; }

        public AppActionsPermission DeletePermission { get; set; }
    }
}
