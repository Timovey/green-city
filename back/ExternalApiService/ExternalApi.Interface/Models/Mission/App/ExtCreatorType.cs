using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Mission.App
{
    public enum ExtCreatorType : byte
    {
        /// <summary> Неизвестный </summary>
        [Display(Name = "Неизвестно")]
        Unknown = 0,

        /// <summary> АО </summary>
        [Display(Name = "АО")]
        Sd = 1,

        /// <summary> Логист </summary>
        [Display(Name = "Логист")]
        Logist = 2,

        /// <summary> Автогенерация </summary>
        [Display(Name = "Автогенерация")]
        System = 3
    }
}
