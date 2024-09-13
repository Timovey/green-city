using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Sd.Contract.ScheduleModels
{
    /// <summary>
    /// Как поступать если рабочий день выпал на праздник или на выходной
    /// </summary>
    public enum ExtNotWorkDayReactionType : byte
    {
        /// <summary> Неизвестно </summary>
        [Display(Name = "Неизвестно")]
        Unknown = 0,

        /// <summary> Ближайший рабочий день вперед </summary>
        [Display(Name = "Ближайший рабочий день вперед")]
        NextWorkDay = 1,

        /// <summary> Ближайший рабочий день назад </summary>
        [Display(Name = "Ближайший рабочий день назад")]
        PrevWorkDat = 2,

        /// <summary> Ближайший рабочий </summary>
        [Display(Name = "Ближайший рабочий")]
        NearestWorkingDay = 3,

        /// <summary> Работаем в праздник или выходной </summary>
        [Display(Name = "Работать в праздник или выходной")]
        Work = 4,

        /// <summary> Просто не работаем </summary>
        [Display(Name = "Не работать")]
        Ignore = 5
    }
}
