using GreenCity.Http;
using SDContracts.DataContracts.Container;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Core.BusinessLogic.Interface
{
    public interface IContainerLogic
    {
        /// <summary>
        /// Получить идентификаторы контейнеров для рейса
        /// </summary>
        /// <param name="contract"></param>
        /// <returns> Словарь, ключ - ИД КП, значение - подходящие под фильтр идентификаторы контейнеров </returns>
        Task<CommonHttpResponse<IDictionary<int, IList<int>>>> GetFilteredContainerIdsForRouteAsync(
            GetFilteredContainerIdsForRouteContract contract
        );
    }
}
