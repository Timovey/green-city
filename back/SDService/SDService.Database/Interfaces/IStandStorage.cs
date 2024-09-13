using GreenCity.Http;
using SDContracts.DataContracts.Stand;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels.Stand;
using SDService.Database.Models.Contracts;
using SDService.Database.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Database.Interfaces
{
    /// <summary>
    /// Хранилище контейнерных площадок
    /// </summary>
    public interface IStandStorage
    {
        /// <summary>
        /// Получить информацию по контейнерной площадке
        /// </summary>
        /// <returns></returns>
        Task<IList<StandViewModel>> GetStandsAsync(GetStandContract getContract);

        /// <summary>
        /// Получить отфильтрованный список контейнерных площадок
        /// </summary>
        /// <param name="filter">Фильтр контейнерных площадок</param>
        /// <returns></returns>
        Task<IList<StandViewModel>> GetFilteredStandsAsync(StandFilterModel filter);

        /// <summary>
        /// Получить пагинированные кп без фильтров
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<StandViewModel>> GetStandsWithoutFilterAsync(CommonHttpFilter filter);

        /// <summary>
        /// Создание новой контейнерной площадки
        /// </summary>
        /// <param name="createContract">Новая контейнерная площадка</param>
        /// <returns> ид созданной записи </returns>
        Task<int> CreateStandAsync(CreateStandContract createContract);

        /// <summary>
        /// Обновить информацию по контейнерной площадке
        /// </summary>
        /// <param name="updateContract">Контейнерная площадка</param>
        /// <returns></returns>
        Task<bool> UpdateStandsAsync(UpdateStandContract updateContract);

        /// <summary>
        /// Удаление контейнерной площадки
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteStandsAsync(DeleteStandContract deleteContract);

        /// <summary>
        /// Получение КП для рейсов
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<(Stand Stand, IList<(Container Container, ContainerType ContainerType)> AttachedContainers)>> GetFilteredStandsForRouteAsync(
            GetFilteredStandsForRouteFilter filter
        );

        Task<IList<Stand>> GetDropDownStandsAsync(GetDropDownStandsFilterModel filter);
    }
}
