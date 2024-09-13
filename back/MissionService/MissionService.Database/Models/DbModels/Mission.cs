using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionService.Database.Models.DbModels
{
    /// <summary> Миссии - заявки на вывоз </summary>
    [Table("mission"), Comment("Миссии - заявки на вывоз")]
    public class Mission : BaseDbModel
    {
        /// <summary> ИД контейнера </summary>
        [Column("container_id"), Comment("ИД контейнера")]
        public int ContainerId { get; set; }

        /// <summary> Дата в которуюнеобходимо вывезти </summary>
        [Column("date"), Comment("Дата в которую необходимо вывезти")]
        public DateTime Date { get; set; }

        /// <summary> ИД рейса </summary>
        [Column("route_id"), Comment("ИД рейса")]
        public int? RouteId { get; set; }

        /// <summary> Приоритет вывоза </summary>
        [Column("priority"), Comment("Приоритет вывоза")]
        public int Priority { get; set; }

        /// <summary> Статус миссии </summary>
        [Column("status"), Comment("Статус миссии")]
        public byte Status { get; set; }

        /// <summary> Комментарий к статусу </summary>
        [Column("status_comment"), Comment("Комментарий к статусу")]
        public string StatusComment { get; set; }

        /// <summary> Фактически вывезенный объем </summary>
        [Column("fact_volume"), Comment("Фактически вывезенный объем")]
        public decimal? FactVolume { get; set; }
    }
}
