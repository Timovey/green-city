using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Таблица-связка для данных по закреплению водителя</summary>
    [Table("driver2transport")]
    public class Driver2Transport : DateRange
    {
        /// <summary> ИД водителя </summary>
        [Column("driver_id"), Comment("ИД Водителя")]
        public int DriverId { get; set; }

        /// <summary> ИД транспорта</summary>
        [Column("transport_id"), Comment("ИД Транспорта")]
        public int TransportId { get; set; }

        /// <summary>
        /// С какой смены выходит водитель 
        /// </summary>
        [Column("work_shift"), Comment("С какой смены выходит водитель")]
        public byte WorkShift { get; set; }
    }
}
