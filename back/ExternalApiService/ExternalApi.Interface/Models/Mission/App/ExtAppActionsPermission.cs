using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Mission.App
{
    /// <summary>
    /// Права на создания мисии
    /// </summary>
    public enum ExtAppActionsPermission : byte
    {
        /// <summary> Неизвестно </summary>
        [Display(Name = "Неизвестно")]
        Unknown = 0,

        /// <summary> Обычные </summary>
        [Display(Name = "Обычные")]
        Default = 1,

        /// <summary> Абсолютные </summary>
        [Display(Name = "Абсолютные")]
        Absolute = 2
    }
}
