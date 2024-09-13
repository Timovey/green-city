using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using MissionService.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace MissionService.Contracts.DataContracts.Mission
{
    /// <summary>
    /// Получение идентификаторы контейнеров упоминающихся в миссиях
    /// </summary>
    public class GetAvailableContainersByMissionsForRouteContract : CommonHttpContract
    {
        /// <summary>
        /// Дата миссии
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Идентификаторы контейнеров
        /// </summary>
        [GCRequired]
        public IList<int> ContainerIds { get; set; }

        /// <summary>
        /// Статусы миссиий
        /// </summary>
        [GCRequired]
        public IList<MissionStatus> MissionStatuses { get; set; }

        /// <summary>
        /// ИД рейса для которого добавить миссии внезависимости от статуса (для редактирования рейсов нужно)
        /// </summary>
        public int? IncludeContainersRouteId { get; set; }
    }
}
