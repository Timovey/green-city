using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherService.Database.Models
{
    /// <summary> Сменное задание </summary>
    [Table("shift_task"), Comment("Сменное задание")]
    public class ShiftTask : BaseDbModel
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
