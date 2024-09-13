using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using LogistService.Contracts.DataContracts.BaseRoute;
using LogistService.Contracts.Interfaces;
using MissionService.Contracts.DataContracts.Mission;
using MissionService.Contracts.Enums;
using MissionService.Contracts.Interface;
using SDContracts.DataContracts.Stand;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels.Stand;
using SDService.Core.Extensions;
using SDService.Database.Interfaces;
using SDService.Database.Models.Contracts;
using SDService.Database.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SDService.Core.BusinessLogic
{
    public class StandLogic
    {
        private readonly IContainerStorage _containerStorage;
        private readonly IStandStorage _standStorage;
        private readonly IBaseRouteApi _baseRouteApi;
        private readonly IMissionApi _missionApi;
        private readonly IMapper _mapper;

        public StandLogic(
            IContainerStorage containerStorage,
            IStandStorage standStorage,
            IBaseRouteApi baseRouteApi,
            IMissionApi missionApi,
            IMapper mapper
        )
        {
            _containerStorage = containerStorage;
            _standStorage = standStorage;
            _baseRouteApi = baseRouteApi;
            _missionApi = missionApi;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<int>> CreateStandAsync(CreateStandContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            try
            {
                //Создание записи
                var createdId = await _standStorage.CreateStandAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(createdId, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<int>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateStandAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse> UpdateStandsAsync(UpdateStandContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _standStorage.UpdateStandsAsync(contract))
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
                    $"Ошибка выполнения метода {nameof(UpdateStandsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<StandViewModel>>> GetStandsAsync(GetStandContract contract)
        {
            if (contract?.StandIds == null || contract.StandIds.Count == 0)
                return CommonHttpHelper.BuildErrorResponse<IList<StandViewModel>>(initialError: "Пустой запрос");

            try
            {
                var stands = await _standStorage.GetStandsAsync(contract);
                if (stands == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<StandViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(stands, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<StandViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetStandsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<StandViewModel>>> GetFilteredStandsAsync(StandFilterModel filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<StandViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _standStorage.GetFilteredStandsAsync(filter) :
                    await _standStorage.GetStandsWithoutFilterAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<StandViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredStandsAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteStandsAsync(DeleteStandContract contract)
        {
            if (contract?.StandIds == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _standStorage.DeleteStandsAsync(contract))
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
                    $"Ошибка выполнения метода {nameof(DeleteStandsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<StandForRouteViewModel>>> GetFilteredStandsForRouteAsync(GetFilteredStandsForRouteFilterModel filter)
        {
            if (filter == default)
                return CommonHttpHelper.BuildErrorResponse<IList<StandForRouteViewModel>>(initialError: "Пустой запрос");

            var sortContainerIdsFromBaseRoute = new List<int>();
            if (filter.BaseRouteId.HasValue)
            {
                var getBaseRouteResult = await _baseRouteApi.GetBaseRoutesAsync(new GetBaseRouteContract
                {
                    Ids = new List<int> { filter.BaseRouteId.Value },
                    RequestId = filter.RequestId
                });

                if (!getBaseRouteResult.Success || getBaseRouteResult.Data == default || getBaseRouteResult.Data.Count == 0 || getBaseRouteResult.Data.Count > 1)
                {
                    return CommonHttpHelper.BuildErrorResponse<IList<StandForRouteViewModel>>(
                        initialError: "Ошибка при получении базовых маршрутов",
                        extErrors: getBaseRouteResult.Errors == default || getBaseRouteResult.Errors.Length == 0 ? default : getBaseRouteResult.Errors.ToList()
                    );
                }

                //!_! Сортируем по приоритету и выбираем только ИД КП
                sortContainerIdsFromBaseRoute = getBaseRouteResult.Data.Single().Stands
                    .OrderBy(x => x.Item1)
                    .Select(x => x.Item2)
                    .Except(filter.ExcludeStandIds ?? new List<int>())
                    .ToList();
            }

            //!_! Получаем доступные КП
            var standsWithContainers = (
                await _standStorage.GetFilteredStandsForRouteAsync(new GetFilteredStandsForRouteFilter
                {
                    IncludeStandIds = sortContainerIdsFromBaseRoute,
                    ExcludeStandIds = filter.ExcludeStandIds,
                    ContainerTypeIds = filter.ContainerTypeIds,
                    StandAddress = filter.StandAddress,
                })
            )
            .Where(x => x.AttachedContainers != default && x.AttachedContainers.Count > 0);

            //!_! Смотрим по каким контейнерам есть миссиии, а по каким нет, чтобы убрать лишние плоащадки
            var getAvailableContainersResult = await _missionApi.GetAvailableContainersByMissionsForRouteAsync(new GetAvailableContainersByMissionsForRouteContract
            {
                Date = filter.Date,
                ContainerIds = standsWithContainers
                    .SelectMany(x => x.AttachedContainers.Select(xx => xx.Container.Id).Distinct())
                    .Distinct()
                    .ToList(),
                MissionStatuses = new List<MissionStatus> { MissionStatus.New },
                IncludeContainersRouteId = filter.IncludeContainersRouteId
            });

            if (!getAvailableContainersResult.Success || getAvailableContainersResult.Data == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<StandForRouteViewModel>>(initialError: "Ошибка при получении контейнеров, на которые созданы миссии");
            }

            //!_! Берем только те КП, у которых есть хотябы один контейнер на который созданна миссия
            var availableStands = standsWithContainers
                .Where(x => getAvailableContainersResult.Data.Any(xx => x.AttachedContainers.Select(x => x.Container.Id).Distinct().Contains(xx)))
                //Пагинация тут, тк только после этого фильтра получаем конечные данные
                .Paging(filter.ItemsOnPage, filter.Page);

            //!_! Получаем контейнеры на которые нет миссий
            var excludeContainers = availableStands
                .SelectMany(x => x.AttachedContainers.Select(xx => xx.Container.Id))
                .Distinct()
                .Except(getAvailableContainersResult.Data);

            //!_! Сортируем найденные КП по КП БМ, если они есть
            if (sortContainerIdsFromBaseRoute.Count != 0)
            {
                availableStands =
                    from sortContainerId in sortContainerIdsFromBaseRoute
                    join stand in availableStands
                        on sortContainerId equals stand.Stand.Id
                    select stand;
            }

            return CommonHttpHelper.BuildSuccessResponse(
                CreateStandForRouteViewModel(availableStands, excludeContainers),
                HttpStatusCode.OK
            );
        }

        public async Task<CommonHttpResponse<IList<StandForRouteViewModel>>> GetStandsByRouteIdAsync(GetStandsForRouteByRouteIdContract contract)
        {
            if (contract == default)
                return CommonHttpHelper.BuildErrorResponse<IList<StandForRouteViewModel>>(initialError: "Пустой запрос");

            var containersFromMissionsResult = await _missionApi.GetContainersFromMissionsByRouteIdWithPrioritiesAsync(contract.RouteId);

            if (!containersFromMissionsResult.Success)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<StandForRouteViewModel>>(
                    initialError: "Ошибка получения информации по контейнерам рейса",
                    extErrors: containersFromMissionsResult.Errors == default ? default : containersFromMissionsResult.Errors.ToList()
                );
            }

            if (containersFromMissionsResult.Data == default ||
                containersFromMissionsResult.Data.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<StandForRouteViewModel>>(
                    initialError: "Ошибка получения информации по контейнерам рейса - контейнеры не найдены",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            //!_! Получаем информацию по всем интересующим нас КП на основании контейнеров из миссий
            var standsByContainers = await _standStorage.GetFilteredStandsForRouteAsync(
                new GetFilteredStandsForRouteFilter
                {
                    IncludeContainerIds = containersFromMissionsResult.Data.SelectMany(x => x.ContainerIds).Distinct().ToList()
                }
            );

            //!_! Здесь по сути выполняется сортировка кп, на основании приоритета миссии, проблемы могут возникнуть в том случае если
            //!_! во время нахождения миссии в рейсе, будет перевязан контейнер на другую КП, тогда в рейсе рядом окажутся две КП
            var result = (
                from containerMissionInfo in containersFromMissionsResult.Data
                from standInfo in standsByContainers.Where(x => x.AttachedContainers != default && x.AttachedContainers.Count > 0)
                    .Where(standInfoX =>
                        containerMissionInfo.ContainerIds.Any(containerMissionInfoContainerIdX =>
                            standInfoX.AttachedContainers.Select(x => x.Container.Id).Distinct().Contains(containerMissionInfoContainerIdX)
                        )
                    )
                select standInfo)
                .DistinctBy(x => x.Stand.Id);

            if (contract.StandIds != default && contract.StandIds.Count > 0)
            {
                result = (
                    from originalStandId in contract.StandIds
                    join standInfoFromDb in result.Where(x => x.AttachedContainers != default && x.AttachedContainers.Count > 0)
                        on originalStandId equals standInfoFromDb.Stand.Id
                    select standInfoFromDb)
                    .DistinctBy(x => x.Stand.Id);
            }

            return CommonHttpHelper.BuildSuccessResponse(CreateStandForRouteViewModel(result), HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<IList<StandDropDownViewModel>>> GetDropDownStandsAsync(
            GetDropDownStandsFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<StandDropDownViewModel>>(initialError: "Пустой запрос");

            var result = await _standStorage.GetDropDownStandsAsync(filter);

            return CommonHttpHelper.BuildSuccessResponse<IList<StandDropDownViewModel>>(
                result.Select(x => new StandDropDownViewModel { Id = x.Id, Address = x.Address }).ToList(),
                HttpStatusCode.OK
            );
        }

        private IList<StandForRouteViewModel> CreateStandForRouteViewModel(
            IEnumerable<(Stand Stand, IList<(Container Container, ContainerType ContainerType)> AttachedContainers)> stands,
            IEnumerable<int> excludeContainerIds = default
        )
        {
            if (excludeContainerIds == default)
                excludeContainerIds = new List<int>();

            return stands.Select(x =>
                CreateStandForRouteViewModel(x.Stand, x.AttachedContainers.Where(xx => !excludeContainerIds.Contains(xx.Container.Id)).ToList()))
                .ToList();
        }

        private StandForRouteViewModel CreateStandForRouteViewModel(
            Stand stand,
            IList<(Container Container, ContainerType ContainerType)> attachedContainers
        )
        {
            return new StandForRouteViewModel
            {
                StandId = stand.Id,
                StandAddress = stand.Address,
                ContainersInfo = attachedContainers.GetContainerInfoForRoute(),
                ContainersVolume = attachedContainers.DistinctBy(x => x.Container.Id).Sum(x => x.ContainerType.Volume)
            };
        }
    }
}
