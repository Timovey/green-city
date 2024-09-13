using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System;
using System.Collections.Generic;

namespace MissionService.Contracts.DataContracts.Mission
{
    public class UpdateMissionBindingsByRouteIdContract : CommonHttpContract
    {
        /// <summary> ИД рейса </summary>
        public int RouteId { get; set; }

        /// <summary> Дата  </summary>
        public DateTime RouteDate { get; set; }

        /// <summary> Иденитификаторы Кп </summary>
        [GCRequired]
        public IList<int> StandIds { get; set; }

        /// <summary> Иденитификаторы типов контейнеров </summary>
        [GCRequired]
        public IList<int> ContainerTypeIds { get; set; }
    }
}
