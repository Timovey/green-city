using ExternalApi.Interface.Models.Sd.Contract.ContractScheduleViewModels;
using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Contract
{
    /// <summary> Пара ИД контейнера - расписание для контрактов </summary>
    public class ExtContainer2ScheduleViewModel
    {
        /// <summary> Расписание </summary>
        public IList<ExtContractScheduleCollectionViewModel> ContractScheduleCollection { get; set; }

        /// <summary> Крон - расписание </summary>
        public string ScheduleComment { get; set; }

        /// <summary> С какого времени можно вывести мусор </summary>
        public string WorkTimeFrom { get; set; }

        /// <summary> До какого времени можно вывести мусор </summary>
        public string WorkTimeTo { get; set; }

        /// <summary> Флаг нахождения контейнера в гос реестре контейнеров </summary>
        public bool IsInRegistry { get; set; }

        /// <summary> С какой даты контейнер находится в гос реестре </summary>
        public DateTime? InRegistryFromDate { get; set; }
    }
}
