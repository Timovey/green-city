using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System;
using System.Collections.Generic;

namespace SDContracts.Filter
{
    /// <summary>
    /// Получение КП для формирования рейсов
    /// </summary>
    public class GetFilteredStandsForRouteFilterModel : CommonHttpFilter
    {
        /// <summary>
        /// Дата миссий
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// ИД КП, которые не нужно брать в выборку
        /// </summary>
        public IList<int> ExcludeStandIds { get; set; }

        /// <summary>
        /// Типы контейнеров
        /// </summary>
        [GCRequired]
        public IList<int> ContainerTypeIds { get; set; }

        /// <summary>
        /// ИД базового маршрута
        /// </summary>
        public int? BaseRouteId { get; set; }

        /// <summary>
        /// Адрес КП
        /// </summary>
        public string StandAddress { get; set; }

        /// <summary>
        /// ИД рейса для которого добавить миссии внезависимости от статуса (для редактирования рейсов нужно)
        /// </summary>
        public int? IncludeContainersRouteId { get; set; }
    }
}
