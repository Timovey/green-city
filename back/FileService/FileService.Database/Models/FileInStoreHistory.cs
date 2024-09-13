using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileService.Database.Models
{
    /// <summary> Историческая таблица для таблицы file</summary>
    [Table("file_in_store_history"), Comment("Историческая таблица для таблицы file")]
    public class FileInStoreHistory : BaseHistoryTableModel
    {
        /// <summary> Имя файла </summary>
        [Column("filename"), Comment("Имя файла"), MaxLength(512)]
        public string Filename { get; set; }

        /// <summary> Папка файла </summary>
        [Column("folder"), Comment("Путь до файла"), MaxLength(512)]
        public string? Folder { get; set; }
    }
}
