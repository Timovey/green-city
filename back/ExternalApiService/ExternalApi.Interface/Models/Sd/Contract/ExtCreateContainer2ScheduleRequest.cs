using ExternalApi.Interface.Models.Sd.Contract.ScheduleModels;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Contract
{
    public class ExtCreateContainer2ScheduleRequest
    {
        /// <summary> ИД контейнера </summary>
        public int ContainerId { get; set; }

        /// <summary> Расписание по контейнеру </summary>
        public IList<ExtContractScheduleCollectionModel> ContractScheduleCollection { get; set; }

        /// <summary> Крон - расписание </summary>
        public string ScheduleComment { get; set; }

        /// <summary> С какого времени можно вывести мусор </summary>
        public string WorkTimeFrom { get; set; }

        /// <summary> До какого времени можно вывести мусор </summary>
        public string WorkTimeTo { get; set; }

        /// <summary> Флаг нахождения контейнера в гос реестре контейнеров </summary>
        public bool IsInRegistry { get; set; }

        /// <summary> С какой даты контейнер находится в гос реестре </summary>
        public string InRegistryFromDate { get; set; }
    }
}
