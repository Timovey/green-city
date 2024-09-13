using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Models.Db
{
    /// <summary>
    /// Таблица изменений сопоставлений пользователя с организациями
    /// </summary>
    [Table("user_to_org_history"), Comment("Таблица изменений сопоставлений пользователя с организациями")]
    public class DbUserToOrgsHistory : BaseHistoryTableModel
    {
        /// <summary>
        /// ИД пользователя
        /// </summary>
        [Column("user_id"), Comment("ИД пользователя")]
        public int UserId { get; set; }

        /// <summary>
        /// Идентификаторы организаций
        /// </summary>
        [Required, Column("org_ids_json"), Comment("Идентификаторы организаций")]
        public string OrgIdsJson { get; set; }
    }
}
