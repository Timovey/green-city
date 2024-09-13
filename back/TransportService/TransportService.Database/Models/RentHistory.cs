using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Историческая таблица для таблицы rent </summary>
    [Table("rent_history"), Comment("Историческая таблица для таблицы rent")]
    public class RentHistory : DateRangeHistory
    {
        /// <summary>
        /// ID транспорта
        /// </summary>
        [Column("transport_id")]
        public int TransportId { get; set; }

        /// <summary>
        /// ИД организации
        /// </summary>
        [Column("organization_id")]
        public int OrganizationId { get; set; }
    }
}
