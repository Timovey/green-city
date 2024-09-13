using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenCity.Logging.DbLogging
{
    /// <summary> Таблица с логами </summary>
    [Table("logs")]
    public class LogDbModel
    {
        /// <summary> Первичный ключ </summary>
        [Key, Column("log_id")]
        public int Id { get; set; }

        /// <summary> Дата записи лога </summary>
        [Column("log_date")]
        public DateTime LogDate { get; set; }

        /// <summary> Уровень лога 1-Debug 2-Info 3-Warning 4-Error 5-Critical 6-None </summary>
        [Column("log_level")]
        public byte LogLevel { get; set; }

        /// <summary> Категория логгера (откуда вызвали) </summary>
        [Column("category_name"), MaxLength(500)]
        public string CategoryName { get; set; }

        /// <summary> Сообщение </summary>
        [Column("message"), MaxLength(4096)]
        public string Message { get; set; }
    }
}
