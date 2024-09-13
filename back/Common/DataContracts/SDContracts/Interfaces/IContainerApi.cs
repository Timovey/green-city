using GreenCity.Http;
using Refit;
using SDContracts.DataContracts.Container;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels;
using SDContracts.ViewModels.Container;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDContracts.Interfaces
{
    public interface IContainerApi
    {
        [Post("/Container/GetFilteredContainer")]
        Task<CommonHttpResponse<IList<ContainerViewModel>>> GetFilteredContainerAsync(
            [Body] ContainerFilterModel filter
        );

        [Post("/Container/GetContainer")]
        Task<CommonHttpResponse<IList<ContainerViewModel>>> GetContainerAsync(
            [Body] GetContainerContract getContract
        );

        [Post("/Container/CreateContainer")]
        Task<CommonHttpResponse<int>> CreateContainerAsync(
            [Body] CreateContainerContract createContract
        );

        [Post("/Container/UpdateContainer")]
        Task<CommonHttpResponse> UpdateContainerAsync(
            [Body] UpdateContainerContract updateContract
        );

        [Post("/Container/DeleteContainer")]
        Task<CommonHttpResponse> DeleteContainerAsync(
            [Body] DeleteContainerContract deleteContract
        );

        [Post("/Container/GetActualContainerTypes")]
        Task<CommonHttpResponse<IList<ContainerTypeViewModel>>> GetActualContainerTypesAsync();
        
        [Post("/Container/GetContainerTypesByIds")]
        Task<CommonHttpResponse<IList<ContainerTypeViewModel>>> GetContainerTypesByIdsAsync(
            [Body] IList<int> containerTypeIds
        );

        /// <summary>
        /// Получить идентификаторы контейнеров для рейса
        /// </summary>
        /// <param name="contract"></param>
        /// <returns> Словарь, ключ - ИД КП, значение - подходящие под фильтр идентификаторы контейнеров </returns>
        [Post("/Container/GetFilteredContainerIdsForRoute")]
        Task<CommonHttpResponse<IDictionary<int, IList<int>>>> GetFilteredContainerIdsForRouteAsync(
            [Body] GetFilteredContainerIdsForRouteContract contract
        );

        [Post("/Container/GetDropDownContainers")]
        Task<CommonHttpResponse<IList<ContainerDropDownV2ViewModel>>> GetDropDownContainersAsync(
           [Body] GetDropDownContainersFilterModel filter
        );
    }
}
