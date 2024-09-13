using SDContracts.DataContracts.Contract;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels;
using SDService.Database.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Database.Interfaces
{
    /// <summary>
    /// Хранилище договоров
    /// </summary>
    public interface IContractStorage
    {
        /// <summary>
        /// Получить договор
        /// </summary>
        /// <returns></returns>
        Task<ContractViewModel> GetContractAsync(GetContractContract getContract);

        /// <summary>
        /// Получить отфильтрованный список договоров
        /// </summary>
        /// <param name="filter">Фильтр договоров</param>
        /// <returns></returns>
        Task<IList<ContractViewModel>> GetFilteredContractsAsync(ContractFilterModel filter);

        /// <summary>
        /// Создание нового договора
        /// </summary>
        /// <returns></returns>
        Task<int> CreateContractAsync(CreateContractContract createContract);

        /// <summary>
        /// Обновить договор
        /// </summary>
        /// <returns></returns>
        Task<bool> UpdateContractAsync(UpdateContractContract updateContract);

        /// <summary>
        /// Массовое обновление договора
        /// </summary>
        /// <returns></returns>
        Task<bool> BulkUpdateContractAsync(BulkUpdateContractContract bulkUpdateContract);

        /// <summary>
        /// Удаление договоров
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteContractsAsync(DeleteContractContract deleteContract);

        /// <summary>
        /// Получение списка договоров без фильтра
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<ContractViewModel>> GetContractsWithoutFilterAsync(ContractFilterModel filter);

        /// <summary>
        /// Получить договоры с контейнерами (аналог GetContract только можно получить множество договоров)
        /// </summary>
        /// <param name="contractIds">ИД договоров</param>
        /// <param name="createdUserId"> ИД пользователя создавшего запись </param>
        /// <returns></returns>
        Task<IList<ContractViewModel>> GetContractsWithGroupedContainersAsync(
            IList<int> contractIds,
            int? createdUserId = null
        );

        Task<IList<Contract>> GetDropDownContractsAsync(GetDropDownContractsFilterModel filter);
    }
}
