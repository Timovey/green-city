using GreenCity.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TransportContracts.DataContracts.Enums;

namespace TransportContracts.DataContracts.Driver
{
    /// <summary> Создание водителя </summary>
    public class CreateDriverContract : CommonHttpContract
    {
        /// <summary> Табличный номер </summary>
        [Required, MaxLength(255)]
        public string TableNumber { get; set; }

        /// <summary> Флаг уволенности </summary>
        public bool IsFired { get; set; }

        /// <summary> Дата увольнения </summary>
        public DateTime? FiredDate { get; set; }

        /// <summary> Имя водителя </summary>
        [Required, MaxLength(500)]
        public string DriverName { get; set; }

        /// <summary> Фамилия водителя </summary>
        [Required, MaxLength(500)]
        public string DriverSurname { get; set; }

        /// <summary> Отчество водителя </summary>
        [MaxLength(500)]
        public string DriverPatronymic { get; set; }

        /// <summary> Дата принятия на работу </summary>
        public DateTime AcceptWorkDate { get; set; }

        /// <summary> Должность </summary>
        public DriverPositionType DriverPositionType { get; set; }

        /// <summary> ИД организации </summary>
        public int? OrganizationId { get; set; }

        /// <summary> Квалификаицонный разряд </summary>
        [MaxLength(255)]
        public string SkillClass { get; set; }

        /// <summary> Чем может управлять в формате Json </summary>
        [Required]
        public IList<DrivingSkillsType> DrivingSkills { get; set; }

        /// <summary> Контактный телефон </summary>
        [MaxLength(128)]
        public string ContactNumber { get; set; }

        /// <summary> Водительское удостоверение </summary>
        [MaxLength(128)]
        public string DriversLicense { get; set; }

        /// <summary> Дата рождения </summary>
        public DateTime BirthDate { get; set; }

        /// <summary> Место рождения </summary>
        [MaxLength(255)]
        public string BirthPlace { get; set; }

        /// <summary> Адрес регистрации </summary>
        [MaxLength(255)]
        public string RegAddress { get; set; }

        /// <summary> Адрес фактический </summary>
        [MaxLength(255)]
        public string FactAddress { get; set; }

        /// <summary> Серия паспорта </summary>
        [MaxLength(16)]
        public string PassportSerie { get; set; }

        /// <summary> Номер паспорта </summary>
        [MaxLength(16)]
        public string PassportNumber { get; set; }

        /// <summary> Код подразделения </summary>
        [MaxLength(16)]
        public string DevisionCode { get; set; }

        /// <summary> Дата выдачи </summary>
        public DateTime IssueDate { get; set; }

        /// <summary> Кем выдан </summary>
        public string Issuer { get; set; }
    }
}
