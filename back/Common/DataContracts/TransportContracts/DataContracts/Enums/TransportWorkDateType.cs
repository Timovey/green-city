namespace TransportContracts.DataContracts.Enums
{
    /// <summary>
    /// возможные состояния транспорта на графике
    /// </summary>
    public enum TransportWorkDateType : byte
    {
        /// <summary> Неизвестный </summary>
        Unknown = 0,
        /// <summary> Работает по расписанию 3/3 </summary>
        WorkThreeThree = 1,
        /// <summary> Работает по расписанию 5/2 </summary>
        WorkFiveTwo = 2,
        /// <summary> Работает по расписанию 6/3 </summary>
        WorkSixThree = 3,
        /// <summary> Чинится в 1 смену </summary>
        RepairFirst = 4,
        /// <summary> Чинится в 2 смену </summary>
        RepairSecond = 5,
        /// <summary> Чинится весь день </summary>
        RepairAll = 6,
        /// <summary> Проходит ТО </summary>
        TO = 7,
        /// <summary> Работает без графика </summary>
        NotShedule = 8,
        /// <summary> Нет водителей на данное число и транспорт не чинится </summary>
        NotDrivers = 9,
        /// <summary> Выходные у работников </summary>
        Holidays = 10,
        /// <summary> В аренде</summary>
        Rent = 11,
        /// <summary> Дежурная смена</summary>
        DutyShift = 12,
    }
}
