using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDService.Database.Models.DbModels.ContractSchedule
{
    /// <summary> Таблица-связка задания на вывоз с контейнерами </summary>
    [Table("contract_schedule__mission_2_container"), Comment("Таблица-связка задания на вывоз с контейнерами")]
    public class Mission2Container : BaseDbModel
    {
        /// <summary> ИД задания на вывоз </summary>
        [Column("mission_id"), Comment("ИД задания на вывоз")]
        public int MissionId { get; set; }

        /// <summary> ИД контейнера </summary>
        [Column("container_id"), Comment("ИД контейнера")]
        public int ContainerId { get; set; }
    }
}
