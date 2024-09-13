using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using MissionService.Contracts.Enums;
using System;

namespace MissionService.Contracts.DataContracts.App
{
    /// <summary> Создание заявки с карты </summary>
    public class CreateAppFromMapContract : CommonHttpContract
    {
        /// <summary> Дата заявки </summary>
        public DateTime Date { get; set; }

        /// <summary> ИД контрагента </summary>
        public int? ContractClientId { get; set; }

        /// <summary> ИД договора </summary>
        public int? ContractId { get; set; }

        /// <summary> Тип создателя заявки </summary>
        public CreatorType CreatorType { get; set; }

        /// <summary> Комментарий </summary>
        [GCMaxLength(512)]
        public string Comment { get; set; }

        /// <summary> Долгота </summary>
        public decimal Longitude { get; set; }

        /// <summary> Широта </summary>
        public decimal Latitude { get; set; }

        /// <summary> Примерный объем </summary>
        public decimal CustomVolume { get; set; }

        /// <summary> Тип прав создателя заявки </summary>
        public AppActionsPermission CreatePermission { get; set; }
    }
}
