namespace ExternalApi.Interface.Models.Sd.Mission
{
    public class ExtContractClientInfoMissionViewModel
    {
        /// <summary> ИД клиента </summary>
        public int ContractClientId { get; set; }

        /// <summary> Наименование клиента </summary>
        public string ClientName { get; set; }

        /// <summary> Контактный телефон </summary>
        public string ClientContact { get; set; }
    }
}
