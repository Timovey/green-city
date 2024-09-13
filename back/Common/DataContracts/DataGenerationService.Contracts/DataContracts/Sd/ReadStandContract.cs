namespace DataGenerationService.Contracts.DataContracts.Sd
{
    public class ReadStandContract
    {
        /// <summary> Номер </summary>
        public int Number { get; set; }

        /// <summary> Признак режимного объекта </summary>
        public bool IsHighSecurityObject { get; set; }

        /// <summary> Адрес </summary>
        public string Address { get; set; }

        /// <summary> Долгота </summary>
        public decimal Longitude { get; set; }

        /// <summary> Широта </summary>
        public decimal Latitude { get; set; }

        /// <summary> Комментарий к координатам </summary>
        public string CoordinatesComment { get; set; }

        /// <summary> Путь подъезда </summary>
        public string DriveWay { get; set; }

        /// <summary> Контакты </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// Тип контейнерной площадки
        /// 0 - Неизвестно
        /// 1 - Обычный (с договором и контрагентами)
        /// 2 - Жилой фонд
        /// </summary>
        public byte StandType { get; set; }

        /// <summary> Есть ли основание </summary>
        public bool HaveBase { get; set; }

        /// <summary> Есть ли крыша </summary>
        public bool HaveRoof { get; set; }

        /// <summary> Есть ли ограждение </summary>
        public bool HaveFence { get; set; }

        /// <summary> Номер пользователя создавшего запись </summary>
        public int CreatedUserNumber { get; set; }

        /// <summary> Привязанные контейнеры </summary>
        public IList<int> AttachedContainerIds { get; set; }
    }
}