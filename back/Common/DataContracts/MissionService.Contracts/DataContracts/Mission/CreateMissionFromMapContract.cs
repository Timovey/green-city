using GreenCity.Http;
using MissionService.Contracts.Enums;
using System;

namespace MissionService.Contracts.DataContracts.Mission
{
    public class CreateMissionFromMapContract : CommonHttpContract
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string CoordinatesComment { get; set; }

        public decimal CustomVolume { get; set; }

        /// <summary> Комментарий к вывозу </summary>
        public string MissionComment { get; set; }

        public int? ContractClientId { get; set; }

        public int? ContractId { get; set; }

        /// <summary> Дата в которуюнеобходимо вывезти </summary>
        public DateTime Date { get; set; }

        /// <summary> Тип создателя миссии </summary>
        public CreatorType CreatorType { get; set; }

        public AppActionsPermission CreatePermission { get; set; }
    }
}
