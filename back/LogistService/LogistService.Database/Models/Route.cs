using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace LogistService.Database.Models
{
    /// <summary> Рейс </summary>
    [Table("route"), Comment("Рейс")]
    public class Route : BaseDbModel
    {
        /// <summary> Название рейса </summary>
        [Column("route_name"), Comment("Название рейса"), Required, MaxLength(512)]
        public string RouteName { get; set; }

        /// <summary> Порядковый номер рейса на дню </summary>
        [Column("route_number_on_day "), Comment("Порядковый номер рейса на дню"), Required]
        public int RouteNumberOnDay { get; set; }

        /// <summary> Дата рейса </summary>
        [Column("date"), Comment("Дата рейса"), Required]
        public DateTime Date { get; set; }

        /// <summary> Готовность рейса </summary>
        [Column("route_ready"), Comment("Готовность рейса"), Required]
        public bool RouteReady { get; set; }

        /// <summary> Завершенность рейса </summary>
        [Column("route_сompleted"), Comment("Завершенность рейса"), Required]
        public bool RouteCompleted { get; set; }

        /// <summary> Место выгрузки </summary>
        [Column("route_finish_place"), Comment("Место выгрузки"), MaxLength(512)]
        public string RouteFinishPlace { get; set; }

        /// <summary> Объем выгрузки </summary>
        [Column("route_total_volume_on_finish"), Comment("Объем выгрузки")]
        public decimal RouteTotalVolumeOnFinish { get; set; }

        /// <summary> ИД экипажа </summary>
        [Column("transport_work_shift_id"), Comment("ИД экипажа"), Required]
        public int TransportWorkShiftId { get; set; }
    }
}