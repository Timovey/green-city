using GreenCity.Http.Attributes.Validation;
using MissionService.Contracts.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MissionService.Contracts.DataContracts.Mission
{
    public class UpdateOneMissionForCloseRouteContract : IValidatableObject
    {
        public int MissionId { get; set; }

        public MissionStatus MissionStatus { get; set; }

        [GCMaxLength(512)]
        public string StatusComment { get; set; }

        public decimal FactVolume { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MissionStatus == MissionStatus.Unknown ||
                MissionStatus == MissionStatus.InProgress)
                yield return new ValidationResult("Неверный статус миссии");

            if (FactVolume < 0)
                yield return new ValidationResult("Объем не может быть меньше 0");
        }
    }
}
