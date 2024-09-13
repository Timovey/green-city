using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Дежурная смена </summary>
    [Table("transport_duty_shift"), Comment("Дежурная смена")]
    public class TransportDutyShift : BaseDbModel
    {
        /// <summary> ИД транспортного средства </summary>
        [Column("transport_id")]
        public int TransportId { get; set; }

        /// <summary> Дата выхода </summary>
        [Column("date")]
        public DateTime Date { get; set; }
    }
}
