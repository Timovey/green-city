using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Историческая таблица для диапазонов дат</summary>
    [Table("data_range_history"), Comment("Историческая таблица для диапазонов дат")]
    public class DateRangeHistory : BaseHistoryTableModel
    {
        /// <summary> Дата создания сущности </summary>
        [Column("create_date"), Comment("Дата создания сущности")]
        public DateTime CreateDate { get; set; }

        /// <summary> Дата начала работы </summary>
        [Column("start_date"), Comment("Дата начала работы")]
        public DateTime StartDate { get; set; }

        /// <summary> Дата окончания работы </summary>
        [Column("end_date"), Comment("Дата окончания работы")]
        public DateTime EndDate { get; set; }
    }
}
