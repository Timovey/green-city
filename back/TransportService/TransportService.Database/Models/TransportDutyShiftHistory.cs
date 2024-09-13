using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Историческая таблица для таблицы дежурных смен </summary>
    [Table("transport_duty_shift_history"), Comment("Историческая таблица для таблицы дежурных смен")]
    public class TransportDutyShiftHistory : BaseHistoryTableModel
    {
        /// <summary> ИД транспортного средства </summary>
        [Column("transport_id")]
        public int TransportId { get; set; }

        /// <summary> Дата выхода </summary>
        [Column("date")]
        public DateTime Date { get; set; }
    }
}
