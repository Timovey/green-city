namespace FileService.Core.Helpers
{
    /// <summary>
    /// Тип ресурса
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// Все ресурсы
        /// </summary>
        Any = 0,
        /// <summary>
        /// Дисковые ресурсы
        /// </summary>
        Disk = 1,
        /// <summary>
        /// Распечатать ресурсы
        /// </summary>
        Print = 2,
        Reserved = 8,
    }
}
