using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using MissionService.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace MissionService.Contracts.DataContracts.App
{
    /// <summary> Создание заявки на основе договоров и КА </summary>
    public class CreateAppByContractClientContract : CommonHttpContract
    {
        /// <summary> Дата заявки </summary>
        public DateTime Date { get; set; }

        /// <summary> ИД контрагента </summary>
        public int ContractClientId { get; set; }

        /// <summary> ИД договора </summary>
        public int ContractId { get; set; }

        /// <summary> Тип создателя заявки </summary>
        public CreatorType CreatorType { get; set; }

        /// <summary> Комментарий </summary>
        [GCMaxLength(512)]
        public string Comment { get; set; }

        [GCRequired]
        public IList<int> ContainerIds { get; set; }

        /// <summary> Тип прав создателя заявки </summary>
        public AppActionsPermission CreatePermission { get; set; }
    }
}
