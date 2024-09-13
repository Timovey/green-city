using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Models.Db
{
    /// <summary> Организация </summary>
    [Table("organization"), Comment("Организация")]
    internal class DbOrganization : BaseDbModel
    {
        /// <summary> Название </summary>
        [Column("name"), Comment("Название"), Required, MaxLength(255)]
        public string Name { get; set; }

        /// <summary> Адрес </summary>
        [Column("address"), Comment("Адрес"), Required, MaxLength(512)]
        public string Address { get; set; }

        /// <summary> Телефон </summary>
        [Column("phone"), Comment("Телефон"), Required, MaxLength(50)]
        public string Phone { get; set; }

        /// <summary> ОГРН </summary>
        [Column("ogrn"), Comment("ОГРН"), Required, MaxLength(50)]
        public string Ogrn { get; set; }
    }
}
