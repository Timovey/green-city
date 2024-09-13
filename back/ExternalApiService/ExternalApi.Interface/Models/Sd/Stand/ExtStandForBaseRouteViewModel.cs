namespace ExternalApi.Interface.Models.Sd.Stand
{
    public class ExtStandForBaseRouteViewModel
    {
        public int Id { get; set; }

        /// <summary> Адрес </summary>
        public string Address { get; set; }

        /// <summary> Долгота </summary>
        public decimal Longitude { get; set; }

        /// <summary> Широта </summary>
        public decimal Latitude { get; set; }

        /// <summary> Комментарий к координатам (иногда вместо координат написан комментарий) </summary>
        public string CoordinatesComment { get; set; }

        /// <summary>
        /// Тип контейнерной площадки
        /// 0 - Неизвестно
        /// 1 - Обычный (с договором и контрагентами)
        /// 2 - Жилой фонд
        /// </summary>
        public ExtStandType StandType { get; set; }

        /// <summary> Контакты </summary>
        public string Contacts { get; set; }
    }
}
