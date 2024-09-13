using SDContracts.DataContracts.Contract.ScheduleModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDContracts.DataContracts.Contract
{
    public class CreateContainer2ScheduleContract : IValidatableObject
    {
        /// <summary> ИД контейнера </summary>
        public int ContainerId { get; set; }

        /// <summary> расписание </summary>
        public IList<ContractScheduleCollectionModel> ContractScheduleCollection { get; set; }

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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (WorkTimeFrom > WorkTimeTo)
                yield return new ValidationResult("Время начала работы должно быть больше времени окончания");
        }
    }
}
