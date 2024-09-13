using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleService.Database.Models
{
    /// <summary> Реестр праздников </summary>
    [Table("weekend_work_day"), Comment("Реестр рабочих дней выпадающих на выходные")]
    internal class WeekendWorkDate : BaseDbModel
    {
        /// <summary> Дата рабочего дня выпадающего на выходной </summary>
        [Column("date"), Comment("Дата рабочего дня выпадающего на выходной")]
        public DateTime Date { get; set; }
    }
}
