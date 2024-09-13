using MissionService.Contracts.DataContracts.Mission;
using MissionService.Contracts.Enums;
using MissionService.Contracts.Filters.Mission;
using MissionService.Database.Models.Contracts;
using MissionService.Database.Models.DbModels;
using MissionService.Database.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionService.Database.Storage.Interface
{
    public interface IMissionStorage
    {
        /// <summary> Создание множества реальных миссий </summary>
        Task<(bool IsSuccess, IList<int> MissionIds)> CreateRealMissionsAsync(
            IList<CreateOneRealMissionContract> missionsContracts);

        /// <summary> Создание миссии с карты </summary>
        Task<int> CreateMissionFromMapAsync(CreateOneMissionFromMapDbContract missionContract);

        /// <summary> Получение идентификаторов контейнеров упоминающихся в миссиях </summary>
        Task<IList<int>> GetAvailableContainersByMissionsForRouteAsync(
            GetAvailableContainersByMissionsForRouteContract contract,
            IList<MissionStatus> includeInRouteMissionStatuses = default
        );

        /// <summary> Привязка миссий к рейсу </summary>
        Task<bool> BindMissionsToRouteByContainersAsync(BindMissionsToRouteDbContract contract);

        /// <summary> Отвязать миссии от рейса </summary>
        Task<bool> UnbindMissionsFromRouteAsync(int routeId, IList<int> missionIds = default);

        /// <summary> Получить контейнеры с приоритетами по рейсам из миссий </summary>
        Task<IList<(int Priority, int ContainerId)>> GetContainerIdsWithPrioritiesByRouteIdAsync(int routeId);

        /// <summary> Обновить связки миссий с рейсом </summary>
        Task<bool> UpdateMissionRouteBindingsAsync(
            UpdateBindingsMissionsToRouteDbContract contract
        );

        /// <summary> Массовое обновление миссий при закрытии рейса </summary>
        Task<bool> UpdateMissionsForCloseRouteAsync(
            IList<UpdateOneMissionForCloseRouteContract> contracts,
            IList<int> missionIdsToUnbind = default,
            int changeUserId = 0,
            string changeUserName = nameof(MissionStorage)
        );

        /// <summary> Получение информации по миссиям, через идентификатор рейса </summary>
        Task<IList<MissionViewModel>> GetMissionsByRouteIdAsync(
            int routeId,
            IList<MissionStatus> missionStatuses = default
        );

        /// <summary> Получение информации по миссиям, через идентификатор рейса в связке с заявками </summary>
        Task<IEnumerable<(Mission Mission, IEnumerable<App> Apps)>> GetMissionsByRouteIdWithAppInfoAsync(
            int routeId,
            IList<MissionStatus> missionStatuses = default
        );

        /// <summary> Получение миссий на дату по фильтру </summary>
        Task<IList<MissionViewModel>> GetFilteredMissionsOnDateAsync(
            DateTime date,
            IList<CreatorType> creatorTypes = default,
            IList<MissionStatus> statuses = default,
            IList<int> containerIds = default
        );

        /// <summary> Получение информации по миссиям, которые не находятся в статусе Done или Failed,
        /// дата которых меньше чем текущая дата. Смена статусов данных миссий.</summary>
        Task<bool> FailMissionsAsync(DateTime date, int changeUserId = 0,
            string changeUserName = nameof(MissionStorage)
        );

        /// <summary>
        /// Получить миссии по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<(IList<(Mission Mission, string MissionComment)> Missions, IList<App> Apps)> GetCommonFilteredMissionsAsync(
            CommonMissionFilterModel filter
        );
    }
}