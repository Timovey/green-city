namespace GreenCity.Http
{
    /// <summary>
    /// Базовый фильтр
    /// </summary>
    public class CommonHttpFilter : CommonHttpContract
    {
        /// <summary>
        /// Элементов на странице
        /// </summary>
        public int ItemsOnPage { get; set; }

        /// <summary>
        /// Номер страницы с 0
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Нужна ли фильтрация
        /// </summary>
        public bool IsFilter { get; set; }

        /// <summary>
        /// Нужна ли фильтрация по создателю записи (по умолчанию - да)
        /// </summary>
        public bool IsFilterByCreatedUser { get; set; } = true;
    }
}
