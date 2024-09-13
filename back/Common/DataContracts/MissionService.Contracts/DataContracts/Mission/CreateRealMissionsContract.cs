using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using MissionService.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace MissionService.Contracts.DataContracts.Mission
{
    public class CreateRealMissionsContract : CommonHttpContract
    {
        /// <summary> Идентификаторы контейнеров </summary>
        [GCRequired("Идентификаторы контейнеров")]
        public IList<int> ContainerIds { get; set; }

        /// <summary> Комментарий к вывозу </summary>
        public string MissionComment { get; set; }

        /// <summary> Дата в которуюнеобходимо вывезти </summary>
        public DateTime Date { get; set; }

        /// <summary> Тип создателя миссии </summary>
        public CreatorType CreatorType { get; set; }

        public AppActionsPermission CreatePermission { get; set; }
    }
}
