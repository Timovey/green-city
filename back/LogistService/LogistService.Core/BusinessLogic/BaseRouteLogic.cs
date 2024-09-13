using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using LogistService.Contracts.DataContracts.BaseRoute;
using LogistService.Contracts.Filters;
using LogistService.Contracts.SortModels;
using LogistService.Contracts.ViewModels;
using LogistService.Database.Interfaces;
using LogistService.Database.Models.Contracts;

namespace LogistService.Core.BusinessLogic
{
    public class BaseRouteLogic
    {
        private readonly IBaseRouteStorage _baseRouteStorage;
        private readonly IMapper _mapper;

        public BaseRouteLogic(
            IBaseRouteStorage baseRouteStorage,
            IMapper mapper
        )
        {
            _baseRouteStorage = baseRouteStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<BaseRouteViewModel>> CreateBaseRouteAsync(CreateBaseRouteContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<BaseRouteViewModel>(initialError: "Пустой запрос");

            try
            {
                //!_! Ищем БМ с названием контракта
                var baseRoutes = await _baseRouteStorage.GetFilteredBaseRoutesAsync(
                    new BaseRouteFilterModel()
                    {
                        Name = contract.Name,
                        IsFilterByCreatedUser = false,
                        Sorting = new BaseRouteSortModel()
                    }
                );

                //!_! Проверяем соответствие названий через Equals
                if (baseRoutes != null && baseRoutes.Count > 0 &&
                    baseRoutes.FirstOrDefault(x => x.Name.Equals(contract.Name)) != default)
                {
                    return CommonHttpHelper.BuildErrorResponse<BaseRouteViewModel>(initialError:
                        "Базовый маршрут с таким названием уже есть");
                }
                
                //!_! Проверяем наличие КП
                if (contract.Stands == default || contract.Stands.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<BaseRouteViewModel>(initialError:
                        "Не выбраны контейнерные площадки");
                }

                //Создаем контракт для добавления в БД
                var dbContract = _mapper.Map<CreateDbBaseRouteContract>(contract);
                
                //Добавляем в контракт список КП с расставленными приоритетами
                dbContract.StandIdsWithPriorities = CreatePriorityList(contract.Stands);

                //Создание записи
                var result = await _baseRouteStorage.CreateBaseRouteAsync(dbContract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<BaseRouteViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateBaseRouteAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<IList<BaseRouteViewModel>>> GetBaseRoutesAsync(GetBaseRouteContract contract)
        {
            if (contract?.Ids == null || contract.Ids.Count == 0)
                return CommonHttpHelper.BuildErrorResponse<IList<BaseRouteViewModel>>(initialError: "Пустой запрос");

            try
            {
                var baseRoutes = await _baseRouteStorage.GetBaseRoutesAsync(contract);

                if (baseRoutes == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<BaseRouteViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(baseRoutes, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<BaseRouteViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetBaseRoutesAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<BaseRouteViewModel>>> GetFilteredBaseRoutesAsync(BaseRouteFilterModel filter)
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<BaseRouteViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _baseRouteStorage.GetFilteredBaseRoutesAsync(filter) :
                    await _baseRouteStorage.GetBaseRoutesWithoutFilterAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<BaseRouteViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredBaseRoutesAsync)}");
            }
        }

        public async Task<CommonHttpResponse<BaseRouteViewModel>> UpdateBaseRouteAsync(UpdateBaseRouteContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<BaseRouteViewModel>(initialError: "Пустой запрос");

            try
            {
                //!_! Ищем БМ с названием контракта
                var baseRoutes = await _baseRouteStorage.GetFilteredBaseRoutesAsync(
                    new BaseRouteFilterModel()
                    {
                        Name = contract.Name,
                        IsFilterByCreatedUser = false,
                        Sorting = new BaseRouteSortModel()
                    }
                );

                //!_! Проверяем соответствие названий через Equals
                if (baseRoutes != null && baseRoutes.Count > 0)                    
                {
                    var element = baseRoutes.FirstOrDefault(x => x.Name.Equals(contract.Name));
                    if (element != default && element.Id != contract.Id)
                    {
                        return CommonHttpHelper.BuildErrorResponse<BaseRouteViewModel>(initialError:
                        "Базовый маршрут с таким названием уже есть");
                    }
                }
                
                //!_! Проверяем наличие КП
                if (contract.Stands == default || contract.Stands.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<BaseRouteViewModel>(initialError:
                        "Не выбраны контейнерные площадки");
                }

                //Создаем контракт для добавления в БД
                var dbContract = _mapper.Map<UpdateDbBaseRouteContract>(contract);
                //Добавляем в контракт список КП с расставленными приоритетами
                dbContract.StandIdsWithPriorities = CreatePriorityList(contract.Stands);

                //Обновление записи
                var result = await _baseRouteStorage.UpdateBaseRouteAsync(dbContract);
                
                if (result == null)
                    return CommonHttpHelper.BuildErrorResponse<BaseRouteViewModel>(HttpStatusCode.NotFound, null, "Запись не найдена");
                
                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<BaseRouteViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(UpdateBaseRouteAsync)} ReqId : {contract.RequestId}");
            }
        }
        
        public async Task<CommonHttpResponse> DeleteBaseRoutesAsync(DeleteBaseRouteContract contract)
        {
            if (contract?.Ids == null || contract.Ids.Count == 0)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");
            try
            {
                if (await _baseRouteStorage.DeleteBaseRoutesAsync(contract))
                {
                    return CommonHttpHelper.BuildSuccessResponse(HttpStatusCode.OK);
                }
                
                return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена");
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(DeleteBaseRoutesAsync)}");
            }
        }

        private IList<(int priority, int standId)> CreatePriorityList(IList<int> standsIds)
        {
            var priorityList = new List<(int priority, int standId)>();

            if (standsIds == default || standsIds.Count == 0)
                return priorityList;

            int counter = 1;
            foreach (var standId in standsIds)
            {
                priorityList.Add((counter++, standId));
            }

            return priorityList;
        }
    }
}
