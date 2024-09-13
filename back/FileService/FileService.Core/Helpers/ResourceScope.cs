namespace FileService.Core.Helpers
{
    /// <summary>
    /// Перечисление для NetResourse, определяет область перечисления
    /// </summary>
    public enum ResourceScope
    {
        /// <summary>
        /// Текущие подключенные ресурсы
        /// </summary>
        Connected = 1,
        /// <summary>
        /// Ресурсы в сети
        /// </summary>
        GlobalNetwork,
        /// <summary>
        /// Запомненные соединения
        /// </summary>
        Remembered,
        Recent,
        Context
    }
}
