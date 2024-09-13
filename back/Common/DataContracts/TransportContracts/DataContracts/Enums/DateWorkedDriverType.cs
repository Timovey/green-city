namespace TransportContracts.DataContracts.Enums
{
    /// <summary>
    /// Ошибки таксировщика по формированию графика транспорта
    /// </summary>
    public enum DateWorkedDriverType : byte
    {
        /// <summary> Неизвестный </summary>
        Unknown = 0,
        /// <summary> Работает нормальное кол-во водителей </summary>
        Work = 1,
        /// <summary> Водителей слишком много на этот день</summary>
        WorkMany = 2,
        /// <summary> Нет водителей на данное число </summary>
        NotDrivers = 3,
        /// <summary> Недостаточно водителей на данное число </summary>
        WorkFew = 4,
        /// <summary> Нет расписания </summary>
        NotShedule = 5,
        /// <summary> Праздники/выходные/ремонт весь день </summary>
        NotWork = 6,
        /// <summary> В аренде </summary>
        Rent = 7,
    }
}
