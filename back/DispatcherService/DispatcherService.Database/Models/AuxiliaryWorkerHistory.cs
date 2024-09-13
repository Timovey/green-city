using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherService.Database.Models
{
    /// <summary> Историческая таблица для подсобного рабочего </summary>
    [Table("auxiliary_worker_history"), Comment("Историческая таблица для подсобного рабочего")]
    public class AuxiliaryWorkerHistory : BaseHistoryTableModel
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

        /// <summary> Имя </summary>
        [Column("name"), Comment("Имя"), Required, MaxLength(500)]
        public string Name { get; set; }

        /// <summary> Фамилия</summary>
        [Column("surname"), Comment("Фамилия"), Required, MaxLength(500)]
        public string Surname { get; set; }

        /// <summary> Отчество</summary>
        [Column("patronymic"), Comment("Отчество"), MaxLength(500)]
        public string? Patronymic { get; set; }

        /// <summary> Дата принятия на работу </summary>
        [Column("accept_work_date"), Comment("Дата принятия на работу")]
        public DateTime AcceptWorkDate { get; set; }

        /// <summary> ИД организации </summary>
        [Column("organization_id"), Comment("ИД организации")]
        public int? OrganizationId { get; set; }

        /// <summary> Контактный телефон </summary>
        [Column("contact_number"), Comment("Контактный телефон"), MaxLength(128)]
        public string ContactNumber { get; set; }

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
