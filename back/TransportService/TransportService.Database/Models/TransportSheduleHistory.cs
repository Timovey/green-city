using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Историческая таблица для таблицы типовых графиков</summary>
    [Table("transport_shedule_history"), Comment("Историческая таблица для таблицы типовых графиков")]
    public class TransportSheduleHistory : DateRangeHistory
    {
        /// <summary>
        /// ID транспорта
        /// </summary>
        [Column("transport_id")]
        public int TransportId { get; set; }

        /// <summary>
        /// Типовой трафик
        /// </summary>
        [Column("work_shedule_type")]
        public byte WorkSheduleType { get; set; }
    }
}
