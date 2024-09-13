using System.Collections.Generic;
using GreenCity.Http;

namespace LogistService.Contracts.DataContracts.Route
{
    /// <summary> Контракт на получение рейсов </summary>
    public class GetRouteContract : CommonHttpContract
    {
        /// <summary> ИД рейсов </summary>
        public IList<int> Ids { get; set; }

        /// <summary> Делать ли запросы для сбора сведений о площадках рейса </summary>
        public bool NeedAttachStands { get; set; } = true;
    }
}