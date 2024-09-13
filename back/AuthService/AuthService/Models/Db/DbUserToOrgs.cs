using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Models.Db
{
    /// <summary>
    /// Таблица сопоставления пользователя с организациями
    /// </summary>
    [Table("user_to_org"), Comment("Таблица сопоставления пользователя с организациями")]
    public class DbUserToOrgs : BaseDbModel
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
