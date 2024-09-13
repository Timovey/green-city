using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System;

namespace DispatcherService.Contracts.DataContracts.AuxiliaryWorker
{
    public class CreateAuxiliaryWorkerContract : CommonHttpContract
    {
        /// <summary> Табличный номер </summary>
        [GCRequired("Табличный номер"), GCMaxLength(255)]
        public string TableNumber { get; set; }

        /// <summary> Флаг уволенности </summary>
        public bool IsFired { get; set; }

        /// <summary> Дата увольнения </summary>
        public DateTime? FiredDate { get; set; }

        /// <summary> Имя </summary>
        [GCRequired("Имя"), GCMaxLength(500)]
        public string Name { get; set; }

        /// <summary> Фамилия </summary>
        [GCRequired("Фамилия"), GCMaxLength(500)]
        public string Surname { get; set; }

        /// <summary> Отчество </summary>
        [GCMaxLength(500)]
        public string Patronymic { get; set; }

        /// <summary> Дата принятия на работу </summary>
        public DateTime AcceptWorkDate { get; set; }

        /// <summary> ИД организации </summary>
        public int? OrganizationId { get; set; }

        /// <summary> Контактный телефон </summary>
        [GCMaxLength(128)]
        public string ContactNumber { get; set; }

        /// <summary> Дата рождения </summary>
        public DateTime BirthDate { get; set; }

        /// <summary> Место рождения </summary>
        [GCMaxLength(255)]
        public string BirthPlace { get; set; }

        /// <summary> Адрес регистрации </summary>
        [GCMaxLength(255)]
        public string RegAddress { get; set; }

        /// <summary> Адрес фактический </summary>
        [GCMaxLength(255)]
        public string FactAddress { get; set; }

        /// <summary> Серия паспорта </summary>
        [GCMaxLength(16)]
        public string PassportSerie { get; set; }

        /// <summary> Номер паспорта </summary>
        [GCMaxLength(16)]
        public string PassportNumber { get; set; }

        /// <summary> Код подразделения </summary>
        [GCMaxLength(16)]
        public string DevisionCode { get; set; }

        /// <summary> Дата выдачи </summary>
        public DateTime IssueDate { get; set; }

        /// <summary> Кем выдан </summary>
        public string Issuer { get; set; }
    }
}
