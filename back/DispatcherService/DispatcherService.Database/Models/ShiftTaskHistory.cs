using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherService.Database.Models
{
    /// <summary> Историческая таблица сменных заданий </summary>
    [Table("shift_task_history"), Comment("Историческая таблица сменных заданий")]
    public class ShiftTaskHistory : BaseHistoryTableModel
    {
        /// <summary> Сменное задание </summary>
        [Column("date"), Comment("Дата")]
        public DateTime Date { get; set; }
        
        /// <summary> ИД путевого листа </summary>
        [Column("travel_sheet_id"), Comment("ИД путевого листа")]
        public int TravelSheetId { get; set; }

        /// <summary> ИД экипажа </summary>
        [Column("work_shift_id"), Comment("ИД экипажа")]
        public int WorkShiftId { get; set; }

        /// <summary> ИД организации </summary>
        [Column("org_id"), Comment("ИД организации")]
        public int OrgId { get; set; }
    }
}
