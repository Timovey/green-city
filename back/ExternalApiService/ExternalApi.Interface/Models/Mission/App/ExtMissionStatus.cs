using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Mission.App
{
    /// <summary>
    /// Статус миссии
    /// </summary>
    public enum ExtMissionStatus : byte
    {
        /// <summary> Неизвестный </summary>
        [Display(Name = "Неизвестно")]
        Unknown = 0,

        /// <summary> Новая </summary>
        [Display(Name = "Новая")]
        New = 1,

        /// <summary> В процессе </summary>
        [Display(Name = "В процессе")]
        InProgress = 2,

        /// <summary> Провалена по вине возчика </summary>
        [Display(Name = "Провалена")]
        Failed = 3,

        /// <summary> Выполнена </summary>
        [Display(Name = "Выполнена")]
        Done = 4
    }
}
