using GreenCity.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.File
{
    public class ExtResponseFileContract : CommonHttpContract
    {
        /// <summary>
        /// Ошибка при обработке запроса
        /// </summary>
        public IDictionary<string, string> Errors { get; set; }
        /// <summary>
        /// Результат для отправки файла
        /// байты файла, расширение и имя файла
        /// </summary>
        public byte[] Buffer { get; set; }
        /// <summary>
        /// Результат для отправки файла
        /// расширение 
        /// </summary>
        [MaxLength(20), Required]
        public string Extension { get; set; }
        /// <summary>
        /// Результат для отправки файла
        /// имя файла
        /// </summary>
        [MaxLength(512), Required]
        public string Filename { get; set; }
    }
}
