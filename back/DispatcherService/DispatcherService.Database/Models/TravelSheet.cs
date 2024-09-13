using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherService.Database.Models
{
    /// <summary> Путевой лист - ПЛ </summary>
    [Table("travel_sheet"), Comment("Путевой лист - ПЛ")]
    public class TravelSheet : BaseDbModel
    {
        /// <summary> Номер ПЛ для отображения </summary>
        [Column("number_to_show"), Comment("Номер ПЛ для отображения"), Required, MaxLength(128)]
        public string NumberToShow { get; set; }

        /// <summary> Порядковый номер ПЛ в разрезе орагнизации </summary>
        [Column("number_per_org"), Comment("Порядковый номер ПЛ в разрезе орагнизации")]
        public int NumberPerOrg { get; set; }

        /// <summary> Дата ПЛ </summary>
        [Column("date"), Comment("Дата ПЛ")]
        public DateTime Date { get; set; }

        /// <summary> ИД машины </summary>
        [Column("car_id"), Comment("ИД машины")]
        public int CarId { get; set; }

        /// <summary> ИД водителя </summary>
        [Column("driver_id"), Comment("ИД водителя")]
        public int DriverId { get; set; }

        /// <summary> ИД организации </summary>
        [Column("org_id"), Comment("ИД организации")]
        public int OrgId { get; set; }

        /// <summary> Дата выезда </summary>
        [Column("start_drive_time"), Comment("Дата выезда"), MaxLength(16)]
        public string StartDriveTime { get; set; }

        /// <summary> Дата заезда </summary>
        [Column("finish_drive_time"), Comment("Дата заезда"), MaxLength(16)]
        public string FinishDriveTime { get; set; }
    }
}
