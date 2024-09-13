using GreenCity.Http;
using SDContracts.SortModels;
using System.ComponentModel.DataAnnotations;

namespace SDContracts.Filter
{
    /// <summary>
    /// Фильтр модель для контрагентов
    /// </summary>
    public class ContractClientFilterModel : CommonHttpFilter
    {
        /// <summary> Наименование клиента </summary>
        [MaxLength(500)]
        public string ClientName { get; set; }

        /// <summary> Контактный телефон </summary>
        [MaxLength(200)]
        public string ClientContact { get; set; }

        /// <summary> Электронный адрес (email) </summary>
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary> ИНН </summary>
        [MaxLength(20)]
        public string Inn { get; set; }

        /// <summary> КПП </summary>
        [MaxLength(30)]
        public string Kpp { get; set; }

        /// <summary> ОГРН </summary>
        [MaxLength(20)]
        public string Ogrn { get; set; }

        /// <summary> Расчетный счет </summary>
        [MaxLength(20)]
        public string Rschet { get; set; }

        /// <summary> Банк </summary>
        [MaxLength(300)]
        public string Bank { get; set; }

        /// <summary> Бик </summary>
        [MaxLength(300)]
        public string Bik { get; set; }

        /// <summary> Корреспондентский счет </summary>
        [MaxLength(50)]
        public string Kschet { get; set; }

        /// <summary> Юридический адрес </summary>
        [MaxLength(300)]
        public string LegalAddress { get; set; }

        /// <summary> Почтовый адрес </summary>
        [MaxLength(300)]
        public string PostalAddress { get; set; }

        /// <summary> Руководитель </summary>
        [MaxLength(300)]
        public string SupervizorName { get; set; }

        /// <summary> Должность руководителя </summary>
        [MaxLength(300)]
        public string SupervizorPosition { get; set; }

        /// <summary> Ответственное лицо - ФИО </summary>
        [MaxLength(200)]
        public string ResponsibleTkoPersonName { get; set; }

        /// <summary> Контактный телефон ответственного лица </summary>
        [MaxLength(200)]
        public string ResponsibleTkoPersonPhone { get; set; }

        /// <summary> Флаг проверки записи сотрудником </summary>
        public bool? IsValidated { get; set; }

        public ContractClientSortModel Sorting { get; set; }
    }
}
