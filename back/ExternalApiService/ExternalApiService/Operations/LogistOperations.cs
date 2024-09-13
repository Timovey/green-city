using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ExternalApi.Interface.Models.Logist.BaseRoute;
using ExternalApi.Interface.Models.Logist.Route;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using LogistService.Contracts.DataContracts.BaseRoute;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Filters;
using LogistService.Contracts.Interfaces;

namespace ExternalApiService.Operations
{
    public class LogistOperations : ILogistOperations
    {
        private readonly IBaseRouteApi _baseRouteApi;
        private readonly IRouteApi _routeApi;
        private readonly IMapper _mapper;

        public LogistOperations(
            IBaseRouteApi baseRouteApi,
            IRouteApi routeApi,
            IMapper mapper
        )
        {
            _baseRouteApi = baseRouteApi;
            _routeApi = routeApi;
            _mapper = mapper;
        }

        #region BaseRoute

        public async Task<CommonHttpResponse<ExtBaseRouteWithStandsViewModel>> CreateBaseRouteAsync(
            ExtCreateBaseRouteRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtBaseRouteWithStandsViewModel>(
                    initialError: "Пустой запрос");

            var createContract = _mapper.Map<CreateBaseRouteContract>(request);
            var result = await _baseRouteApi.CreateBaseRouteAsync(createContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtBaseRouteWithStandsViewModel>(result.Data),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtBaseRouteWithStandsViewModel>(result.StatusCode,
                result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtBaseRouteWithStandsViewModel>> GetBaseRouteAsync(
            ExtGetBaseRouteRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtBaseRouteWithStandsViewModel>(
                    initialError: "Пустой запрос");

            //Создаем контракт со списком из одного идшника для передачи в сервис логиста
            var getContract = _mapper.Map<GetBaseRouteContract>(request);
            var result = await _baseRouteApi.GetBaseRoutesAsync(getContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtBaseRouteWithStandsViewModel>(result.Data.FirstOrDefault()),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtBaseRouteWithStandsViewModel>(result.StatusCode,
                result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtBaseRouteViewModel[]>> GetFilteredBaseRoutesAsync(
            ExtBaseRouteFilterRequest filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtBaseRouteViewModel[]>(initialError: "Пустой запрос");

            var baseRouteFilter = _mapper.Map<BaseRouteFilterModel>(filter);
            var result = await _baseRouteApi.GetFilteredBaseRoutesAsync(baseRouteFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtBaseRouteViewModel>).ToArray(),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtBaseRouteViewModel[]>(result.StatusCode,
                result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtBaseRouteWithStandsViewModel>> UpdateBaseRouteAsync(
            ExtUpdateBaseRouteRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtBaseRouteWithStandsViewModel>(
                    initialError: "Пустой запрос");

            var updateContract = _mapper.Map<UpdateBaseRouteContract>(request);
            var result = await _baseRouteApi.UpdateBaseRouteAsync(updateContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtBaseRouteWithStandsViewModel>(result.Data),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtBaseRouteWithStandsViewModel>(result.StatusCode,
                result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtBaseRouteViewModel[]>> GetDropdownBaseRoutesAsync(
            ExtDropdownBaseRouteFilterRequest filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtBaseRouteViewModel[]>(initialError: "Пустой запрос");

            var baseRouteFilter = _mapper.Map<BaseRouteFilterModel>(filter);
            var result = await _baseRouteApi.GetFilteredBaseRoutesAsync(baseRouteFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtBaseRouteViewModel>).ToArray(),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtBaseRouteViewModel[]>(result.StatusCode,
                result.Errors.ToList());
        }

        public async Task<CommonHttpResponse> DeleteBaseRouteAsync(ExtDeleteBaseRouteRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteContract = _mapper.Map<DeleteBaseRouteContract>(request);
            var result = await _baseRouteApi.DeleteBaseRoutesAsync(deleteContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.ToList());
        }

        #endregion

        #region Route

        public async Task<CommonHttpResponse<ExtRouteViewModel>> CreateRouteAsync(
            ExtCreateRouteRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtRouteViewModel>(
                    initialError: "Пустой запрос");

            var createContract = _mapper.Map<CreateRouteContract>(request);
            var result = await _routeApi.CreateRouteAsync(createContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtRouteViewModel>(result.Data),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtRouteViewModel>(result.StatusCode,
                result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtFilteredRouteViewModel[]>> GetFilteredRoutesAsync(
            ExtRouteFilterRequest filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<ExtFilteredRouteViewModel[]>(initialError: "Пустой запрос");

            var routeFilter = _mapper.Map<RouteFilterModel>(filter);
            var result = await _routeApi.GetFilteredRoutesAsync(routeFilter);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data.Select(_mapper.Map<ExtFilteredRouteViewModel>).ToArray(),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtFilteredRouteViewModel[]>(result.StatusCode,
                result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtRouteViewModel>> GetRouteAsync(
            ExtGetRouteRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtRouteViewModel>(
                    initialError: "Пустой запрос");

            //Создаем контракт со списком из одного идшника для передачи в сервис логиста
            var getContract = _mapper.Map<GetRouteContract>(request);
            var result = await _routeApi.GetRoutesAsync(getContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtRouteViewModel>(result.Data.FirstOrDefault()),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtRouteViewModel>(result.StatusCode,
                result.Errors.ToList());
        }

        public async Task<CommonHttpResponse<ExtRouteViewModel>> UpdateRouteAsync(
            ExtUpdateRouteRequest request
        )
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse<ExtRouteViewModel>(
                    initialError: "Пустой запрос");

            var updateContract = _mapper.Map<UpdateRouteContract>(request);
            var result = await _routeApi.UpdateRouteAsync(updateContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    _mapper.Map<ExtRouteViewModel>(result.Data),
                    HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<ExtRouteViewModel>(result.StatusCode,
                result.Errors.ToList());
        }

        public async Task<CommonHttpResponse> DeleteRouteAsync(ExtDeleteRouteRequest request)
        {
            if (request == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            var deleteContract = _mapper.Map<DeleteRouteContract>(request);
            var result = await _routeApi.DeleteRouteAsync(deleteContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse(result.StatusCode, result.Errors.ToList());
        }

        #endregion
    }
}