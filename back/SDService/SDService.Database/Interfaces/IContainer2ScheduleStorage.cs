using SDContracts.Enums;
using SDContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Database.Interfaces
{
    public interface IContainer2ScheduleStorage
    {
        Task<IList<Container2ScheduleForMissionsViewModel>> GetFilteredContainer2ScheduleForMissionsAsync(
             DateTime? date = null,
             IList<int> contractClientIds = default,
             IList<int> contractIds = default,
             MissionInfoEntityType? entityType = MissionInfoEntityType.Real
        );
    }
}
