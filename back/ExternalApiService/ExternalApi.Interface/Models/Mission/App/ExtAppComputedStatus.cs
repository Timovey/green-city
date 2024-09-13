using System.ComponentModel.DataAnnotations;

namespace ExternalApi.Interface.Models.Mission.App
{
    /// <summary>
    /// Высчитанный на основе миссий статус заявки
    /// </summary>
    public enum ExtAppComputedStatus : byte
    {
        /// <summary> Неизвестный </summary>
        [Display(Name = "Неизвестный")]
        Unknown = 0,

        /// <summary> Все миссии нераспределены </summary>
        [Display(Name = "Новая")]
        New = 1,

        /// <summary> Хотябы одна миссия в работе, а остальные новые </summary>
        [Display(Name = "В процессе")]
        InProgress = 2,

        /// <summary> Есть хотябы одна проваленная миссия </summary>
        [Display(Name = "Провал")]
        Failed = 3,

        /// <summary> Часть выполнена, часть в процессе или новая </summary>
        [Display(Name = "Частично выполнена")]
        PartiallyDone = 4,

        /// <summary> Все выполнены </summary>
        [Display(Name = "Успех")]
        Done = 5
    }
}
