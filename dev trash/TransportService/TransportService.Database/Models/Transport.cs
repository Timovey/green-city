using System;
using System.ComponentModel.DataAnnotations;
using TransportContracts.DataContracts.Enums;

namespace TransportService.Database.Models
{
    internal class Transport
    {
        public int Id { get; set; }

        /// <summary>
        /// Название транспортного средства
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// ИД организации
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Аренда
        /// </summary>
        public bool Rent { get; set; }

        /// <summary>
        /// Регистрационный номер
        /// </summary>
        [Required, MaxLength(32)]
        public string RegNumber { get; set; }

        /// <summary>
        /// СТС
        /// </summary>
        [Required, MaxLength(32)]
        public string Sts { get; set; }

        /// <summary>
        /// Тип транспортного средства
        /// </summary>
        [Required, MaxLength(100)]
        public string TransportType { get; set; }

        /// <summary>
        /// Тип кузова
        /// </summary>
        [Required, MaxLength(100)]
        public string BodyType { get; set; }

        /// <summary>
        /// Цвет кузова
        /// </summary>
        [Required, MaxLength(100)]
        public string BodyColor { get; set; }

        /// <summary>
        /// Забор бака
        /// </summary>
        [Required, MaxLength(100)]
        public string TankFence { get; set; }

        /// <summary>
        /// Тип бака
        /// </summary>
        public TankType TankType { get; set; }

        /// <summary>
        /// Грузоподъемность
        /// </summary>
        [Required, MaxLength(32)]
        public string LoadCapacity { get; set; }

        /// <summary>
        /// Объем кузова
        /// </summary>
        public int CargoVolume { get; set; }

        /// <summary>
        /// Объем кузова с коефицентом 
        /// </summary>
        [Required, MaxLength(32)]
        public string CargoVolumeCoef { get; set; }

        /// <summary>
        /// Пробег
        /// </summary>
        public int Mileage { get; set; }

        /// <summary>
        /// Год выпуска
        /// </summary>
        public int ProductionYear { get; set; }

        /// <summary>
        /// Предполагаемый СЭ
        /// </summary>
        [Required, MaxLength(32)]
        public string Se { get; set; }

        /// <summary>
        /// Лимит затрат
        /// </summary>
        public string SpendingLimit { get; set; }

        /// <summary>
        /// Номер расхода ГСМ
        /// </summary>
        public string GsmNumber { get; set; }

        /// <summary>
        /// Дата последнего ТО
        /// </summary>
        public DateTime LastTO { get; set; }

        /// <summary>
        /// ТО и ремонты
        /// </summary>
        public string RepairsTO { get; set; }
    }
}
