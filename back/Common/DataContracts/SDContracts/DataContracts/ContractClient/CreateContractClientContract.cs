using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;

namespace SDContracts.DataContracts.ContractClient
{
    public class CreateContractClientContract : CommonHttpContract
    {
        /// <summary> Наименование клиента </summary>
        [GCRequired, GCMaxLength(500)]
        public string ClientName { get; set; }

        /// <summary> Контактный телефон </summary>
        [GCMaxLength(200)]
        public string ClientContact { get; set; }

        /// <summary> Электронный адрес (email) </summary>
        [GCMaxLength(100)]
        public string Email { get; set; }

        /// <summary> ИНН </summary>
        [GCMaxLength(20)]
        public string Inn { get; set; }

        /// <summary> КПП </summary>
        [GCMaxLength(30)]
        public string Kpp { get; set; }

        /// <summary> ОГРН </summary>
        [GCMaxLength(20)]
        public string Ogrn { get; set; }

        /// <summary> Расчетный счет </summary>
        [GCMaxLength(20)]
        public string Rschet { get; set; }

        /// <summary> Банк </summary>
        [GCMaxLength(300)]
        public string Bank { get; set; }

        /// <summary> Бик </summary>
        [GCMaxLength(300)]
        public string Bik { get; set; }

        /// <summary> Корреспондентский счет </summary>
        [GCMaxLength(50)]
        public string Kschet { get; set; }

        /// <summary> Юридический адрес </summary>
        [GCMaxLength(300)]
        public string LegalAddress { get; set; }

        /// <summary> Почтовый адрес </summary>
        [GCMaxLength(300)]
        public string PostalAddress { get; set; }

        /// <summary> Руководитель </summary>
        [GCMaxLength(300)]
        public string SupervizorName { get; set; }

        /// <summary> Должность руководителя </summary>
        [GCMaxLength(300)]
        public string SupervizorPosition { get; set; }

        /// <summary> Ответственное лицо - ФИО </summary>
        [GCMaxLength(200)]
        public string ResponsibleTkoPersonName { get; set; }

        /// <summary> Контактный телефон ответственного лица </summary>
        [GCMaxLength(200)]
        public string ResponsibleTkoPersonPhone { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool IsValidated { get; set; }
    }
}
