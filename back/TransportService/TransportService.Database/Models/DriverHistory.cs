using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Историческая таблица для таблицы driver </summary>
    [Table("driver_history"),Comment("Историческая таблица для таблицы driver")]
    public class DriverHistory : BaseHistoryTableModel
    {
        /// <summary> Табличный номер </summary>
        [Column("table_number"), Comment("Табличный номер"), Required, MaxLength(255)]
        public string TableNumber { get; set; }

        /// <summary> Флаг уволенности </summary>
        [Column("is_fired"), Comment("Флаг уволенности")]
        public bool IsFired { get; set; }

        /// <summary> Дата увольнения </summary>
        [Column("fired_date"), Comment("Дата увольнения")]
        public DateTime? FiredDate { get; set; }

        /// <summary> Имя водителя </summary>
        [Column("driver_name"), Comment("Имя водителя"), Required, MaxLength(500)]
        public string DriverName { get; set; }

        /// <summary> Фамилия водителя </summary>
        [Column("driver_surname"), Comment("Фамилия водителя"), Required, MaxLength(500)]
        public string DriverSurname { get; set; }

        /// <summary> Отчество водителя </summary>
        [Column("driver_patronymic"), Comment("Отчество водителя "), MaxLength(500)]
        public string DriverPatronymic { get; set; }

        /// <summary> Дата принятия на работу </summary>
        [Column("accept_work_date"), Comment("Дата принятия на работу")]
        public DateTime AcceptWorkDate { get; set; }

        /// <summary> Должность </summary>
        [Column("driver_position_type"), Comment("Должность"), Required]
        public byte DriverPositionType { get; set; }

        /// <summary> ИД организации </summary>
        [Column("organization_id"), Comment("ИД организации")]
        public int? OrganizationId { get; set; }

        /// <summary> Квалификаицонный разряд </summary>
        [Column("skill_class"), Comment("Квалификаицонный разряд"), MaxLength(255)]
        public string SkillClass { get; set; }

        /// <summary> Чем может управлять в формате Json </summary>
        [Column("driving_skills_json"), Comment("Чем может управлять в формате Json"), Required, MaxLength(255)]
        public string DrivingSkillsJson { get; set; }

        /// <summary> Контактный телефон </summary>
        [Column("contact_number"), Comment("Контактный телефон"), MaxLength(128)]
        public string ContactNumber { get; set; }

        /// <summary> Водительское удостоверение </summary>
        [Column("drivers_license"), Comment("Водительское удостоверение"), MaxLength(128)]
        public string DriversLicense { get; set; }

        /// <summary> Дата рождения </summary>
        [Column("birth_date"), Comment("Дата рождения")]
        public DateTime BirthDate { get; set; }

        /// <summary> Место рождения </summary>
        [Column("birth_place"), Comment("Место рождения"), MaxLength(255)]
        public string BirthPlace { get; set; }

        /// <summary> Адрес регистрации </summary>
        [Column("reg_address"), Comment("Адрес регистрации"), MaxLength(255)]
        public string RegAddress { get; set; }

        /// <summary> Адрес фактический </summary>
        [Column("fact_address"), Comment("Адрес фактический"), MaxLength(255)]
        public string FactAddress { get; set; }

        /// <summary> Серия паспорта </summary>
        [Column("passport_serie"), Comment("Серия паспорта"), MaxLength(16)]
        public string PassportSerie { get; set; }

        /// <summary> Номер паспорта </summary>
        [Column("passport_number"), Comment("Номер паспорта"), MaxLength(16)]
        public string PassportNumber { get; set; }

        /// <summary> Код подразделения </summary>
        [Column("devision_code"), Comment("Код подразделения"), MaxLength(16)]
        public string DevisionCode { get; set; }

        /// <summary> Дата выдачи </summary>
        [Column("issue_date"), Comment("Дата выдачи")]
        public DateTime IssueDate { get; set; }

        /// <summary> Кем выдан </summary>
        [Column("issuer"), Comment("Кем выдан")]
        public string Issuer { get; set; }
    }
}
