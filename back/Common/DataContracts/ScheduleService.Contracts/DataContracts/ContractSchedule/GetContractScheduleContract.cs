using GreenCity.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScheduleService.Contracts.DataContracts.ContractSchedule
{
    public class GetContractScheduleContract : CommonHttpContract
    {
        public SSContractScheduleModel Schedule { get; set; }

        [Range(1, 100)]
        public int ExamplesCount { get; set; }

        public DateTime FromDate { get; set; } = DateTime.UtcNow;

        public DateTime? ToDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ToDate.HasValue)
            {
                if (FromDate >= ToDate.Value)
                    yield return new ValidationResult("Дата начала должна быть больше даты окончания");
            }
        }
    }
}
