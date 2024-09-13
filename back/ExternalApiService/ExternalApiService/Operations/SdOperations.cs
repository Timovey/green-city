using AutoMapper;
using ExternalApi.Interface.Models.Mission.App;
using ExternalApi.Interface.Models.Sd.Container;
using ExternalApi.Interface.Models.Sd.Contract;
using ExternalApi.Interface.Models.Sd.ContractClient;
using ExternalApi.Interface.Models.Sd.Stand;
using ExternalApiService.Extensions;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using SDContracts.DataContracts.Container;
using SDContracts.DataContracts.Contract;
using SDContracts.DataContracts.ContractClient;
using SDContracts.DataContracts.Stand;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.Interfaces;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExternalApiService.Operations
{
    internal class SdOperations : ISdOperations
    {
        private readonly IStandApi _standApi;
        private readonly IContractClientApi _contractClientApi;
        private readonly IContractApi _contractApi;
        private readonly IContainerApi _containerApi;
        //private readonly IMissionApi _missionApi;
        private readonly IMapper _mapper;

        public SdOperations(
            IContractClientApi contractClientApi,
            IStandApi standApi,
            IContractApi contractApi,
            IContainerApi containerApi,
            // missionApi,
            IMapper mapper
        )
        {
            _contractClientApi = contractClientApi;
            _contractApi = contractApi;
            _standApi = standApi;
            _containerApi = containerApi;
            //_missionApi = missionApi;
            _mapper = mapper;
        }

        #region Contract client

        public async Task<CommonHttpResponse<ExtContractClientViewModel[]>> GetFilteredContractClientAsync(ExtContractClientFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtContractClientViewModel[]>(initialError: "Пустой запрос");

            var contractFilter = _mapper.Map<ContractClientFilterModel>(filter);
            var result = await _contractClientApi.GetFilteredContractClientAsync(contractFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtContractClientViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtContractClientViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtContractClientViewModel>> GetContractClientAsync(ExtGetContractClientRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtContractClientViewModel>(initialError: "Пустой запрос");

            var getContract = _mapper.Map<GetContractClientContract>(request);
            var result = await _contractClientApi.GetContractClientAsync(getContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(_mapper.Map<ExtContractClientViewModel>(result.Data), System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtContractClientViewModel>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<int>> CreateContractClientAsync(ExtCreateContractClientRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            var createContract = _mapper.Map<CreateContractClientContract>(request);
            var result = await _contractClientApi.CreateContractClientAsync(createContract);

            if (result.Success)
            {
                return result;
            }

            return CommonHttpHelper.BuildErrorResponse<int>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> UpdateContractClientAsync(ExtUpdateContractClientRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var updateContract = _mapper.Map<UpdateContractClientContract>(request);
            var result = await _contractClientApi.UpdateContractClientAsync(updateContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> DeleteContractClientAsync(ExtDeleteContractClientRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteContract = _mapper.Map<DeleteContractClientContract>(request);
            var result = await _contractClientApi.DeleteContractClientAsync(deleteContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtContractClientDropDownViewModel[]>> GetDropdownContractClientAsync(
            ExtGetDropDownContractClientFilterRequest filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtContractClientDropDownViewModel[]>(initialError: "Пустой запрос");

            var contractFilter = _mapper.Map<ContractClientFilterModel>(filter);
            var result = await _contractClientApi.GetFilteredContractClientAsync(contractFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(x => new ExtContractClientDropDownViewModel { Id = x.ContractClientId, Name = x.ClientName }).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtContractClientDropDownViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtContractClientDropDownViewModel[]>> GetDropDownContractClientV2Async(
            ExtGetDropDownContractClientFilterRequest filter,
            ExtAppType extAppType
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtContractClientDropDownViewModel[]>(initialError: "Пустой запрос");

            var contractFilter = _mapper.Map<GetDropDownContractClientFilterModel>(filter);
            contractFilter.EntityTypes = extAppType.GetAvailableEntityTypes();
            var result = await _contractClientApi.GetDropDownContractClientAsync(contractFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtContractClientDropDownViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtContractClientDropDownViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        #endregion

        #region Containers

        public async Task<CommonHttpResponse<ExtContainerViewModel[]>> GetFilteredContainerAsync(ExtContainerFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtContainerViewModel[]>(initialError: "Пустой запрос");

            var contractFilter = _mapper.Map<ContainerFilterModel>(filter);
            var result = await _containerApi.GetFilteredContainerAsync(contractFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtContainerViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtContainerViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtContainerViewModel[]>> GetContainersAsync(ExtGetContainerRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtContainerViewModel[]>(initialError: "Пустой запрос");

            var getContract = _mapper.Map<GetContainerContract>(request);
            var result = await _containerApi.GetContainerAsync(getContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtContainerViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtContainerViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<int>> CreateContainerAsync(ExtCreateContainerRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            var createContract = _mapper.Map<CreateContainerContract>(request);
            var result = await _containerApi.CreateContainerAsync(createContract);

            if (result.Success)
            {
                return result;
            }

            return CommonHttpHelper.BuildErrorResponse<int>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> UpdateContainerAsync(ExtUpdateContainerRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var updateContract = _mapper.Map<UpdateContainerContract>(request);
            var result = await _containerApi.UpdateContainerAsync(updateContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> DeleteContainerAsync(ExtDeleteContainerRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteContract = _mapper.Map<DeleteContainerContract>(request);
            var result = await _containerApi.DeleteContainerAsync(deleteContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtContainerTypeViewModel[]>> GetActualContainerTypesAsync()
        {
            var result = await _containerApi.GetActualContainerTypesAsync();

            if (result.Success && result.Data != default)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtContainerTypeViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtContainerTypeViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtContainerDropDownV2ViewModel[]>> GetDropDownContainersAsync(ExtGetDropDownContainersFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtContainerDropDownV2ViewModel[]>(initialError: "Пустой запрос");

            var contractFilter = _mapper.Map<GetDropDownContainersFilterModel>(filter);
            var result = await _containerApi.GetDropDownContainersAsync(contractFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtContainerDropDownV2ViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtContainerDropDownV2ViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        #endregion

        #region Stands

        public async Task<CommonHttpResponse<ExtStandViewModel[]>> GetFilteredStandsAsync(ExtStandFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtStandViewModel[]>(initialError: "Пустой запрос");

            var contractFilter = _mapper.Map<StandFilterModel>(filter);
            var result = await _standApi.GetFilteredStandsAsync(contractFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtStandViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtStandViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtStandForBaseRouteViewModel[]>> GetFilteredStandsForBaseRouteAsync(ExtStandForBaseRouteFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtStandForBaseRouteViewModel[]>(initialError: "Пустой запрос");

            var contractFilter = _mapper.Map<StandFilterModel>(filter);
            var result = await _standApi.GetFilteredStandsAsync(contractFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtStandForBaseRouteViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtStandForBaseRouteViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtOneStandViewModel[]>> GetStandsAsync(ExtGetStandRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtOneStandViewModel[]>(initialError: "Пустой запрос");

            var getContract = _mapper.Map<GetStandContract>(request);
            var result = await _standApi.GetStandsAsync(getContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtOneStandViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtOneStandViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<int>> CreateStandAsync(ExtCreateStandRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            var createContract = _mapper.Map<CreateStandContract>(request);
            var result = await _standApi.CreateStandAsync(createContract);

            if (result.Success)
            {
                return result;
            }

            return CommonHttpHelper.BuildErrorResponse<int>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> UpdateStandsAsync(ExtUpdateStandRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var updateContract = _mapper.Map<UpdateStandContract>(request);
            var result = await _standApi.UpdateStandsAsync(updateContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> DeleteStandsAsync(ExtDeleteStandRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteContract = _mapper.Map<DeleteStandContract>(request);
            var result = await _standApi.DeleteStandsAsync(deleteContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtStandDropDownViewModel[]>> GetDropDownStandsAsync(ExtGetDropDownStandsFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtStandDropDownViewModel[]>(initialError: "Пустой запрос");

            var contractFilter = _mapper.Map<GetDropDownStandsFilterModel>(filter);
            var result = await _standApi.GetDropDownStandsAsync(contractFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtStandDropDownViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtStandDropDownViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtStandForRouteViewModel[]>> GetFilteredStandsForRouteAsync(
            ExtGetFilteredStandsForRouteFilterRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtStandForRouteViewModel[]>(initialError: "Пустой запрос");

            var contract = _mapper.Map<GetFilteredStandsForRouteFilterModel>(request);
            var result = await _standApi.GetFilteredStandsForRouteAsync(contract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtStandForRouteViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtStandForRouteViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtStandForRouteViewModel[]>> GetStandsByRouteIdAsync(
            ExtGetStandsForRouteByRouteIdRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtStandForRouteViewModel[]>(initialError: "Пустой запрос");

            var contract = _mapper.Map<GetStandsForRouteByRouteIdContract>(request);
            var result = await _standApi.GetStandsByRouteIdAsync(contract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtStandForRouteViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtStandForRouteViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        #endregion

        #region Contracts

        public async Task<CommonHttpResponse<ExtContractViewModel[]>> GetFilteredContractsAsync(ExtContractFilterRequest filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtContractViewModel[]>(initialError: "Пустой запрос");

            var contractFilter = _mapper.Map<ContractFilterModel>(filter);
            var result = await _contractApi.GetFilteredContractAsync(contractFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtContractViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtContractViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtContractViewModel>> GetContractAsync(ExtGetContractRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtContractViewModel>(initialError: "Пустой запрос");

            var getContract = _mapper.Map<GetContractContract>(request);
            var result = await _contractApi.GetContractAsync(getContract);

            if (result.Success)
            {
                var final = _mapper.Map<ExtContractViewModel>(result.Data);
                return CommonHttpHelper.BuildSuccessResponse(
                    final,
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtContractViewModel>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<int>> CreateContractAsync(ExtCreateContractRequest request, ClaimsPrincipal user)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            //!_! Валидируем договор графика
            var validationResult = request.AttachedContainers.ValidateContractSchedule(user);

            if (!validationResult.IsSuccess)
                return CommonHttpHelper.BuildErrorResponse<int>(
                    statusCode: HttpStatusCode.BadRequest,
                    initialError: validationResult.Error
                );

            var createContract = _mapper.Map<CreateContractContract>(request);

            var result = await _contractApi.CreateContractAsync(createContract);

            if (result.Success)
            {
                return result;
            }

            return CommonHttpHelper.BuildErrorResponse<int>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<int>> CreateContractForTestDataAsync(ExtCreateContractRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            var createContract = _mapper.Map<CreateContractContract>(request);

            var result = await _contractApi.CreateContractAsync(createContract);

            if (result.Success)
            {
                return result;
            }

            return CommonHttpHelper.BuildErrorResponse<int>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> BulkUpdateContractsAsync(ExtBulkUpdateContractRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var updateContract = _mapper.Map<BulkUpdateContractContract>(request);
            var result = await _contractApi.BulkUpdateContractAsync(updateContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> UpdateContractsAsync(ExtUpdateContractRequest request, ClaimsPrincipal user)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            //!_! Валидируем договор графика
            var validationResult = request.AttachedContainers.ValidateContractSchedule(user);

            if (!validationResult.IsSuccess)
                return CommonHttpHelper.BuildErrorResponse<int>(
                    statusCode: HttpStatusCode.BadRequest,
                    initialError: validationResult.Error
                );

            var updateContract = _mapper.Map<UpdateContractContract>(request);

            if (request.AttachedContainers != null && request.AttachedContainers.Count > 0)
            {
                updateContract.AttachedContainers = request.AttachedContainers
                    .Select(_mapper.Map<CreateContainer2ScheduleContract>)
                    .ToList();
            }

            var result = await _contractApi.UpdateContractAsync(updateContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse> DeleteContractsAsync(ExtDeleteContractRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteContract = _mapper.Map<DeleteContractContract>(request);
            var result = await _contractApi.DeleteContractAsync(deleteContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        public async Task<CommonHttpResponse<ExtContractDropDownViewModel[]>> GetDropdownContractAsync(
            ExtGetDropDownContractsFilterRequest filter,
            ExtAppType extAppType
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtContractDropDownViewModel[]>(initialError: "Пустой запрос");

            var contractFilter = _mapper.Map<GetDropDownContractsFilterModel>(filter);
            contractFilter.EntityTypes = extAppType.GetAvailableEntityTypes();
            var result = await _contractApi.GetDropDownContractsAsync(contractFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtContractDropDownViewModel>).ToArray(),
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtContractDropDownViewModel[]>(result.StatusCode, result.Errors.GetErrorsOrDefault());
        }

        #endregion       
    }
}
