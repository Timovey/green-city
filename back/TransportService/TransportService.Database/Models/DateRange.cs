using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Базовый класс диапазонов дат</summary>
    [Table("data_range"), Comment("Базовый класс диапазонов дат")]
    public class DateRange : BaseDbModel
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
