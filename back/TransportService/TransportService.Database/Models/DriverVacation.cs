using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Отпуск/больничный водителя </summary>
    [Table("driver_vacation"), Comment("Отпуск/больничный водителя")]
    public class DriverVacation : DateRange
    {
        /// <summary> ИД Водителя </summary>
        [Column("driver_id"), Comment("ИД водителя")]
        public int DriverId { get; set; }

        /// <summary> ИД сменного Водителя </summary>
        [Column("replacement_driver_id"), Comment("ИД сменного Водителя")]
        public int? ReplacementDriverId { get; set; }

        /// <summary> Отпуск/Больничный</summary>
        [Column("vacation_type"), Comment("Отпуск/Больничный")]
        public byte VacationType { get; set; }
    }
}
