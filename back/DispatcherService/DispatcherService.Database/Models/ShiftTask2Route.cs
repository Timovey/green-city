using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherService.Database.Models
{
    /// <summary> Связка сменного задания с рейсом </summary>
    [Table("shift_task_2_route"), Comment("Связка сменного задания с рейсом")]
    public class ShiftTask2Route : BaseDbModel
    {
        /// <summary> ИД сменного задания </summary>
        [Column("shift_task_id"), Comment("ИД сменного задания")]
        public int ShiftTaskId { get; set; }

        /// <summary> ИД рейса </summary>
        [Column("route_id"), Comment("ИД рейса")]
        public int RouteId { get; set; }
    }
}
