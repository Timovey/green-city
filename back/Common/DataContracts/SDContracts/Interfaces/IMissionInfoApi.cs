using GreenCity.Http;
using Refit;
using SDContracts.DataContracts.MissionInfo;
using SDContracts.ViewModels;
using SDContracts.ViewModels.MissionInfo;
using SDContracts.ViewModels.Stand;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDContracts.Interfaces
{
    public interface IMissionInfoApi
    {
        /// <summary>
        /// Получение информации из АО, для миссий
        /// </summary>
        /// <param name="contractClientIds"></param>
        /// <param name="standIds"></param>
        /// <param name="containerIds"></param>
        /// <param name="containerTypeIds"></param>
        /// <param name="contractIds"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [Post("/MissionInfo/GetFilteredMissionInfo")]
        Task<CommonHttpResponse<IEnumerable<MissionInfoViewModel>>> GetFilteredMissionInfoAsync(
           [Body] GetFilteredMissionInfoContract contract
        );

        /// <summary>
        /// Создание фейковых данных для миссии с карты
        /// </summary>
        /// <param name="createContract"> Минимально необходимая информация для создания фейковых данных </param>
        /// <returns> Идентификаторы фейковых данных </returns>
        [Post("/MissionInfo/CreateFakeDataForMissionInfo")]
        Task<CommonHttpResponse<CreateFakeDataForMissionInfoResponse>> CreateFakeDataForMissionInfoAsync(
          [Body] CreateFakeDataForMissionInfoContract createContract
        );

        /// <summary>
        /// Получить короткую информацию по КА и по договорам
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [Post("/MissionInfo/GetContractsWithContractClientsShortInfo")]
        Task<CommonHttpResponse<ContractsWithContractClientsShortViewModel>> GetContractsWithContractClientsShortInfoAsync(
          [Body] GetContractsWithContractClientsShortInfoContract contract
        );

        /// <summary>
        /// Получить информацию по КП для миссий
        /// </summary>
        /// <param name="containerIds"></param>
        /// <returns></returns>
        [Post("/MissionInfo/GetStandsForMissionInfo")]
        Task<CommonHttpResponse<IList<StandForMissionInfoViewModel>>> GetStandsForMissionInfoAsync(
           [Body] IList<int> containerIds
        );
    }
}
