namespace SDContracts.Enums
{
    public enum ContractScheduleDateCreatorSource : byte
    {
        /// <summary> Неизвестно </summary>
        Unknown = 0,

        /// <summary> Из формы по редактированию или созданию контракта </summary>
        Contract = 1,

        /// <summary> Срочная заявка </summary>
        UrgentTask = 2
    }
}
