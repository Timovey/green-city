using GreenCity.Http;
using MissionService.Contracts.DataContracts.Mission;
using MissionService.Contracts.Enums;
using MissionService.Contracts.Filters.Mission;
using MissionService.Contracts.ViewModels.Mission;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionService.Core.BusinessLogic.Interface
{
    public interface IMissionLogic
    {
        /// <summary>
        /// Связать контейнеры с миссиями, создает новые миссии если их еще нету, объединяя с существующими миссиями
        /// </summary>
        /// <param name="date"> Дата миссии </param>
        /// <param name="containerIds"> ИД контейнеров </param>
        /// <param name="creatorType"> Тип создателя </param>
        /// <returns> Возвращает идентификаторы миссий </returns>
        Task<(bool IsSuccess, IList<int> MissionIds, string ErrorMessage)> BindMissionsByContainers(DateTime date, IList<int> containerIds, CreatorType creatorType);

        /// <summary>
        /// Генерация миссии с карты
        /// </summary>
        /// <param name="containerId"> ИД контейнера </param>
        /// <param name="date"> Дата миссии </param>
        /// <param name="creatorType"> Тип создателя записи </param>
        /// <param name="userId"> ИД пользователя </param>
        /// <param name="userName"> Имя пользователя </param>
        /// <returns></returns>
        Task<int> CreateMissionFromMapAsync(
            int containerId,
            DateTime date,
            CreatorType creatorType,
            int userId = 0,
            string userName = "system"
        );

        /// <summary>
        /// Получение идентификаторы контейнеров упоминающихся в миссиях 
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task<CommonHttpResponse<IList<int>>> GetAvailableContainersByMissionsForRouteAsync(
            GetAvailableContainersByMissionsForRouteContract contract
        );

        /// <summary>
        /// Привязка миссий к рейсу
        /// </summary>
        /// <param name="contract"></param>
        Task<CommonHttpResponse> BindMissionsToRouteByStandsAsync(
            BindMissionsToRouteByStandsContract contract
        );

        /// <summary>
        /// Отвязка миссий от рейса
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns></returns>
        Task<CommonHttpResponse> UnbindMissionsFromRouteAsync(int routeId);

        /// <summary>
        /// Получить идентификаторы контейнеров сгруппированные и отсортированные (ASC) по приоритетам, по ИД рейса
        /// </summary>
        /// <param name="routeId"> ИД рейса </param>
        /// <returns></returns>
        Task<CommonHttpResponse<IList<ContainersFromMissionsWithPrioritiesViewModel>>> GetContainersFromMissionsByRouteIdWithPrioritiesAsync(
            int routeId
        );

        /// <summary>
        /// Обновление связок миссий с рейсом
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task<CommonHttpResponse> UpdateMissionBindingsByRouteIdAsync(
            UpdateMissionBindingsByRouteIdContract contract
        );

        /// <summary>
        /// Массовое обновление миссий при закрытии рейса
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task<CommonHttpResponse> UpdateMissionsForCloseRouteAsync(
            UpdateMissionsForCloseRouteContract contract
        );

        /// <summary>
        /// Получение миссий в расширенном виде по рейсу
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns></returns>
        Task<CommonHttpResponse<MissionsForDispatcherViewModel>> GetMissionsForDispatcherByRouteIdAsync(int routeId);

        /// <summary>
        /// Получение информации по миссиям рейса для генерации отчетов
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns></returns>
        Task<CommonHttpResponse<MissionsForBuildShiftTaskReportViewModel>> GetMissionsForBuildShiftTaskReportByRouteIdAsync(int routeId);

        /// <summary>
        /// Получение информации по миссиям рейса для генерации журнала движения мусоровозов
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns></returns>
        Task<CommonHttpResponse<MissionsForBuildDoneTaskListReportViewModel>> GetMissionsForBuildDoneTaskListReportByRouteIdAsync(
            int routeId
        );

        /// <summary>
        /// Провал миссий
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<CommonHttpResponse> FailMissionsAsync(DateTime date);

        /// <summary>
        /// Получить миссии по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<CommonHttpResponse<IList<CommonMissionViewModel>>> GetCommonFilteredMissionsAsync(CommonMissionFilterModel filter);
    }
}
