using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherService.Database.Models
{
    /// <summary> Связка бригады с рейсом </summary>
    [Table("brigade_2_route"), Comment("Связка бригады с рейсом")]
    public class Brigade2Route : BaseDbModel
    {
        /// <summary> ИД бригады </summary>
        [Column("brigade_id"), Comment("ИД бригады")]
        public int BrigadeId { get; set; }

        /// <summary> ИД рейса </summary>
        [Column("route_id"), Comment("ИД рейса")]
        public int RouteId { get; set; }
    }
}
