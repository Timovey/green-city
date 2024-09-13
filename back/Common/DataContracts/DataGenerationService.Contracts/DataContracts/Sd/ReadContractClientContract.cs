namespace DataGenerationService.Contracts.DataContracts.Sd
{
    public class ReadContractClientContract
    {
        /// <summary> Номер </summary>
        public int Number { get; set; }

        /// <summary> Имя клиента </summary>
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

        /// <summary> Юридический адрес </summary>
        public string LegalAddress { get; set; }

        /// <summary> Почтовый адрес </summary>
        public string PostalAddress { get; set; }

        /// <summary> Руководитель </summary>
        public string SupervizorName { get; set; }

        /// <summary> Должность руководителя </summary>
        public string SupervizorPosition { get; set; }

        /// <summary> Ответственное лицо - ФИО </summary>
        public string ResponsibleTkoPersonName { get; set; }

        /// <summary> Номер ответственного по ТКО лица </summary>
        public string ResponsibleTkoPersonPhone { get; set; }

        /// <summary> Номер пользователя создавшего запись </summary>
        public int CreatedUserNumber { get; set; }
    }
}