using System.Collections.Generic;
using GreenCity.Http;

namespace LogistService.Contracts.DataContracts.Route
{
    /// <summary> Контракт на получение рейсов по Экипажам </summary>
    public class GetRoutesByWorkShiftsIdsContract : CommonHttpContract
    {
        /// <summary> ИД экипажей </summary>
        public IList<int> WorkShiftsIds { get; set; }
    }
}