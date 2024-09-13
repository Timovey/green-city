using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    public enum ExtDriverWorkDateType : byte
    {
        /// <summary> Неизвестный </summary>
        [Display(Name = "Неизвестно")]
        Unknown = 0,
        /// <summary> Работает в первую смену по графику 6/3</summary>
        [Display(Name = "1")]
        WorkFirst = 1,
        /// <summary> Работает во вторую смену по графику 6/3</summary>
        [Display(Name = "2")]
        WorkSecond = 2,
        /// <summary> Работает весь день по графику 3/3</summary>
        [Display(Name = "3/3")]
        WorkThreeThree = 3,
        /// <summary> Работает весь день по графику 5/2</summary>
        [Display(Name = "5/2")]
        WorkFiveTwo = 4,
        /// <summary> Болеет </summary>
        [Display(Name = "Болеет")]
        SickLeave = 5,
        /// <summary> В отпуске </summary>
        [Display(Name = "В отпуске")]
        Vacation = 6,
        /// <summary> Не работает </summary>
        [Display(Name = "Не работает")]
        NotWork = 7,
        /// <summary> ТО/Ремонт </summary>
        [Display(Name = "ТО/Ремонт")]
        ToRepair = 8,
        /// <summary> Не пришел </summary>
        [Display(Name = "Не пришел")]
        NotCome = 9,
    }
}
