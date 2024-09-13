using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDService.Database.Models.DbModels.ContractSchedule
{
    /// <summary> Табличка с историей изменения задач на вывоз </summary>
    [Table("contract_schedule__mission_history"), Comment("Табличка с историей изменения задач на вывоз")]
    public class ContractScheduleMissionHistory : BaseHistoryTableModel
    {
        /// <summary> Ид договора </summary>
        [Column("contract_id"), Comment("Ид договора")]
        public int ContractId { get; set; }

        /// <summary>
        /// Ид контейнерной площадки
        /// </summary>
        [Column("stand_id"), Comment("Ид контейнерной площадки")]
        public int StandId { get; set; }

        /// <summary> Дата в которую необходимо выполнить вывоз </summary>
        [Column("mission_date"), Comment("Дата в которую необходимо выполнить вывоз")]
        public DateTime MissionDate { get; set; }

        /// <summary> Выполнена ли задача </summary>
        [Column("is_completed"), Comment("Выполнена ли задача")]
        public bool IsCompleted { get; set; }

        /// <summary> Является ли задача срочной true - срочная, false - сгенерированная автоматически </summary>
        [Column("is_urgent"), Comment("Является ли задача срочной true - срочная, false - сгенерированная автоматически")]
        public bool IsUrgent { get; set; }

        /// <summary> Была ли задача провалена (пропущен вызов) </summary>
        [Column("is_failed"), Comment("Была ли задача провалена (пропущен вызов)")]
        public bool IsFailed { get; set; }

        /// <summary> ИД пользователя создавшего запись </summary>
        [Column("created_user_id"), Comment("ИД пользователя создавшего запись")]
        public int CreatedUserId { get; set; }

        /// <summary> Имя пользователя создавшего запись </summary>
        [Column("created_user_name"), Comment("Имя пользователя создавшего запись"), MaxLength(512)]
        public string CreatedUserName { get; set; }
    }
}
