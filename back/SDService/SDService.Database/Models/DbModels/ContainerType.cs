using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDService.Database.Models.DbModels
{
    /// <summary>
    /// Тип тары
    /// </summary>
    [Table("container_type"), Comment("Тип тары")]
    public class ContainerType : BaseDbModel
    {
        /// <summary>
        /// Название типа тары
        /// </summary>
        [Column("name"), Comment("Название типа тары")]
        public string Name { get; set; }

        /// <summary>
        /// Объем тары
        /// </summary>
        [Column("volume"), Comment("Объем тары")]
        public decimal Volume { get; set; }

        /// <summary> Тип сущности - доступная или недоступная для отображения пользователю </summary>
        [Column("entity_type"), Comment("Тип сущности - доступная или недоступная для отображения пользователю")]
        public byte EntityType { get; set; }
    }
}
