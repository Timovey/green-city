using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Transport.Driver
{
    public enum ExtDriverPositionType : byte
    {
        /// <summary> Неизвестный </summary>
        [Display(Name = "Неизвестно")]
        Unknown = 0,
        /// <summary> Основной водитель </summary>
        [Display(Name = "Основной")]
        Main = 1,
        /// <summary> Сменный водитель</summary>
        [Display(Name = "Сменный")]
        Replacement = 2,
    }
}
