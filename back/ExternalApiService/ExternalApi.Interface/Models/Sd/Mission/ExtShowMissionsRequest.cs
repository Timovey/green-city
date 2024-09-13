using GreenCity.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Sd.Mission
{
    public class ExtShowMissionsRequest : CommonHttpContract, IValidatableObject
    {
        /// <summary> С какой даты создавать задания на вывоз </summary>
        public DateTime DateFrom { get; set; }

        /// <summary> По какую дату создавать задание на вывоз (если не указано, то только на одну дату - дату начала) </summary>
        public DateTime? DateTo { get; set; }

        /// <summary> Фильтр только по определенным договорам </summary>
        public IList<int> ContractIds { get; set; }

        /// <summary> Показывать ли те которые уже созданы </summary>
        public bool IncludeNotCompleted { get; set; } = false;

        /// <summary> Фильтровать ли по ид пользователя создателя договоры </summary>
        public bool IsFilterByUserId { get; set; } = true;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTo.HasValue && DateTo < DateFrom)
            {
                yield return new ValidationResult("Дата окончания должна быть больше или равна дате начала");
            }
        }
    }
}
