using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MissionService.Contracts.DataContracts.Mission
{
    public class UpdateMissionsForCloseRouteContract : CommonHttpContract, IValidatableObject
    {
        [GCRequired]
        public IList<UpdateOneMissionForCloseRouteContract> UpdateContracts { get; set; }

        /// <summary> ИД рейса </summary>
        public int RouteId { get; set; }

        /// <summary> Место выгрузки </summary>
        [GCMaxLength(512, "Место выгрузки")]
        public string RouteFinishPlace { get; set; }

        /// <summary> Объем выгрузки </summary>
        public decimal RouteTotalVolumeOnFinish { get; set; }

        /// <summary> Необходимость закрытия рейса </summary>
        public bool NeedCloseRoute { get; set; } = true;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (RouteTotalVolumeOnFinish < 0)
                yield return new ValidationResult("Общий объем не может быть меньше 0");
        }
    }
}
