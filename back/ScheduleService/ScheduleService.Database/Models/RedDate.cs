using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleService.Database.Models
{
    /// <summary> Реестр праздников </summary>
    [Table("red_date"), Comment("Реестр праздников")]
    internal class RedDate : BaseDbModel
    {
        /// <summary> Дата праздника </summary>
        [Column("date"), Comment("Дата праздника")]
        public DateTime Date { get; set; }

        /// <summary> Название праздника </summary>
        [Column("name"), Comment("Название праздника"), MaxLength(512)]
        public string Name { get; set; }
    }
}
