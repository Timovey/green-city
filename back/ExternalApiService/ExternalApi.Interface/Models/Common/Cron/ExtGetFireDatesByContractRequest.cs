using GreenCity.Http.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Common.Cron
{
    /// <summary>
    /// Запрос на вывод кп с контейнерами, которые необходимо вывезти по датам
    /// </summary>
    public class ExtGetFireDatesByContractRequest : IValidatableObject
    {
        /// <summary>
        /// Идентификаторы интересующих договоров
        /// </summary>
        [GCRequired]
        public IList<int> ContractIds { get; set; }

        /// <summary>
        /// Дата от которой необходимо сделать вывод
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Дата до которой интересует вывод
        /// </summary>
        public DateTime? DateTo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTo.HasValue)
            {
                if (DateFrom >= DateTo.Value)
                    yield return new ValidationResult("Дата начала должна быть больше даты окончания");

                if((DateTo.Value - DateFrom) > TimeSpan.FromDays(90))
                {
                    yield return new ValidationResult("Слишком большой диапазон, максимально возможный диапазон - 90 дней");
                }
            }
        }
    }
}
