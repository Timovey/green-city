using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionService.Database.Models.DbModels
{
    /// <summary> Заявка на вывоз </summary>
    [Table("app"), Comment("Миссии - заявки на вывоз")]
    public class App : BaseDbModel
    {
        /// <summary> Дата заявки </summary>
        [Column("date"), Comment("Дата заявки")]
        public DateTime Date { get; set; }

        /// <summary> ИД контрагента </summary>
        [Column("contract_client_id"), Comment("ИД контрагента")]
        public int ContractClientId { get; set; }

        /// <summary> ИД договора </summary>
        [Column("contract_id"), Comment("ИД договора")]
        public int ContractId { get; set; }

        /// <summary> Тип заявки </summary>
        [Column("type"), Comment("Тип заявки")]
        public byte Type { get; set; }

        /// <summary> Тип создателя заявки </summary>
        [Column("creator_type"), Comment("Тип создателя заявки")]
        public byte CreatorType { get; set; }

        /// <summary> Комментарий </summary>
        [Column("comment"), Comment("Комментарий"), MaxLength(512)]
        public string Comment { get; set; }

        /// <summary> ИД пользователя создателя </summary>
        [Column("created_user_id"), Comment("ИД пользователя создателя")]
        public int CreatedUserId { get; set; }
    }
}
