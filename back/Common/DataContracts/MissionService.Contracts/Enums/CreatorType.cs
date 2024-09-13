namespace MissionService.Contracts.Enums
{
    public enum CreatorType : byte
    {
        /// <summary> Неизвестный </summary>
        Unknown = 0,

        /// <summary> АО </summary>
        Sd = 1,

        /// <summary> Логист </summary>
        Logist = 2,

        /// <summary> Автогенерация </summary>
        System = 3
    }
}
