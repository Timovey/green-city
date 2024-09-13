using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.Transport
{
    /// <summary>
    /// Вью-модель для редактирования
    /// </summary>
    public class ExtTransportViewModel
    {
        /// <summary>
        /// ИД сущности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ИД транспорта
        /// </summary>
        public int TransportId { get; set; }

        /// <summary>
        /// Название транспортного средства
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ИД организации
        /// </summary>
        public int? OrganizationId { get; set; }

        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// СТС
        /// </summary>
        public string Sts { get; set; }

        /// <summary>
        /// Тип транспортного средства
        /// </summary>
        public ExtTransportType TransportType { get; set; }

        /// <summary>
        /// Тип кузова
        /// </summary>
        public string BodyType { get; set; }

        /// <summary>
        /// Цвет кузова
        /// </summary>
        public string BodyColor { get; set; }

        /// <summary>
        /// Забор бака
        /// </summary>
        public string TankFence { get; set; }

        /// <summary>
        /// Тип бака (тип тары из Абонентского отдела)
        /// </summary>
        public IList<int> TankTypes { get; set; }

        /// <summary>
        /// Грузоподъемность
        /// </summary>
        public string LoadCapacity { get; set; }

        /// <summary>
        /// Объем кузова
        /// </summary>
        public decimal CargoVolume { get; set; }

        /// <summary>
        /// Объем кузова с коефицентом 
        /// </summary>
        public decimal CargoVolumeCoef { get; set; }

        /// <summary>
        /// Год выпуска
        /// </summary>
        public int ProductionYear { get; set; }

        /// <summary>
        /// Лимит затрат
        /// </summary>
        public string SpendingLimit { get; set; }

        /// <summary>
        /// Номер расхода ГСМ зима город
        /// </summary>
        public decimal? GsmNumberWinterCity { get; set; }

        /// <summary>
        /// Номер расхода ГСМ зима трасса
        /// </summary>
        public decimal? GsmNumberWinterRoad { get; set; }

        /// <summary>
        /// Номер расхода ГСМ лето город
        /// </summary>
        public decimal? GsmNumberSummerCity { get; set; }

        /// <summary>
        /// Номер расхода ГСМ лето трасса
        /// </summary>
        public decimal? GsmNumberSummerRoad { get; set; }

        /// <summary>
        /// Норма на 1 моточас
        /// </summary>
        public decimal? NormOneMotoHour { get; set; }

        /// <summary>
        /// Норма на 1 академический час
        /// </summary>
        public decimal? NormOneAcademyHour { get; set; }

        /// <summary>
        /// Норма на 1 минуту
        /// </summary>
        public decimal? NormOneMinute { get; set; }

        /// <summary>
        /// Дата последнего ТО
        /// </summary>
        public DateTime? LastTO { get; set; }

        /// <summary>
        /// ТО и ремонты
        /// </summary>
        public string RepairsTO { get; set; }
    }
}
