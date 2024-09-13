using MissionService.Contracts.Enums;
using System;

namespace MissionService.Database.Models.Contracts
{
    public class CreateOneMissionFromMapDbContract
    {
        public int ContainerId { get; set; }

        /// <summary> Дата в которуюнеобходимо вывезти </summary>
        public DateTime Date { get; set; }

        /// <summary> Тип создателя миссии </summary>
        public CreatorType CreatorType { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}
