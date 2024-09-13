using SDContracts.DataContracts.MissionInfo;
using SDService.Database.Models.DbModels;
using SDService.Database.Models.ViewModels.Container;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Database.Interfaces
{
    public interface IMissionInfoStorage
    {
        /// <summary>
        /// Получение информации для формирования миссий
        /// </summary>
        /// <param name="contractClientIds"> Идентификаторы контрагентов </param>
        /// <param name="standIds"> Идентификаторы КП </param>
        /// <param name="containerIds"> Идентификаторы контейнеров </param>
        /// <param name="containerTypeIds"> Идентификаторы типов емкостей </param>
        /// <param name="contractIds"> Идентификаторы договоров </param>
        /// <param name="date"> Дата </param>
        /// <returns></returns>
        Task<IList<InfoForMissionModel>> GetFilteredContainersForFilterMissionsAsync(
            IList<int> contractClientIds = default,
            IList<int> standIds = default,
            IList<int> containerIds = default,
            IList<int> containerTypeIds = default,
            IList<int> contractIds = default,
            DateTime? date = default
        );

        /// <summary>
        /// Создание фейковых данных для информации по миссии
        /// </summary>
        /// <param name="createContract"></param>
        /// <returns> ИД фейкового контейнера и договора </returns>
        Task<(bool IsSuccess, int FakeContainerId, int ContractClientId, int ContractId)> CreateFakeDataAsync(
            FullDataModelForCreateFakeMissionInfoContract createContract
        );

        /// <summary>
        /// Получить информацию по КА и договоры по ИД
        /// </summary>
        /// <param name="contractClientIds"></param>
        /// <param name="contractIds"></param>
        /// <returns></returns>
        Task<(IList<ContractClient> ContractClients, IList<Contract> Contracts)> GetContractClientsWithContractsAsync(
            IList<int> contractClientIds,
            IList<int> contractIds
        );

        /// <summary>
        /// Получить информацию по КП для миссий
        /// </summary>
        /// <param name="containerIds"></param>
        /// <returns></returns>
        Task<IList<(Stand Stand, decimal ContainerVolume)>> GetStandsForMissionInfoAsync(IList<int> containerIds);
    }
}
