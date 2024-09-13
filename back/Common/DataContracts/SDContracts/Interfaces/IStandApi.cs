using GreenCity.Http;
using Refit;
using SDContracts.DataContracts.Stand;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels.Stand;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDContracts.Interfaces
{
    public interface IStandApi
    {
        [Post("/Stand/GetFilteredStands")]
        Task<CommonHttpResponse<IList<StandViewModel>>> GetFilteredStandsAsync(
            [Body] StandFilterModel filter
        );

        [Post("/Stand/GetStands")]
        Task<CommonHttpResponse<IList<StandViewModel>>> GetStandsAsync(
           [Body] GetStandContract getContract
        );

        [Post("/Stand/CreateStand")]
        Task<CommonHttpResponse<int>> CreateStandAsync(
            [Body] CreateStandContract createContract
        );

        [Post("/Stand/UpdateStands")]
        Task<CommonHttpResponse> UpdateStandsAsync(
           [Body] UpdateStandContract updateContract
        );

        [Post("/Stand/DeleteStands")]
        Task<CommonHttpResponse> DeleteStandsAsync(
           [Body] DeleteStandContract deleteContract
        );

        /// <summary>
        /// Получение фильтрованного списка КП для создания рейса
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Post("/Stand/GetFilteredStandsForRoute")]
        Task<CommonHttpResponse<IList<StandForRouteViewModel>>> GetFilteredStandsForRouteAsync(
            [Body] GetFilteredStandsForRouteFilterModel filter
        );

        /// <summary>
        /// Получение КП по рейсу с необязательным фильтром по КП
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/Stand/GetStandsByRouteId")]
        Task<CommonHttpResponse<IList<StandForRouteViewModel>>> GetStandsByRouteIdAsync(
           [Body] GetStandsForRouteByRouteIdContract contract
        );

        [Post("/Stand/GetDropDownStands")]
        Task<CommonHttpResponse<IList<StandDropDownViewModel>>> GetDropDownStandsAsync(
           [Body] GetDropDownStandsFilterModel filter
        );
    }
}
