using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Рабочая смена машины на число </summary>
    [Table("transport_work_shift"), Comment("Рабочая смена машины на число")]
    public class TransportWorkShift : BaseDbModel
    {
        /// <summary>
        /// ID транспорта
        /// </summary>
        [Column("transport_id")]
        public int TransportId { get; set; }

        /// <summary>
        /// Дата выхода
        /// </summary>
        [Column("date_time")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Организации, которым доступны рабочие смены
        /// </summary>
        [Column("organization_ids_json")]
        public string OrganizationIdsJson { get; set; }

        /// <summary>
        /// ИД Водителя
        /// </summary>
        [Column("driver_id")]
        public int DriverId { get; set; }

        /// <summary>
        /// находится ли экипаж в рейсе
        /// </summary>
        [Column("is_in_route")]
        public bool IsInRoute { get; set; }

        /// <summary>
        /// Рабочая смена(график) водителя
        /// </summary>
        public byte DriverWorkDateType { get; set; }
    }
}
