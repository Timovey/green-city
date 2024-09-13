using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using SDContracts.DataContracts.Container;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels;
using SDContracts.ViewModels.Container;
using SDService.Core.BusinessLogic.Interface;
using SDService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SDService.Core.BusinessLogic
{
    public class ContainerLogic : IContainerLogic
    {
        private readonly IContainerStorage _containerStorage;
        private readonly IMapper _mapper;

        public ContainerLogic(
            IContainerStorage containerStorage,
            IMapper mapper
        )
        {
            _containerStorage = containerStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<int>> CreateContainerAsync(CreateContainerContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            try
            {
                var result = await _containerStorage.CreateContainerAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<int>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateContainerAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse> UpdateContainersAsync(UpdateContainerContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _containerStorage.UpdateContainersAsync(contract))
                {
                    return CommonHttpHelper.BuildSuccessResponse();
                }

                return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена");
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(UpdateContainersAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<ContainerViewModel>>> GetContainersAsync(
            GetContainerContract contract
        )
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<IList<ContainerViewModel>>(initialError: "Пустой запрос");

            try
            {
                var transport = await _containerStorage.GetContainersAsync(contract);
                if (transport == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<ContainerViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(transport, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<ContainerViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetContainersAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<ContainerViewModel>>> GetFilteredContainersAsync(
            ContainerFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<ContainerViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _containerStorage.GetFilteredContainersAsync(filter) :
                    await _containerStorage.GetContainersWithoutFilterAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<ContainerViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredContainersAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteContainersAsync(DeleteContainerContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _containerStorage.DeleteContainersAsync(contract))
                {
                    return CommonHttpHelper.BuildSuccessResponse();
                }

                return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена");
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(DeleteContainersAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<ContainerTypeViewModel>>> GetActualContainerTypesAsync()
        {
            try
            {
                var result = await _containerStorage.GetActualContainerTypesAsync();

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<ContainerTypeViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetActualContainerTypesAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<ContainerTypeViewModel>>> GetContainerTypesByIdsAsync(IList<int> containerTypeIds)
        {
            try
            {
                var result = await _containerStorage.GetContainerTypesByIdsAsync(containerTypeIds);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<ContainerTypeViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetContainerTypesByIdsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IDictionary<int, IList<int>>>> GetFilteredContainerIdsForRouteAsync(
            GetFilteredContainerIdsForRouteContract contract
        )
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<IDictionary<int, IList<int>>>(initialError: "Пустой запрос");

            var resultFromDb = await _containerStorage.GetFilteredContainerIdsForRouteGroupedByStandsAsync(contract);

            if (resultFromDb.Count == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IDictionary<int, IList<int>>>(
                    initialError: "Контейнеры по таким площадкам с такими типами не найдены",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            Func<List<int>, IList<int>> toIList = list => list;
            var result = resultFromDb
                .GroupBy(x => x.StandId)
                .ToDictionary(x => x.Key, value => toIList(value.Select(x => x.ContainerId).Distinct().ToList()));

            return CommonHttpHelper.BuildSuccessResponse<IDictionary<int, IList<int>>>(result, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<IList<ContainerDropDownV2ViewModel>>> GetDropDownContainersAsync(
            GetDropDownContainersFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<ContainerDropDownV2ViewModel>>(initialError: "Пустой запрос");

            var result = await _containerStorage.GetDropDownContainersAsync(filter);

            return CommonHttpHelper.BuildSuccessResponse<IList<ContainerDropDownV2ViewModel>>(
                result
                    .Where(x => x.Container != default && x.ContainerType != default)
                    .Select(x => new ContainerDropDownV2ViewModel { Id = x.Container.Id, Name = $"{x.Container.Name} {x.ContainerType.Name}" }).ToList(),
                HttpStatusCode.OK
            );
        }
    }
}
