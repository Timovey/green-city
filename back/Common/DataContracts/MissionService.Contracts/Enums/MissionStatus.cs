namespace MissionService.Contracts.Enums
{
    /// <summary>
    /// Статус миссии
    /// </summary>
    public enum MissionStatus : byte
    {
        /// <summary> Неизвестный </summary>
        Unknown = 0,

        /// <summary> Новая </summary>
        New = 1,

        /// <summary> В процессе </summary>
        InProgress = 2,

        /// <summary> Провалена по вине возчика </summary>
        Failed = 3,

        /// <summary> Выполнена </summary>
        Done = 4
    }
}
