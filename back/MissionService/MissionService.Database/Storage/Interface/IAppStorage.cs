using MissionService.Contracts.Filters.App;
using MissionService.Database.Models.Contracts;
using MissionService.Database.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionService.Database.Storage.Interface
{
    public interface IAppStorage
    {
        Task<(bool IsSuccess, int CreatedAppId)> CreateAppAsync(CreateAppDbContract dbContract);

        /// <summary>
        /// Получить Идентификаторы миссий которые нельзя удалять при удалении заявки
        /// </summary>
        /// <param name="appsToDelete"></param>
        /// <returns> Идентификаторы миссий </returns>
        Task<IList<int>> GetMissionsThanCantBeDeletedAsync(
            IList<int> appsToDelete
        );

        /// <summary>
        /// Удалить заявку
        /// </summary>
        /// <param name="appIds"> Идентификаторы заявок </param>
        /// <param name="excludeMissionIds"> Миссии которые нельзя удалять </param>
        /// <param name="deleteUserId"></param>
        /// <param name="deleteUserName"></param>
        /// <returns></returns>
        Task<bool> DeleteAppsAsync(
            IList<int> appIds,
            IList<int> excludeMissionIds,
            int deleteUserId = 0,
            string deleteUserName = nameof(AppStorage)
        );

        /// <summary>
        /// Получить реальные заявки по фильтру - без группировок
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<(App App, byte MissionStatus, DateTime MissionDate)>> GetFilteredAppsAsync(
            CommonAppFilterModel filter
        );

        /// <summary>
        /// Получение заявок по идентификаторам, с прикрепленными миссиями
        /// </summary>
        /// <param name="appIds"></param>
        /// <returns></returns>
        Task<IList<(App App, IList<Mission> AttachedMissions)>> GetAppsByIdAsync(
            IList<int> appIds
        );

        /// <summary>
        /// Обновление заявки
        /// </summary>
        /// <param name="appId"> Идентификатор заявки </param>
        /// <param name="missionsToDelete"> Идентификаторы миссий которые нужно удалить </param>
        /// <param name="newMissionIds"> Идентификаторы миссий, которые нужно связать с заявкой </param>
        /// <param name="appDate"> Новая дата заявки </param>
        /// <param name="changeUserId"> Ид юзера изменившего запись </param>
        /// <param name="changeUserName"> Имя пользователя изменившего запись </param>
        /// <returns></returns>
        Task<bool> UpdateAppAsync(
            int appId,
            IList<int> missionsToDelete,
            IList<int> newMissionIds,
            DateTime? appDate =  default,
            int changeUserId = 0,
            string changeUserName = nameof(AppStorage)
        );

        /// <summary>
        /// Получить короткую информацию по заявкам для автогенерации миссий с заявками
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<App>> GetFilteredAppsForAutoGenerationAsync(
            AppsForAutoGenerationFilterModel filter
        );
    }
}
