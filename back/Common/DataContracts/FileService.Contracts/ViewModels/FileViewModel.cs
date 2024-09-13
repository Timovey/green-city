using System.ComponentModel.DataAnnotations;

namespace FileService.Contracts.ViewModels
{
    public class FileViewModel
    {
        /// <summary> ID сущности</summary>
        public int FileId { get; set; }

        /// <summary> Название файла</summary>
        [MaxLength(512)]
        public string Filename { get; set; }

        /// <summary> Название файла</summary>
        [MaxLength(512)]
        public string? Folder { get; set; }
    }
}
