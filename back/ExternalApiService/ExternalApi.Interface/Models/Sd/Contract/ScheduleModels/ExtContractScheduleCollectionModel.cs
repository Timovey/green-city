using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Sd.Contract.ScheduleModels
{
    /// <summary>
    /// Объект коллекции расписаний договоров
    /// </summary>
    public class ExtContractScheduleCollectionModel : IValidatableObject
    {
        /// <summary>
        /// Расписание договора
        /// </summary>
        public ExtContractScheduleModel ContractSchedule { get; set; }

        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата действия начала действия расписания
        /// </summary>
        public DateTime? ScheduleDateFrom { get; set; }

        /// <summary>
        /// Дата окончания действия расписания
        /// </summary>
        public DateTime? ScheduleDateTo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(ScheduleDateFrom.HasValue && ScheduleDateTo.HasValue)
            {
                if (ScheduleDateFrom.Value.Date > ScheduleDateTo.Value.Date)
                    yield return new ValidationResult("Начало временного промежутка не может быть больше его окончания");
            }
        }
    }
}
