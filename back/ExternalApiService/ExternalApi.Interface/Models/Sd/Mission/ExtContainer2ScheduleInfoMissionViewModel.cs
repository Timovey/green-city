using System;

namespace ExternalApi.Interface.Models.Sd.Mission
{
    public class ExtContainer2ScheduleInfoMissionViewModel
    {
        /// <summary> Комментарий к расписанию </summary>
        public string ScheduleComment { get; set; }

        /// <summary> С какого времени можно вывести мусор </summary>
        public TimeSpan WorkTimeFrom { get; set; }

        /// <summary> До какого времени можно вывести мусор </summary>
        public TimeSpan WorkTimeTo { get; set; }

        /// <summary> Флаг нахождения контейнера в гос реестре контейнеров </summary>
        public bool IsInRegistry { get; set; }

        /// <summary> С какой даты контейнер находится в гос реестре </summary>
        public DateTime? InRegistryFromDate { get; set; }
    }
}
