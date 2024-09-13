using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogistService.Database.Models
{
    /// <summary> Базовый маршрут </summary>
    [Table("base_route"), Comment("Базовый маршрут")]
    public class BaseRoute : BaseDbModel
    {
        /// <summary> Название БМ </summary>
        [Column("name"), Comment("Название БМ"), Required, MaxLength(512)]
        public string Name { get; set; }

        /// <summary> ИД пользователя создателя </summary>
        [Column("created_user_id"), Comment("ИД пользователя создателя"), Required]
        public int CreatedUserId { get; set; }

        /// <summary> Имя пользователя создателя </summary>
        [Column("created_user_name"), Comment("Имя пользователя создателя"), MaxLength(500)]
        public string CreatedUserName { get; set; }
    }
}
