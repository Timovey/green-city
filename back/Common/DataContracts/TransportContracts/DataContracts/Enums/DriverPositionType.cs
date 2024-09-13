namespace TransportContracts.DataContracts.Enums
{
    public enum DriverPositionType : byte
    {
        /// <summary> Неизвестный </summary>
        Unknown = 0,
        /// <summary> Основной водитель </summary>
        Main = 1,
        /// <summary> Сменный водитель</summary>
        Replacement = 2,
    }
}
