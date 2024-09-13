using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Stand
{
    /// <summary>
    /// Получение КП для формирования рейсов
    /// </summary>
    public class ExtGetFilteredStandsForRouteFilterRequest : CommonHttpFilter
    {
        /// <summary>
        /// Дата миссий
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// ИД КП, которые не нужно брать в выборку
        /// </summary>
        public IList<int> ExcludeStandIds { get; set; }

        /// <summary>
        /// Типы контейнеров
        /// </summary>
        public IList<int> ContainerTypeIds { get; set; }

        /// <summary>
        /// ИД базового маршрута
        /// </summary>
        public int? BaseRouteId { get; set; }

        /// <summary>
        /// Адрес КП
        /// </summary>
        public string StandAddress { get; set; }

        public int? RouteId { get; set; }
    }
}
