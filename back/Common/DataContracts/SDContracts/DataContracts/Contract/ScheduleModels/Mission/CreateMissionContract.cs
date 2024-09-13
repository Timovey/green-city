using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace SDContracts.DataContracts.Contract.ScheduleModels.Mission
{
    public class CreateMissionContract : CommonHttpContract
    {
        /// <summary> Ид договора </summary>
        public int ContractId { get; set; }

        /// <summary>
        /// Ид контейнерной площадки
        /// </summary>
        public int StandId { get; set; }

        /// <summary> Дата в которую необходимо выполнить вывоз </summary>
        public DateTime MissionDate { get; set; }

        /// <summary> Выполнена ли задача </summary>
        public bool IsCompleted { get; set; }

        /// <summary> Является ли задача срочной true - срочная, false - сгенерированная автоматически </summary>
        public bool IsUrgent { get; set; }

        /// <summary> Была ли задача провалена (пропущен вызов) </summary>
        public bool IsFailed { get; set; }

        public IList<CreateContainerMissionModel> AttachedContainers { get; set; }
    }
}
