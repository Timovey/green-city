namespace SDContracts.Enums
{
    public enum ContractStatus : byte
    {
        /// <summary>
        /// Неизвестный статус
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Направлен
        /// </summary>
        Sended = 2,

        /// <summary>
        /// Заключен
        /// </summary>
        Accepted = 1
    }
}
