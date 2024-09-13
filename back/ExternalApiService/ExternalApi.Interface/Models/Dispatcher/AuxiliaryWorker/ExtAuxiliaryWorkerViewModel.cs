using System;

namespace ExternalApi.Interface.Models.Dispatcher.AuxiliaryWorker
{
    public class ExtAuxiliaryWorkerViewModel
    {
        /// <summary> ИД </summary>
        public int Id { get; set; }

        /// <summary> Табличный номер </summary>
        public string TableNumber { get; set; }

        /// <summary> Флаг уволенности </summary>
        public bool IsFired { get; set; }

        /// <summary> Дата увольнения </summary>
        public DateTime? FiredDate { get; set; }

        /// <summary> Имя </summary>
        public string Name { get; set; }

        /// <summary> Фамилия</summary>
        public string Surname { get; set; }

        /// <summary> Отчество</summary>
        public string? Patronymic { get; set; }

        /// <summary> Дата принятия на работу </summary>
        public DateTime AcceptWorkDate { get; set; }

        /// <summary> ИД организации </summary>
        public int? OrganizationId { get; set; }

        /// <summary> Контактный телефон </summary>
        public string ContactNumber { get; set; }

        /// <summary> Дата рождения </summary>
        public DateTime BirthDate { get; set; }

        /// <summary> Место рождения </summary>
        public string BirthPlace { get; set; }

        /// <summary> Адрес регистрации </summary>
        public string RegAddress { get; set; }

        /// <summary> Адрес фактический </summary>
        public string FactAddress { get; set; }

        /// <summary> Серия паспорта </summary>
        public string PassportSerie { get; set; }

        /// <summary> Номер паспорта </summary>
        public string PassportNumber { get; set; }

        /// <summary> Код подразделения </summary>
        public string DevisionCode { get; set; }

        /// <summary> Дата выдачи </summary>
        public DateTime IssueDate { get; set; }

        /// <summary> Кем выдан </summary>
        public string Issuer { get; set; }
    }
}
