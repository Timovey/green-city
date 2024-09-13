using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Историческая таблица для отпуска/больничного водителя </summary>
    [Table("driver_vacation_history"), Comment("Историческая таблица для отпуска/больничного водителя")]
    public class DriverVacationHistory : DateRangeHistory
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
