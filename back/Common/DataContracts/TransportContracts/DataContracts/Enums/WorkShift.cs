namespace TransportContracts.DataContracts.Enums
{
    /// <summary> Возможные смены </summary>
    public enum WorkShift : byte
    {
        /// <summary> Неизвестно </summary>
        Unknown = 0,

        /// <summary> Первая смена </summary>
        First = 1,

        /// <summary> Вторая смена </summary>
        Second = 2,

        /// <summary> Весь день</summary>
        AllDay = 3,

    }
}
