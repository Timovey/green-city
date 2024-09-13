namespace TransportContracts.DataContracts.Enums
{
    public enum DriverVacationType : byte
    {
        Unknown = 0,
        /// <summary> Больничный</summary>
        SickLeave = 1,
        /// <summary> Отпуск </summary>
        Vacation = 2,
        /// <summary>
        /// Не пришел
        /// </summary>
        NotCome = 3,
    }
}
