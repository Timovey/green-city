using GreenCity.Http;
using SDContracts.DataContracts.ContractClient;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels;
using SDService.Database.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Database.Interfaces
{
    /// <summary>
    /// Хранилище контрагентов
    /// </summary>
    public interface IContractClientStorage
    {
        /// <summary>
        /// Получить информацию по контрагенту
        /// </summary>
        /// <param name="contractClientId">ИД контрагента</param>
        /// <returns></returns>
        Task<ContractClientViewModel> GetContractClientAsync(int contractClientId);

        /// <summary>
        /// Получить контрагентов без фильтрации
        /// </summary>
        /// <param name="filter">Базовый фильтр</param>
        /// <returns></returns>
        Task<IList<ContractClientViewModel>> GetContractClientsWithoutFilterAsync(CommonHttpFilter filter);

        /// <summary>
        /// Получить отфильтрованный список контрагентов
        /// </summary>
        /// <param name="filter">Фильтр контрагентов</param>
        /// <returns></returns>
        Task<IList<ContractClientViewModel>> GetFilteredContractClientsAsync(ContractClientFilterModel filter);

        /// <summary>
        /// Создание нового контрагента
        /// </summary>
        /// <param name="contractClient">Новый контрагент</param>
        /// <returns></returns>
        Task<int> CreateContractClientAsync(CreateContractClientContract contractClient);

        /// <summary>
        /// Обновить информацию по контрагенту
        /// </summary>
        /// <param name="contractClient">Контрагент</param>
        /// <returns></returns>
        Task<bool> UpdateContractClientAsync(UpdateContractClientContract contractClient);

        /// <summary>
        /// Удаление контрагента
        /// </summary>
        /// <param name="deleteContract">Контракт на удаление</param>
        /// <returns></returns>
        Task<bool> DeleteContractClientAsync(DeleteContractClientContract deleteContract);

        /// <summary>
        /// Проврить наличие информации по контрагнету по его ИД
        /// </summary>
        /// <param name="id"> ИД контрагента </param>
        /// <returns></returns>
        Task<bool> CheckContracClientById(int id);

        /// <summary>
        /// Получить КА для выпадающего списка
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<ContractClient>> GetDropDownContracClientAsync(GetDropDownContractClientFilterModel filter);
    }
}
