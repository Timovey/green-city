using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogistService.Database.Models
{
    /// <summary> Историческая таблица для таблицы base_route2stand </summary>
    [Table("base_route2stand_history"), Comment("Историческая таблица для таблицы base_route2stand")]
    public class BaseRoute2StandHistory : BaseHistoryTableModel
    {
        /// <summary> ИД базового маршрута </summary>
        [Column("base_route_id"), Comment("ИД базового маршрута"), Required]
        public int BaseRouteId { get; set; }

        /// <summary> Приоритет </summary>
        [Column("priority"), Comment("Приоритет"), Required]
        public int Priority { get; set; }

        /// <summary> ИД контейнерной площадки </summary>
        [Column("stand_id"), Comment("ИД контейнерной площадки"), Required]
        public int StandId { get; set; }
    }
}
