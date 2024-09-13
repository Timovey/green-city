using DispatcherService.Contracts.DataContracts.Brigade;
using DispatcherService.Contracts.Filters;
using DispatcherService.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Database.Interfaces
{
    public interface IBrigadeStorage
    {
        /// <summary>
        /// Создание бригады
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, int AddedBrigadeId)> CreateBrigadeAsync(CreateBrigadeContract contract);

        /// <summary>
        /// Получить связки рейсов с бригадами по идентификаторам рейсов
        /// </summary>
        /// <param name="routeIds"></param>
        /// <returns></returns>
        Task<IList<Brigade2Route>> GetBrigade2RoutesByRouteIdsAsync(
            IList<int> routeIds
        );

        /// <summary>
        /// Получить фильтрованный список бригад
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<(Brigade Brigade, IList<AuxiliaryWorker> AuxWorkers, IList<int> RouteIds)>> GetCommonFilteredBrigadesAsync(
            CommonBrigadesFilterModel filter
        );

        /// <summary>
        /// Удаление бригады
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task<bool> DeleteBrigadesAsync(DeleteBrigadesContract contract);

        Task<IList<int>> GetRouteIdsFromBrigadesWithFilterAsync(
            DateTime shiftTaskDate
        );
    }
}
