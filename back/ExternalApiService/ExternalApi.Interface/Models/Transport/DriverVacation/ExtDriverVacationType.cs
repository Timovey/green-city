using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Transport.DriverVacation
{
    public enum ExtDriverVacationType
    {
        [Display(Name = "Неизвестно")]
        Unknown = 0,
        /// <summary> Больничный</summary>
        [Display(Name = "Больничный")]
        SickLeave = 1,
        /// <summary> Отпуск </summary>
        [Display(Name = "Отпуск")]
        Vacation = 2,
        /// <summary>
        /// Не пришел
        /// </summary>
        [Display(Name = "Не пришел")]
        NotCome = 3,
    }
}
