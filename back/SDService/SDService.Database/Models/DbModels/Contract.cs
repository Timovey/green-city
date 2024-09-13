using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDService.Database.Models.DbModels
{
    /// <summary> Договор с контрагентом </summary>
    [Table("contract"), Comment("Договор с контрагентом")]
    public class Contract : BaseDbModel
    {
        /// <summary> Номер договора </summary>
        [Column("contract_number"), Required, MaxLength(100)]
        public string ContractNumber { get; set; }

        /// <summary> ИД контрагента </summary>
        [Column("contract_client_id")]
        public int? ContractClientId { get; set; }

        /// <summary> Статус подписания договора </summary>
        [Column("contract_status"), Comment("Статус подписания договора")]
        public bool ContractStatus { get; set; }

        /// <summary> Дата создания договора </summary>
        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        /// <summary> Дата начала действия договора </summary>
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        /// <summary> Срок договора </summary>
        [Column("end_date")]
        public DateTime? EndDate { get; set; }

        /// <summary> Способ отправки документов </summary>
        [Column("send_docs_type"), MaxLength(300)]
        public string SendDocsType { get; set; }

        /// <summary> Комментарий к договору </summary>
        [Column("comment"), MaxLength(2048), Comment("Комментарий к договору")]
        public string Comment { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        [Column("is_validated"), Comment("Флаг проверки записи сотрудником")]
        public bool IsValidated { get; set; }

        /// <summary> Тип сущности - доступная или недоступная для отображения пользователю </summary>
        [Column("entity_type"), Comment("Тип сущности - доступная или недоступная для отображения пользователю")]
        public byte EntityType { get; set; }

        /// <summary> ИД пользователя в базе AuthService создавшего запись </summary>
        [Column("created_user_id"), Comment("ИД пользователя в базе AuthService создавшего запись")]
        public int CreatedUserId { get; set; }

        /// <summary> Имя пользователя создавшего запись </summary>
        [Column("created_user_name"), Comment("Имя пользователя создавшего запись"), MaxLength(500)]
        public string CreatedUserName { get; set; }

        /// <summary> Включить ли автопродление графика </summary>
        [Column("is_autorenew_schedules"), Comment("Включить ли автопродление графика")]
        public bool IsAutorenewSchedules { get; set; } = false;
    }
}
