namespace ExternalApi.Interface.Models.Enums
{
    /// <summary>
    /// Перечисление для запросов преобразователя перечислений
    /// </summary>
    public enum ModuleType : byte
    {
        /// <summary>
        /// Таксировщик
        /// </summary>
        Transport = 1,
        /// <summary>
        /// Абонентский отдел
        /// </summary>
        SD = 2,
        /// <summary>
        /// Логист
        /// </summary>
        Logist = 3,
        /// <summary>
        /// Миссии
        /// </summary>
        Mission = 4
    }
}
