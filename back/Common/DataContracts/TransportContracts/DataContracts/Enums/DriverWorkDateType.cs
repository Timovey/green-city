namespace TransportContracts.DataContracts.Enums
{
    /// <summary>
    /// отображение в таблице водителей
    /// </summary>
    public enum DriverWorkDateType : byte
    {
        /// <summary> Неизвестный </summary>
        Unknown = 0,
        /// <summary> Работает в первую смену по графику 6/3</summary>
        WorkFirst = 1,
        /// <summary> Работает во вторую смену по графику 6/3</summary>
        WorkSecond = 2,
        /// <summary> Работает весь день по графику 3/3</summary>
        WorkThreeThree = 3,
        /// <summary> Работает весь день по графику 5/2</summary>
        WorkFiveTwo = 4,
        /// <summary> Болеет </summary>
        SickLeave = 5,
        /// <summary> В отпуске </summary>
        Vacation = 6,
        /// <summary> Не работает </summary>
        NotWork = 7,
        /// <summary> ТО/ Ремонт</summary>
        ToRepair = 8,
        /// <summary> Не пришел </summary>
        NotCome = 9,
    }
}
