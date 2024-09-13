using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherService.Database.Models
{
    /// <summary> Историческая таблица бригад </summary>
    [Table("brigade_history"), Comment("Историческая таблица бригад")]
    public class BrigadeHistory : BaseHistoryTableModel
    {
        /// <summary> Дата работы бригады </summary>
        [Column("date"), Comment("Дата работы бригады")]
        public DateTime Date { get; set; }

        /// <summary> ИД экипажа </summary>
        [Column("driver_id"), Comment("ИД водителя")]
        public int WorkShiftId { get; set; }
    }
}
