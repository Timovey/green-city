using SDContracts.DataContracts.Contract.ScheduleModels;
using System;
using System.Collections.Generic;

namespace SDService.Core.Extensions
{
    internal static class ContractScheduleExtensions
    {
        internal static IList<ContractScheduleCollectionModel> AddCreationDate(this IList<ContractScheduleCollectionModel> scheduleCollection)
        {
            if (scheduleCollection == default || scheduleCollection.Count < 1)
            {
                return null;
            }

            int tick = 0;
            foreach (var schedule in scheduleCollection)
            {
                schedule.CreateDate = DateTime.UtcNow.AddSeconds(tick++);
            }

            return scheduleCollection;
        }
    }
}
