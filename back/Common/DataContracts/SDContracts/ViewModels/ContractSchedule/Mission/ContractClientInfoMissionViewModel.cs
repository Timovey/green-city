namespace SDContracts.ViewModels.ContractSchedule.Mission
{
    /// <summary> Информация по контрагенту в задании </summary>
    public class ContractClientInfoMissionViewModel
    {
        /// <summary> ИД клиента </summary>
        public int ContractClientId { get; set; }

        /// <summary> Наименование клиента </summary>
        public string ClientName { get; set; }

        /// <summary> Контактный телефон </summary>
        public string ClientContact { get; set; }
    }
}
