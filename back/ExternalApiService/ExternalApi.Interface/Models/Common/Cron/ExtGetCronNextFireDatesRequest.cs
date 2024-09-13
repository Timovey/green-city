using ExternalApi.Interface.Models.Sd.Contract.ScheduleModels;
using GreenCity.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Common.Cron
{
    public class ExtGetCronNextFireDatesRequest : CommonHttpContract, IValidatableObject
    {
        public ExtContractScheduleModel Schedule { get; set; }

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
