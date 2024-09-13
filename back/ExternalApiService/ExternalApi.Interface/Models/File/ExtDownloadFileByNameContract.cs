using GreenCity.Http;
using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.File
{
    public class ExtDownloadFileByNameContract : CommonHttpContract
    {
        /// <summary> Название файла</summary>
        [MaxLength(512), Required]
        public string FileName { get; set; }

        /// <summary> Папка файла</summary>
        [MaxLength(512)]
        public string? Folder { get; set; }
    }
}
