using GreenCity.Http;
using MissionService.Contracts.Enums;
using System;

namespace MissionService.Contracts.DataContracts.Mission
{
    public class CreateOneRealMissionContract : CommonHttpContract
    {
        /// <summary> ИД контейнера </summary>
        public int ContainerId { get; set; }

        /// <summary> Дата в которуюнеобходимо вывезти </summary>
        public DateTime Date { get; set; }

        /// <summary> Тип создателя миссии </summary>
        public CreatorType CreatorType { get; set; }
    }
}
