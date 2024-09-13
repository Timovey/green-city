using GreenCity.Http;
using LogistService.Contracts.DataContracts.BaseRoute;
using LogistService.Contracts.Filters;
using LogistService.Contracts.ViewModels;
using LogistService.Database.Models.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogistService.Database.Interfaces
{
    public interface IBaseRouteStorage
    {
        /// <summary>
        /// Создание нового базового маршрута
        /// </summary>
        /// <param name="createContract">Новый базовый маршрут</param>
        /// <returns> ид созданной записи </returns>
        Task<BaseRouteViewModel> CreateBaseRouteAsync(CreateDbBaseRouteContract createContract);

        /// <summary>
        /// Получить информацию по базовому маршруту
        /// </summary>
        /// <param name="getContract"></param>
        /// <returns></returns>
        Task<IList<BaseRouteViewModel>> GetBaseRoutesAsync(GetBaseRouteContract getContract);

        /// <summary>
        /// Получить пагинированные базовые маршруты без фильтров
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<BaseRouteViewModel>> GetBaseRoutesWithoutFilterAsync(CommonHttpFilter filter);

        /// <summary>
        /// Получить отфильтрованный список базовых маршрутов
        /// </summary>
        /// <param name="filter">Фильтр базовых маршрутов</param>
        /// <returns></returns>
        Task<IList<BaseRouteViewModel>> GetFilteredBaseRoutesAsync(BaseRouteFilterModel filter);

        /// <summary>
        /// Обновить базовый маршрут
        /// </summary>
        /// <param name="updateContract">Обновленный базовый маршрут</param>
        /// <returns></returns>
        Task<BaseRouteViewModel> UpdateBaseRouteAsync(UpdateDbBaseRouteContract updateContract);

        /// <summary>
        /// Удалить базовый маршрут
        /// </summary>
        /// <param name="deleteContract">Удаляемый базовый маршрут</param>
        /// <returns></returns>
        Task<bool> DeleteBaseRoutesAsync(DeleteBaseRouteContract deleteContract);
    }
}
