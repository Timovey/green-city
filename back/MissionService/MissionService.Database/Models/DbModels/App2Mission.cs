using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionService.Database.Models.DbModels
{
    /// <summary> Связка заявки и миссии </summary>
    [Table("app2mission"), Comment("Связка заявки и миссии")]
    public class App2Mission : BaseDbModel
    {
        /// <summary> ИД заявки </summary>
        [Column("app_id"), Comment("ИД заявки")]
        public int AppId { get; set; }

        /// <summary> ИД миссии </summary>
        [Column("mission_id"), Comment("ИД миссии")]
        public int MissionId { get; set; }
    }
}
