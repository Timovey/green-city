using GreenCity.Http;

namespace ExternalApi.Interface.Http
{
    public class CommonHttpFilterV2 : CommonHttpContract
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
        public bool IsFilterByCreatedUser { get { return false; } set { _isFilter = value; } }

        private bool _isFilter;
    }
}
