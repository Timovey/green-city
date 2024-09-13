using GreenCity.Http;
using System.Collections.Generic;

namespace SDContracts.DataContracts.Container
{
    /// <summary>
    /// Получить идентификаторы контейнеров для рейса
    /// </summary>
    public class GetFilteredContainerIdsForRouteContract : CommonHttpContract
    {
        public IList<int> StandIds { get; set; }

        public IList<int> ContainerTypeIds { get; set; }
    }
}
