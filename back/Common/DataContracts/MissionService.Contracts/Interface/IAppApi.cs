using GreenCity.Http;
using MissionService.Contracts.DataContracts.App;
using MissionService.Contracts.Filters.App;
using MissionService.Contracts.ViewModels.App;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionService.Contracts.Interface
{
    public interface IAppApi
    {
        /// <summary>
        /// Получить миссию с карты по ИД
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [Post("/App/GetFromMapApp")]
        Task<CommonHttpResponse<AppFromMapViewModel>> GetFromMapAppAsync(
           int appId
        );

        /// <summary>
        /// Получить по идентификаторам реальные миссии
        /// </summary>
        /// <param name="appIds"></param>
        /// <returns></returns>
        [Post("/App/GetRealAppsByIds")]
        Task<CommonHttpResponse<IList<AppByContractClientViewModel>>> GetRealAppsByIdsAsync(
           IList<int> appIds
        );

        /// <summary>
        /// Получение списка заявок созданных на основе КА и договоров
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/App/GetCommonFilteredApps")]
        Task<CommonHttpResponse<IList<AppCommonViewModel>>> GetCommonFilteredAppsAsync(
           [Body] CommonAppFilterModel filter
        );

        /// <summary>
        /// Создание заявки на основе договоров и КА из АО
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/App/CreateAppByContractClient")]
        Task<CommonHttpResponse<int>> CreateAppByContractClientAsync(
           [Body] CreateAppByContractClientContract contract
        );

        /// <summary>
        /// Создание заявки с карты с необязательной привязкой КА и договоров
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/App/CreateAppFromMap")]
        Task<CommonHttpResponse<int>> CreateAppFromMapAsync(
           [Body] CreateAppFromMapContract contract
        );

        /// <summary>
        /// Удалить заявки
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/App/DeleteApps")]
        Task<CommonHttpResponse> DeleteAppsAsync(
           [Body] DeleteAppsContract contract
        );

        /// <summary>
        /// Обновление заявки
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/App/UpdateAppByContractClient")]
        Task<CommonHttpResponse> UpdateAppByContractClientAsync(
           [Body] UpdateAppByContractClient contract
        );

        /// <summary>
        /// Перегенерация заявок на основе договоров и КА
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/App/ReGenerateApps")]
        Task<CommonHttpResponse> ReGenerateAppsAsync(
            RunAppGenerationInSingleThreadContract contract
        );
    }
}
