using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Database.Models
{
    /// <summary> Историческая таблица для таблицы transport </summary>
    [Table("transport_history"), Comment("Историческая таблица для таблицы transport")]
    public class TransportHistory : BaseHistoryTableModel
    {
        /// <summary>
        /// Название транспортного средства
        /// </summary>
        [Column("name"), Comment("Название транспортного средства"), Required, MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// ИД организации
        /// </summary>
        [Column("organization_id"), Comment("ИД организации")]
        public int? OrganizationId { get; set; }

        /// <summary>
        /// Регистрационный номер
        /// </summary>
        [Column("reg_number"), Comment("Регистрационный номер"), Required, MaxLength(32)]
        public string RegNumber { get; set; }

        /// <summary>
        /// СТС
        /// </summary>
        [Column("sts"), Comment("СТС"), Required, MaxLength(32)]
        public string Sts { get; set; }

        /// <summary>
        /// Тип транспортного средства
        /// </summary>
        [Column("transport_type"), Comment("Тип транспортного средства"), Required]
        public byte TransportType { get; set; }

        /// <summary>
        /// Тип кузова
        /// </summary>
        [Column("body_type"), Comment("Тип кузова"), MaxLength(100)]
        public string BodyType { get; set; }

        /// <summary>
        /// Цвет кузова
        /// </summary>
        [Column("body_color"), Comment("Цвет кузова"), MaxLength(100)]
        public string BodyColor { get; set; }

        /// <summary>
        /// Забор бака
        /// </summary>
        [Column("tank_fence"), Comment("Забор бака"), MaxLength(100)]
        public string TankFence { get; set; }

        /// <summary>
        /// Тип бака (тип тары из Абонентского отдела)
        /// Сериализованный вид 
        /// </summary>
        [Column("tank_types_json"), Comment("Тип бака"), MaxLength(255)]
        public string TankTypesJson { get; set; }

        /// <summary>
        /// Грузоподъемность
        /// </summary>
        [Column("load_capacity"), Comment("Грузоподъемность"), MaxLength(32)]
        public string LoadCapacity { get; set; }

        /// <summary>
        /// Объем кузова
        /// </summary>
        [Column("cargo_volume"), Comment("Объем кузова")]
        public decimal CargoVolume { get; set; }

        /// <summary>
        /// Объем кузова с коефицентом 
        /// </summary>
        [Column("cargo_volume_coef"), Comment("Объем кузова с коефицентом")]
        public decimal CargoVolumeCoef { get; set; }

        /// <summary>
        /// Год выпуска
        /// </summary>
        [Column("production_year"), Comment("Год выпуска")]
        public int ProductionYear { get; set; }

        /// <summary>
        /// Лимит затрат
        /// </summary>
        [Column("spending_limit"), Comment("Лимит затрат")]
        public string SpendingLimit { get; set; }

        /// <summary>
        /// Номер расхода ГСМ зима город
        /// </summary>
        [Column("gsm_number_winter_city"), Comment("Номер расхода ГСМ зима город")]
        public decimal? GsmNumberWinterCity { get; set; }

        /// <summary>
        /// Номер расхода ГСМ зима трасса
        /// </summary>
        [Column("gsm_number_winter_road"), Comment("Номер расхода ГСМ зима трасса")]
        public decimal? GsmNumberWinterRoad { get; set; }

        /// <summary>
        /// Номер расхода ГСМ лето город
        /// </summary>
        [Column("gsm_number_summer_city"), Comment("Номер расхода ГСМ лето город")]
        public decimal? GsmNumberSummerCity { get; set; }

        /// <summary>
        /// Номер расхода ГСМ лето трасса
        /// </summary>
        [Column("gsm_number_summer_road"), Comment("Номер расхода ГСМ лето трасса")]
        public decimal? GsmNumberSummerRoad { get; set; }

        /// <summary>
        /// Норма на 1 моточас
        /// </summary>
        [Column("norm_one_moto_hour"), Comment("Норма на 1 моточас")]
        public decimal? NormOneMotoHour { get; set; }

        /// <summary>
        /// Норма на 1 академический час
        /// </summary>
        [Column("norm_one_academy_hour"), Comment("Норма на 1 академический час")]
        public decimal? NormOneAcademyHour { get; set; }

        /// <summary>
        /// Норма на 1 минуту
        /// </summary>
        [Column("norm_one_minute"), Comment("Норма на 1 минуту")]
        public decimal? NormOneMinute { get; set; }

        /// <summary>
        /// Дата последнего ТО
        /// </summary>
        [Column("last_to_date"), Comment("Дата последнего ТО")]
        public DateTime? LastTO { get; set; }

        /// <summary>
        /// ТО и ремонты
        /// </summary>
        [Column("repairs_to"), Comment("ТО и ремонты")]
        public string RepairsTO { get; set; }
    }
}
