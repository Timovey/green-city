using GreenCity.Http;
using System.ComponentModel.DataAnnotations;

namespace FileService.Contracts.DataContracts.File
{
    public class DownloadFileByNameContract : CommonHttpContract
    {
        /// <summary> Название файла</summary>
        [MaxLength(512)]
        public string FileName { get; set; }

        /// <summary> Папка файла</summary>
        [MaxLength(512)]
        public string? Folder { get; set; }
    }
}
