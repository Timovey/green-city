namespace MissionService.Contracts.Enums
{
    /// <summary>
    /// Высчитанный на основе миссий статус заявки
    /// </summary>
    public enum AppComputedStatus : byte
    {
        /// <summary> Неизвестный </summary>
        Unknown = 0,

        /// <summary> Все миссии нераспределены </summary>
        New = 1,

        /// <summary> Хотябы одна миссия в работе, а остальные новые </summary>
        InProgress = 2,

        /// <summary> Есть хотябы одна проваленная миссия </summary>
        Failed = 3,

        /// <summary> Часть выполнена, часть в процессе или новая </summary>
        PartiallyDone = 4,

        /// <summary> Все выполнены </summary>
        Done = 5
    }
}
