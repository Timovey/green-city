using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Transport.RepairCard
{
    public enum ExtWorkShift
    {
        [Display(Name = "Неизвестно")]
        /// <summary> Неизвестно </summary>
        Unknown = 0,

        /// <summary> Первая смена </summary>
        [Display(Name = "Первая смена")]
        First = 1,

        /// <summary> Вторая смена </summary>
        [Display(Name = "Вторая смена")]
        Second = 2,

        /// <summary> Весь день</summary>
        [Display(Name = "Весь день")]
        AllDay = 3,
    }
}
