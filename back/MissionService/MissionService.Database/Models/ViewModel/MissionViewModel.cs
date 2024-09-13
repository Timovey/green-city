using MissionService.Contracts.Enums;
using System;

namespace MissionService.Database.Models.ViewModel
{
    public class MissionViewModel
    {
        /// <summary> ИД миссии </summary>
        public int Id { get; set; }

        /// <summary> ИД контейнера </summary>
        public int ContainerId { get; set; }

        /// <summary> Дата в которуюнеобходимо вывезти </summary>
        public DateTime Date { get; set; }

        /// <summary> Тип миссии </summary>
        public AppType Type { get; set; }

        /// <summary> Тип создателя миссии </summary>
        public CreatorType CreatorType { get; set; }

        /// <summary> ИД рейса </summary>
        public int? RouteId { get; set; }

        /// <summary> Приоритет вывоза </summary>
        public int Priority { get; set; }

        /// <summary> Комментарий к вывозу </summary>
        public string MissionComment { get; set; }

        /// <summary> Статус миссии </summary>
        public MissionStatus Status { get; set; }

        /// <summary> Комментарий к статусу </summary>
        public string StatusComment { get; set; }

        /// <summary> Фактически вывезенный объем </summary>
        public decimal? FactVolume { get; set; }

        /// <summary> ИД пользователя создателя </summary>
        public int CreatedUserId { get; set; }
    }
}
