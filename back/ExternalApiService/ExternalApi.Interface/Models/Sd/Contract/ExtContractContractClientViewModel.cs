namespace ExternalApi.Interface.Models.Sd.Contract
{
    public class ExtContractContractClientViewModel
    {
        /// <summary> Наименование клиента </summary>
        public string ContractClientId { get; set; }

        /// <summary> Наименование клиента </summary>
        public string ClientName { get; set; }

        /// <summary> Контактный телефон </summary>
        public string ClientContact { get; set; }

        /// <summary> Электронный адрес (email) </summary>
        public string Email { get; set; }

        /// <summary> ИНН </summary>
        public string Inn { get; set; }

        /// <summary> КПП </summary>
        public string Kpp { get; set; }

        /// <summary> ОГРН </summary>
        public string Ogrn { get; set; }

        /// <summary> Расчетный счет </summary>
        public string Rschet { get; set; }

        /// <summary> Банк </summary>
        public string Bank { get; set; }

        /// <summary> Бик </summary>
        public string Bik { get; set; }

        /// <summary> Корреспондентский счет </summary>
        public string Kschet { get; set; }
    }
}
