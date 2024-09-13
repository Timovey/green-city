using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    public enum ExtDateWorkedDriverType : byte
    {
        /// <summary> Неизвестный </summary>
        [Display(Name = "Неизвестная ошибка")]
        Unknown = 0,
        /// <summary> Работает нормальное кол-во водителей </summary>
        [Display(Name = "Нормально")]
        Work = 1,
        /// <summary> Водителей слишком много на этот день</summary>
        [Display(Name = "Много водителей")]
        WorkMany = 2,
        /// <summary> Нет водителей на данное число </summary>
        [Display(Name = "Нет водителей")]
        NotDrivers = 3,
        /// <summary> Недостаточно водителей на данное число </summary>
        [Display(Name = "Мало водителей")]
        WorkFew = 4,
        /// <summary> Нет расписания </summary>
        [Display(Name = "Нет расписания")]
        NotShedule = 5,
        /// <summary> Праздники/выходные/ремонт весь день </summary>
        [Display(Name = "Не работает")]
        NotWork = 6,
        /// <summary> В аренде </summary>
        [Display(Name = "В аренде")]
        Rent = 7,
    }
}
