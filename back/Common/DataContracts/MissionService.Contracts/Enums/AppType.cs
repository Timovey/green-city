namespace MissionService.Contracts.Enums
{
    /// <summary>
    /// Тип миссии
    /// </summary>
    public enum AppType : byte
    {
        /// <summary> Неизвестный </summary>
        Unknown = 0,

        /// <summary> По существующим данным (КА и Договор) </summary>
        ByContractClient = 1,

        /// <summary> Точка на карте </summary>
        FromMap = 2
    }
}
