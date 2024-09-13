using AutoMapper;
using ExternalApi.Interface.Models.Mission.App;
using ExternalApi.Interface.Models.Mission.Mission;
using ExternalApiService.Extensions;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using MissionService.Contracts.DataContracts.App;
using MissionService.Contracts.Enums;
using MissionService.Contracts.Filters.App;
using MissionService.Contracts.Filters.Mission;
using MissionService.Contracts.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExternalApiService.Operations
{
    public class AppOperations : IAppOperations
    {
        private readonly IAppApi _appApi;
        private readonly IMissionApi _missionApi;
        private readonly IMapper _mapper;

        public AppOperations(
            IAppApi appApi,
            IMissionApi missionApi,
            IMapper mapper
        )
        {
            _appApi = appApi;
            _missionApi = missionApi;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<ExtAppFromMapViewModel>> GetFromMapAppAsync(ExtGetFromMapAppRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtAppFromMapViewModel>(initialError: "Пустой запрос");

            var result = await _appApi.GetFromMapAppAsync(request.Id);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtAppFromMapViewModel>(result.Data),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtAppFromMapViewModel>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtAppByContractClientViewModel>> GetRealAppByIdAsync(ExtGetRealAppByIdRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtAppByContractClientViewModel>(initialError: "Пустой запрос");

            var result = await _appApi.GetRealAppsByIdsAsync(new List<int> { request.Id });

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtAppByContractClientViewModel>).FirstOrDefault(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtAppByContractClientViewModel>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtAppCommonViewModel[]>> GetCommonFilteredAppsAsync(ExtCommonAppFilterRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtAppCommonViewModel[]>(initialError: "Пустой запрос");

            var filter = _mapper.Map<CommonAppFilterModel>(request);
            var result = await _appApi.GetCommonFilteredAppsAsync(filter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtAppCommonViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtAppCommonViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtCreateAppByContractClientResponse>> CreateAppByContractClientAsync(
            ExtCreateAppByContractClientRequest request,
            ExtAppActionsPermission appActionPermission,
            ExtCreatorType creatorType
        )
        {
            if (request == default)
                return CommonHttpHelper.BuildErrorResponse<ExtCreateAppByContractClientResponse>(initialError: "Пустой запрос");

            if (request.Containers == default || request.Containers.Count == 0)
                return CommonHttpHelper.BuildErrorResponse<ExtCreateAppByContractClientResponse>(initialError: "Необходимо выбрать хотябы один контейнер");

            var contract = _mapper.Map<CreateAppByContractClientContract>(request);
            contract.ContainerIds = request.Containers.Select(x => x.Id).Distinct().ToList();
            contract.CreatePermission = (AppActionsPermission)appActionPermission;
            contract.CreatorType = (CreatorType)creatorType;

            var result = await _appApi.CreateAppByContractClientAsync(contract);

            if (result.Success)
            {
                var response = _mapper.Map<ExtCreateAppByContractClientResponse>(request);
                response.Id = result.Data;
                return CommonHttpHelper.BuildSuccessResponse(
                    response,
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtCreateAppByContractClientResponse>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtCreateAppFromMapResponse>> CreateAppFromMapAsync(
            ExtCreateAppFromMapRequest request,
            ExtAppActionsPermission appActionPermission,
            ExtCreatorType creatorType
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtCreateAppFromMapResponse>(initialError: "Пустой запрос");

            var contract = _mapper.Map<CreateAppFromMapContract>(request);
            contract.CreatePermission = (AppActionsPermission)appActionPermission;
            contract.CreatorType = (CreatorType)creatorType;

            var result = await _appApi.CreateAppFromMapAsync(contract);

            if (result.Success)
            {
                var response = _mapper.Map<ExtCreateAppFromMapResponse>(request);
                response.Id = result.Data;
                return CommonHttpHelper.BuildSuccessResponse(
                    response,
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtCreateAppFromMapResponse>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtUpdateAppByContractClientResponse>> UpdateAppByContractClientAsync(
            ExtUpdateAppByContractClientRequest request,
            ExtAppActionsPermission appActionPermission,
            ExtCreatorType updaterType
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtUpdateAppByContractClientResponse>(initialError: "Пустой запрос");

            if (request.Containers == default || request.Containers.Count == 0)
                return CommonHttpHelper.BuildErrorResponse<ExtUpdateAppByContractClientResponse>(initialError: "Необходимо выбрать хотябы один контейнер");

            var contract = _mapper.Map<UpdateAppByContractClient>(request);
            contract.ContainerIds = request.Containers.Select(x => x.Id).Distinct().ToList();
            contract.UpdatePermission = (AppActionsPermission)appActionPermission;
            contract.UpdaterType = (CreatorType)updaterType;

            var result = await _appApi.UpdateAppByContractClientAsync(contract);

            if (result.Success)
            {
                var response = _mapper.Map<ExtUpdateAppByContractClientResponse>(request);
                return CommonHttpHelper.BuildSuccessResponse(
                    response,
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtUpdateAppByContractClientResponse>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> DeleteAppByIdAsync(
            ExtDeleteAppsRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var contract = _mapper.Map<DeleteAppsContract>(request);
            contract.AppIds = new List<int> { request.Id };

            var result = await _appApi.DeleteAppsAsync(contract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<IList<ExtCommonMissionViewModel>>> GetCommonFilteredMissionsAsync(
            ExtCommonMissionFilterRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<IList<ExtCommonMissionViewModel>>(initialError: "Пустой запрос");

            var contract = _mapper.Map<CommonMissionFilterModel>(request);

            var result = await _missionApi.GetCommonFilteredMissionsAsync(contract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse<IList<ExtCommonMissionViewModel>>(
                    result.Data.Select(_mapper.Map<ExtCommonMissionViewModel>).ToList(),
                    HttpStatusCode.OK
                );
            }

            return CommonHttpHelper.BuildErrorResponse<IList<ExtCommonMissionViewModel>>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> ReGenerateAppsAsync(
            ExtRunAppGenerationInSingleThreadRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var contract = _mapper.Map<RunAppGenerationInSingleThreadContract>(request);

            var result = await _appApi.ReGenerateAppsAsync(contract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }
    }
}
