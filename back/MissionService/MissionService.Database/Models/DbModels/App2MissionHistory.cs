using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionService.Database.Models.DbModels
{
    /// <summary> Историческая таблица связок заявок и мисиий </summary>
    [Table("app2mission_history"), Comment("Историческая таблица связок заявок и мисиий")]
    public class App2MissionHistory : BaseHistoryTableModel
    {
        /// <summary> ИД заявки </summary>
        [Column("app_id"), Comment("ИД заявки")]
        public int AppId { get; set; }

        /// <summary> ИД миссии </summary>
        [Column("mission_id"), Comment("ИД миссии")]
        public int MissionId { get; set; }
    }
}
