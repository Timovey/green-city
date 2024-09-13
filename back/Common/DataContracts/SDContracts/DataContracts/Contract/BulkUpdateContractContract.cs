using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDContracts.DataContracts.Contract
{
    /// <summary> Массовое обновление контрактов </summary>
    public class BulkUpdateContractContract : CommonHttpContract, IValidatableObject
    {
        /// <summary> ИД договора </summary>
        [GCRequired]
        public IList<int> ContractIds { get; set; }

        /// <summary> Дата начала действия договора </summary>
        public DateTime? StartDate { get; set; }

        /// <summary> Срок договора </summary>
        public DateTime? EndDate { get; set; }

        public bool? ContractStatus { get; set; }

        public bool? IsValidated { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate.HasValue && StartDate.HasValue && EndDate.Value <= StartDate.Value)
            {
                yield return new ValidationResult("Дата начала окончания договора должна быть больше даты начала");
            }
        }
    }
}
