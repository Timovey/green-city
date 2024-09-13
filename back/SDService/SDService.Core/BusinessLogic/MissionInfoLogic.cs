using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using SDContracts.DataContracts.MissionInfo;
using SDContracts.SpecificModels.ContainerStand;
using SDContracts.ViewModels;
using SDContracts.ViewModels.MissionInfo;
using SDContracts.ViewModels.Stand;
using SDService.Core.Extensions;
using SDService.Database.Interfaces;
using SDService.Database.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SDService.Core.BusinessLogic
{
    public class MissionInfoLogic
    {
        private readonly IMissionInfoStorage _missionInfoStorage;
        private readonly IMapper _mapper;

        public MissionInfoLogic(
            IMissionInfoStorage missionInfoStorage,
            IMapper mapper
        )
        {
            _missionInfoStorage = missionInfoStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<IEnumerable<MissionInfoViewModel>>> GetFilteredMissionInfoAsync(
            GetFilteredMissionInfoContract contract
        )
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<IEnumerable<MissionInfoViewModel>>(initialError: "Пустой запрос");

            var missionInfos = await _missionInfoStorage.GetFilteredContainersForFilterMissionsAsync(
                contract.ContractClientIds,
                contract.StandIds,
                contract.ContainerIds,
                contract.ContainerTypeIds,
                contract.ContractIds,
                contract.Date
            );

            var result = missionInfos.BuildMissionInfo(_mapper);

            return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<CreateFakeDataForMissionInfoResponse>> CreateFakeDataForMissionInfoAsync(
            CreateFakeDataForMissionInfoContract createContract
        )
        {
            if (createContract == null)
                return CommonHttpHelper.BuildErrorResponse<CreateFakeDataForMissionInfoResponse>(initialError: "Пустой запрос");

            try
            {
                var createFakeDataResult = await _missionInfoStorage.CreateFakeDataAsync(createContract.BuildDataForFakeMissionInfo());

                if (!createFakeDataResult.IsSuccess)
                    return CommonHttpHelper.BuildErrorResponse<CreateFakeDataForMissionInfoResponse>(
                        HttpStatusCode.InternalServerError,
                        default,
                        $"Ошибка выполнения метода {nameof(CreateFakeDataForMissionInfoAsync)}");

                return CommonHttpHelper.BuildSuccessResponse(
                    new CreateFakeDataForMissionInfoResponse
                    {
                        FakeContainerId = createFakeDataResult.FakeContainerId,
                        ContractId = createFakeDataResult.ContractId,
                        ContractClientId = createFakeDataResult.ContractClientId
                    },
                    HttpStatusCode.OK
                );
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<CreateFakeDataForMissionInfoResponse>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateFakeDataForMissionInfoAsync)} ReqId : {createContract.RequestId}");
            }

        }

        public async Task<CommonHttpResponse<ContractsWithContractClientsShortViewModel>> GetContractsWithContractClientsShortInfoAsync(
           IList<int> contractClientIds,
           IList<int> contractIds
        )
        {
            var dbResult = await _missionInfoStorage.GetContractClientsWithContractsAsync(contractClientIds, contractIds);

            return CommonHttpHelper.BuildSuccessResponse(
                new ContractsWithContractClientsShortViewModel
                {
                    ContractClientShortInfo = GetContractClientShortInfoDict(dbResult.ContractClients),
                    ContractShortInfo = GetContractShortInfoDict(dbResult.Contracts)
                },
                HttpStatusCode.OK
            );
        }

        public async Task<CommonHttpResponse<IList<StandForMissionInfoViewModel>>> GetStandsForMissionInfoAsync(IList<int> containerIds)
        {
            if (containerIds == default || containerIds.Count == 0)
                return CommonHttpHelper.BuildErrorResponse<IList<StandForMissionInfoViewModel>>(initialError: "Пустой запрос");

            var stands = await _missionInfoStorage.GetStandsForMissionInfoAsync(containerIds);

            if (stands == default || stands.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<StandForMissionInfoViewModel>>(
                    initialError: "Данные по КП не найдены",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            var result = new List<StandForMissionInfoViewModel>();
            foreach (var stand in stands)
            {
                var coordinatesList = stand.Stand.CoordinatesJson.SafeDeserialize<StandCoordinatesModel>(null);
                if (coordinatesList == null)
                    continue;

                var coordinates = coordinatesList.Coordinates.FirstOrDefault();
                if (coordinates == null)
                    continue;

                result.Add(new StandForMissionInfoViewModel
                {
                    Longitude = coordinates.Longitude,
                    Latitude = coordinates.Latitude,
                    CustomVolume = stand.ContainerVolume
                });
            }

            if (result.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<StandForMissionInfoViewModel>>(
                    initialError: "Данные по КП не найдены",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            return CommonHttpHelper.BuildSuccessResponse<IList<StandForMissionInfoViewModel>>(result);
        }

        private IDictionary<int, ContractClientShortInfo> GetContractClientShortInfoDict(IList<ContractClient> contractClients)
        {
            return contractClients
                .GroupBy(x => x.Id)
                .ToDictionary(x => x.Key, value => value.Select(x => new ContractClientShortInfo { ContractClientName = x.ClientName }).FirstOrDefault());
        }

        private IDictionary<int, ContractShortInfo> GetContractShortInfoDict(IList<Contract> contracts)
        {
            return contracts
                .GroupBy(x => x.Id)
                .ToDictionary(x => x.Key, value => value.Select(x => new ContractShortInfo { ContractNumber = x.ContractNumber }).FirstOrDefault());
        }
    }
}
