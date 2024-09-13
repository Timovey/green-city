using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace FileService.Database.Models
{
    /// <summary> хранение информации о файлах </summary>
    [Table("file_in_store"), Comment("хранение информации о файлах")]
    public class FileInStore : BaseDbModel
    {
        /// <summary> Имя файла </summary>
        [Column("filename"), Comment("Имя файла"), MaxLength(512)]
        public string Filename { get; set; }

        /// <summary> Папка файла </summary>
        [Column("folder"), Comment("Путь до файла"), MaxLength(512)]
        public string? Folder { get; set; }
    }
}
