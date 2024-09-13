using GreenCity.Http;
using MissionService.Contracts.DataContracts.App;
using MissionService.Contracts.Filters.App;
using MissionService.Contracts.ViewModels.App;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionService.Core.BusinessLogic.Interface
{
    public interface IAppLogic
    {
        /// <summary>
        /// Получить миссию с карты по ИД
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        Task<CommonHttpResponse<AppFromMapViewModel>> GetFromMapAppAsync(int appId);

        /// <summary>
        /// Получить реальные заявки по идентификаторам
        /// </summary>
        /// <param name="appIds"> Идентификаторы заявок </param>
        /// <returns></returns>
        Task<CommonHttpResponse<IList<AppByContractClientViewModel>>> GetRealAppsByIdsAsync(IList<int> appIds);

        /// <summary>
        /// Получение списка заявок созданных на основе КА и договоров
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<CommonHttpResponse<IList<AppCommonViewModel>>> GetCommonFilteredAppsAsync(
            CommonAppFilterModel filter
        );

        /// <summary>
        /// Получить короткую информацию по заявкам для автогенерации
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<CommonHttpResponse<IList<AppsForAutoGenerationViewModel>>> GetFilteredAppsForAutoGenerationAsync(
            AppsForAutoGenerationFilterModel filter
        );

        /// <summary>
        /// Создание заявки на основе договоров и КА из АО
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task<CommonHttpResponse<int>> CreateAppByContractClientAsync(CreateAppByContractClientContract contract);

        /// <summary>
        /// Создание заявки с карты с необязательной привязкой КА и договоров
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task<CommonHttpResponse<int>> CreateAppFromMapAsync(CreateAppFromMapContract contract);

        /// <summary>
        /// Удалить заявки
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task<CommonHttpResponse> DeleteAppsAsync(DeleteAppsContract contract);

        /// <summary>
        /// Отредактировать заявку
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task<CommonHttpResponse> UpdateAppByContractClientAsync(
            UpdateAppByContractClient contract
        );

        /// <summary>
        /// Перегенерация заявок на основе договоров и КА
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task<CommonHttpResponse> ReGenerateAppsAsync(
            RunAppGenerationInSingleThreadContract contract
        );
    }
}
