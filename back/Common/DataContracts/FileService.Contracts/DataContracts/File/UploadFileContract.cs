using GreenCity.Http;
using System.ComponentModel.DataAnnotations;

namespace FileService.Contracts.DataContracts.File
{
    public class UploadFileContract : CommonHttpContract
    {
        /// <summary>
        /// Длина файла
        /// </summary>
        public long FileLength { get; set; }
        /// <summary>
        /// Имя файла
        /// </summary>
        [MaxLength(512), Required]
        public string Filename { get; set; }
        /// <summary>
        /// Папка файла
        /// </summary>
        [MaxLength(512)]
        public string? Folder { get; set; }
        /// <summary>
        /// байты файла, расширение и имя файла
        /// </summary>
        public byte[] Buffer { get; set; }
    }
}
