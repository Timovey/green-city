using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Аренда транспорта </summary>
    [Table("rent"), Comment("Аренда транспорта")]
    public class Rent : DateRange
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
