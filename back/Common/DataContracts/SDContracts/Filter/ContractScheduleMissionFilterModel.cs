using GreenCity.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDContracts.Filter
{
    public class ContractScheduleMissionFilterModel : CommonHttpFilter, IValidatableObject
    {
        /// <summary> Идентификаторы договоров </summary>
        public IList<int> ContractIds { get; set; }

        /// <summary>
        /// Идентификаторы контейнерных площадок
        /// </summary>
        public IList<int> StandIds { get; set; }

        /// <summary> Сериализованные идентификаторы контейнеров </summary>
        public IList<int> ContainerIds { get; set; }

        /// <summary> Дата в которую необходимо выполнить вывоз c </summary>
        public DateTime? MissionDateFrom { get; set; }

        /// <summary> Дата в которую необходимо выполнить вывоз c </summary>
        public DateTime? MissionDateTo { get; set; }

        /// <summary> Выполнена ли задача </summary>
        public bool? IsCompleted { get; set; }

        /// <summary> Является ли задача срочной true - срочная, false - сгенерированная автоматически </summary>
        public bool? IsUrgent { get; set; }
        
        /// <summary> Была ли задача провалена </summary>
        public bool? IsFailed { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MissionDateFrom.HasValue && MissionDateTo.HasValue && MissionDateFrom.Value > MissionDateTo.Value)
            {
                yield return new ValidationResult("Дата задачи С должна быть меньше даты задачи ПО");
            }
        }
    }
}
