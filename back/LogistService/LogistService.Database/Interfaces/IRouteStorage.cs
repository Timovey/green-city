using System.Collections.Generic;
using System.Threading.Tasks;
using GreenCity.Http;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Filters;
using LogistService.Contracts.ViewModels;

namespace LogistService.Database.Interfaces
{
    public interface IRouteStorage
    {
        /// <summary>
        /// Создание нового рейса
        /// </summary>
        /// <param name="createContract">Новый рейс</param>
        /// <returns> созданная запись </returns>
        Task<RouteViewModel> CreateRouteAsync(CreateRouteContract createContract);

        /// <summary>
        /// Получить пагинированные рейсы без фильтров
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<FilteredRouteViewModel>> GetRoutesWithoutFilterAsync(CommonHttpFilter filter);

        /// <summary>
        /// Получить отфильтрованный список рейсов
        /// </summary>
        /// <param name="filter">Фильтр рейсов</param>
        /// <returns></returns>
        Task<IList<FilteredRouteViewModel>> GetFilteredRoutesAsync(RouteFilterModel filter);

        /// <summary>
        /// Получить информацию по рейсу
        /// </summary>
        /// <param name="getContract"></param>
        /// <returns></returns>
        Task<IList<RouteViewModel>> GetRoutesAsync(GetRouteContract getContract);

        /// <summary>
        /// Обновить рейс
        /// </summary>
        /// <param name="updateContract">Обновленный рейс</param>
        /// <returns></returns>
        Task<RouteViewModel> UpdateRouteAsync(UpdateRouteContract updateContract);

        /// <summary>
        /// Удалить рейс
        /// </summary>
        /// <param name="deleteContract">Удаляемый рейс</param>
        /// <returns></returns>
        Task<bool> DeleteRouteAsync(DeleteRouteContract deleteContract);
        
        /// <summary>
        /// Закрыть рейс
        /// </summary>
        /// <param name="closeContract">рейс</param>
        /// <returns></returns>
        Task<bool> CloseRouteAsync(CloseRouteContract closeContract);

        /// <summary>
        /// Получить список рейсов по ид экипажей
        /// </summary>
        /// <param name="getContract"></param>
        /// <returns></returns>
        Task<IList<GetRoutesByWorkShiftsIdsViewModel>> GetRoutesByWorkShiftsIdsAsync(GetRoutesByWorkShiftsIdsContract getContract);
    }
}