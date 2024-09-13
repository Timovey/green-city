using GreenCity.Http;
using MissionService.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace MissionService.Contracts.DataContracts.App
{
    /// <summary>
    /// Обновление заявки на основе договоров (Ральной)
    /// </summary>
    public class UpdateAppByContractClient : CommonHttpContract
    {
        /// <summary> Идентификатор заявки </summary>
        public int AppId { get; set; }

        /// <summary> Дата </summary>
        public DateTime Date { get; set; }

        /// <summary> Идентификаторы контейнеров </summary>
        public IList<int> ContainerIds { get; set; }

        /// <summary> Тип пользователя изменяющего запись </summary>
        public CreatorType UpdaterType { get; set; }

        /// <summary> Права на изменение записи </summary>
        public AppActionsPermission UpdatePermission { get; set; }
    }
}
