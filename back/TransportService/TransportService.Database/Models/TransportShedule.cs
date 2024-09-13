using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Закрепление графика за машиной</summary>
    [Table("transport_shedule"), Comment("Типовой трафик")]
    public class TransportShedule : DateRange
    {
        /// <summary>
        /// ID транспорта
        /// </summary>
        [Column("transport_id"), Comment("ID транспорта")]
        public int TransportId { get; set; }

        /// <summary>
        /// Типовой трафик
        /// </summary>
        [Column("work_shedule_type"), Comment("Типовой трафик")]
        public byte WorkSheduleType { get; set; }
    }
}
