namespace MissionService.Contracts.Enums
{
    /// <summary>
    /// Права на создания мисии
    /// </summary>
    public enum AppActionsPermission : byte
    {
        /// <summary> Неизвестно </summary>
        Unknown = 0,

        /// <summary> Обычные </summary>
        Default = 1,

        /// <summary> Абсолютные </summary>
        Absolute = 2
    }
}
