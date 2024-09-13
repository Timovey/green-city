using DispatcherService.Contracts.SortModels;
using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace DispatcherService.Contracts.Filters
{
    public class AuxiliaryWorkerFilterModel : CommonHttpFilter
    {
        /// <summary> Флаг уволенности </summary>
        public bool? IsFired { get; set; }

        /// <summary> Дата увольнения с </summary>
        public DateTime? FiredDateFrom { get; set; }

        /// <summary> Дата увольнения по </summary>
        public DateTime? FiredDateTo { get; set; }

        /// <summary> Имя водителя </summary>
        public string? Name { get; set; }

        /// <summary> Фамилия водителя </summary>
        public string? Surname { get; set; }

        /// <summary> Дата принятия на работу с </summary>
        public DateTime? AcceptWorkDateFrom { get; set; }

        /// <summary> Дата принятия на работу по </summary>
        public DateTime? AcceptWorkDateTo { get; set; }

        /// <summary> ИД организации </summary>
        public IList<int>? OrganizationIds { get; set; }

        /// <summary> Контактный телефон </summary>
        public string? ContactNumber { get; set; }

        /// <summary> Дата рождения с </summary>
        public DateTime? BirthDateFrom { get; set; }

        /// <summary> Дата рождения по </summary>
        public DateTime? BirthDateTo { get; set; }

        /// <summary> Место рождения </summary>
        public string? BirthPlace { get; set; }

        /// <summary> Адрес регистрации </summary>
        public string? RegAddress { get; set; }

        /// <summary> Адрес фактический </summary>
        public string? FactAddress { get; set; }

        /// <summary> Серия паспорта </summary>
        public string? PassportSerie { get; set; }

        /// <summary> Номер паспорта </summary>
        public string? PassportNumber { get; set; }

        /// <summary> Дата выдачи с </summary>
        public DateTime? IssueDateFrom { get; set; }

        /// <summary> Дата выдачи по </summary>
        public DateTime? IssueDateTo { get; set; }

        /// <summary> Кем выдан </summary>
        public string? Issuer { get; set; }

        public AuxiliaryWorkerSortModel Sorting { get; set; }
    }
}
