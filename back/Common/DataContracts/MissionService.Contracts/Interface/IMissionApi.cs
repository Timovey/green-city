using GreenCity.Http;
using MissionService.Contracts.DataContracts.Mission;
using MissionService.Contracts.Filters.Mission;
using MissionService.Contracts.ViewModels.Mission;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionService.Contracts.Interface
{
    public interface IMissionApi
    {
        /// <summary>
        /// Получить доступные КП для добавления в рейс на основе миссиий
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/Mission/GetAvailableContainersByMissionsForRoute")]
        Task<CommonHttpResponse<IList<int>>> GetAvailableContainersByMissionsForRouteAsync(
            [Body] GetAvailableContainersByMissionsForRouteContract contract
        );

        /// <summary>
        /// Связать миссии с рейсом на основе КП
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/Mission/BindMissionsToRouteByStands")]
        Task<CommonHttpResponse> BindMissionsToRouteByStandsAsync(
            [Body] BindMissionsToRouteByStandsContract contract
        );

        /// <summary>
        /// Отвязать миссии от рейса
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/Mission/UnbindMissionsFromRoute")]
        Task<CommonHttpResponse> UnbindMissionsFromRouteAsync(
           int routeId
        );

        /// <summary>
        /// Получить идентификаторы контейнеров сгруппированные и отсортированные (ASC) по приоритетам, по ИД рейса
        /// </summary>
        /// <param name="routeId"> ИД рейса </param>
        /// <returns></returns>
        [Post("/Mission/GetContainersFromMissionsByRouteIdWithPriorities")]
        Task<CommonHttpResponse<IList<ContainersFromMissionsWithPrioritiesViewModel>>> GetContainersFromMissionsByRouteIdWithPrioritiesAsync(
           int routeId
        );

        /// <summary>
        /// Обновление связок миссий с рейсом
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/Mission/UpdateMissionBindingsByRouteId")]
        Task<CommonHttpResponse> UpdateMissionBindingsByRouteIdAsync(
           [Body] UpdateMissionBindingsByRouteIdContract contract
        );

        /// <summary>
        /// Массовое обновление миссий при закрытии рейса
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/Mission/UpdateMissionsForCloseRoute")]
        Task<CommonHttpResponse> UpdateMissionsForCloseRouteAsync(
           [Body] UpdateMissionsForCloseRouteContract contract
        );

        /// <summary>
        /// Получение информации по миссия в расширенном виде по рейсу, для диспетчера
        /// </summary>
        /// <param name="routeId"> ИД рейса </param>
        /// <returns></returns>
        [Post("/Mission/GetMissionsForDispatcherByRouteId")]
        Task<CommonHttpResponse<MissionsForDispatcherViewModel>> GetMissionsForDispatcherByRouteIdAsync(
            int routeId
        );

        /// <summary>
        /// Получение информации по миссиям рейса для генерации отчетов
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns></returns>
        [Post("/Mission/GetMissionsForBuildShiftTaskReportByRouteId")]
        Task<CommonHttpResponse<MissionsForBuildShiftTaskReportViewModel>> GetMissionsForBuildShiftTaskReportByRouteIdAsync(
            int routeId
        );

        /// <summary>
        /// Получение информации по миссиям рейса для генерации журнала движения мусоровозов
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns></returns>
        [Post("/Mission/GetMissionsForBuildDoneTaskListReportByRouteId")]
        Task<CommonHttpResponse<MissionsForBuildDoneTaskListReportViewModel>> GetMissionsForBuildDoneTaskListReportByRouteIdAsync(
            int routeId
        );

        /// <summary>
        /// Получить миссии по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Post("/Mission/GetCommonFilteredMissions")]
        Task<CommonHttpResponse<IList<CommonMissionViewModel>>> GetCommonFilteredMissionsAsync(
           [Body] CommonMissionFilterModel filter
        );
    }
}
