using System;
using System.Collections.Generic;
using TransportContracts.DataContracts.Enums;

namespace TransportContracts.ViewModels
{
    public class DriverViewModel
    {
        /// <summary> ИД водителя </summary>
        public int DriverId { get; set; }

        /// <summary> Табличный номер </summary>
        public string TableNumber { get; set; }

        /// <summary> Флаг уволенности </summary>
        public bool IsFired { get; set; }

        /// <summary> Дата увольнения </summary>
        public DateTime? FiredDate { get; set; }

        /// <summary> Имя водителя </summary>
        public string DriverName { get; set; }

        /// <summary> Фамилия водителя </summary>
        public string DriverSurname { get; set; }

        /// <summary> Отчество водителя </summary>
        public string DriverPatronymic { get; set; }

        /// <summary> Дата принятия на работу </summary>
        public DateTime AcceptWorkDate { get; set; }

        /// <summary> Должность </summary>
        public DriverPositionType DriverPositionType { get; set; }

        /// <summary> ИД организации </summary>
        public int? OrganizationId { get; set; }

        /// <summary> Квалификаицонный разряд </summary>
        public string SkillClass { get; set; }

        /// <summary> Чем может управлять в формате Json </summary>
        public IList<DrivingSkillsType> DrivingSkills { get; set; }

        /// <summary> Контактный телефон </summary>
        public string ContactNumber { get; set; }

        /// <summary> Водительское удостоверение </summary>
        public string DriversLicense { get; set; }

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
