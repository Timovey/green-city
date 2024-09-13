using GreenCity.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDContracts.DataContracts.Contract.ScheduleModels.Mission
{
    /// <summary> Запрос на создание задания на вывоз </summary>
    public class ShowMissionsContract : CommonHttpContract, IValidatableObject
    {
        /// <summary> С какой даты создавать задания на вывоз </summary>
        public DateTime DateFrom { get; set; }

        /// <summary> По какую дату создавать задание на вывоз (если не указано, то только на одну дату - дату начала) </summary>
        public DateTime? DateTo { get; set; }

        /// <summary> Фильтр только по определенным договорам </summary>
        public IList<int> ContractIds { get; set; }

        /// <summary> Показывать ли те которые уже созданы </summary>
        public bool IncludeNotCompleted { get; set; }

        /// <summary> Фильтровать ли по ид пользователя создателя договоры </summary>
        public bool IsFilterByUserId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTo.HasValue)
            {
                if (DateTo < DateFrom)
                {
                    yield return new ValidationResult("Дата окончания должна быть больше или равна дате начала");
                }

                if (ContractIds == default || ContractIds.Count > 10 && (DateTo.Value - DateFrom).Days > 10)
                {
                    yield return new ValidationResult("Если задаете диапазон больше 10 дней, то выбирать можно не более 10 графиков");
                }
            }
        }
    }
}
