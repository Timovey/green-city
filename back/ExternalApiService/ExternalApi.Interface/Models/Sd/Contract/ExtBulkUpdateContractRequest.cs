using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Sd.Contract
{
    public class ExtBulkUpdateContractRequest
    {
        /// <summary> ИД договора </summary>
        [Required]
        public IList<int> ContractIds { get; set; }

        /// <summary> Дата начала действия договора </summary>
        public DateTime? StartDate { get; set; }

        /// <summary> Срок договора </summary>
        public DateTime? EndDate { get; set; }

        /// <summary> Статус подписания договора </summary>
        public bool? ContractStatus { get; set; }

        public bool? IsValidated { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate.HasValue && StartDate.HasValue)
            {
                if (EndDate.Value <= StartDate.Value)
                {
                    yield return new ValidationResult("Дата начала окончания договора должна быть больше даты начала");
                }
            }
        }
    }
}
