using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using GreenCity.Http;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Filters;
using LogistService.Contracts.ViewModels;
using LogistService.Database.Interfaces;
using LogistService.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LogistService.Database.Implements
{
    public class RouteStorage : IRouteStorage
    {
        private readonly LogistDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly ILogger<RouteStorage> _logger;
        private readonly IMapper _mapper;

        public RouteStorage(
            LogistDbContext context,
            FillHistoryService fillHistoryService,
            ILogger<RouteStorage> logger,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<RouteViewModel> CreateRouteAsync(CreateRouteContract createContract)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Создаем рейс
                    var route = _mapper.Map<Route>(createContract);
                    //!_! Ищем последний идшник на указанную дату
                    var lastRouteNumber = GetRoutesOnDateAsync(createContract.Date).Result.FirstOrDefault()
                        ?.RouteNumberOnDay;
                    //!_! Присваиваем порядковый номер рейсу (если на указанную дату нет рейсов, ставим 1) 
                    route.RouteNumberOnDay = lastRouteNumber.HasValue ? lastRouteNumber.Value + 1 : 1;

                    await _context.AddAsync(route);
                    await _context.SaveChangesAsync();

                    //!_! Добавляем исторические записи
                    await _fillHistoryService.FillHistoryAsync<Route, RouteHistory>(
                        new List<Route> { route },
                        _context,
                        createContract.UserId,
                        createContract.UserName
                    );

                    await transaction.CommitAsync();

                    var routeViewModel = _mapper.Map<RouteViewModel>(route);
                    routeViewModel.StandIds = createContract.StandIds;
                    return routeViewModel;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while creating Route with date {createContract.Date}");
                    await transaction.RollbackAsync();
                    return default;
                }
            }
        }

        public async Task<IList<FilteredRouteViewModel>> GetRoutesWithoutFilterAsync(CommonHttpFilter filter)
        {
            var data = await _context.Routes
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Id)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            return data.Select(_mapper.Map<FilteredRouteViewModel>).ToList();
        }

        public async Task<IList<FilteredRouteViewModel>> GetFilteredRoutesAsync(RouteFilterModel filter)
        {
            if (filter == null)
                return null;

            var data = await _context.Routes
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Date.Date == filter.Date.Date)
                .WhereIf(filter.IsReady.HasValue, x => x.RouteReady == filter.IsReady.Value)
                .WhereIf(!filter.TransportWorkShiftIds.IsNullOrEmpty() && filter.TransportWorkShiftIds.Count > 0,
                    x => filter.TransportWorkShiftIds.Contains(x.TransportWorkShiftId))
                //.OrderByWithSortingModel(filter.Sorting)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            return data.Select(x => _mapper.Map<FilteredRouteViewModel>(x)).ToList();
        }

        public async Task<IList<RouteViewModel>> GetRoutesAsync(GetRouteContract getContract)
        {
            if (getContract.Ids == null || getContract.Ids.Count < 1)
                return null;

            var data = await _context.Routes
                .Where(x => x.IsDeleted == false)
                .Where(x => getContract.Ids.Contains(x.Id))
                .AsNoTracking()
                .ToListAsync();

            if (data == null || data.Count < 1)
            {
                return null;
            }

            return data.Select(x => _mapper.Map<RouteViewModel>(x)).ToList();
        }

        public async Task<RouteViewModel> UpdateRouteAsync(UpdateRouteContract updateContract)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Ищем рейс по ид контракта
                    var elem = await _context.Routes.FirstOrDefaultAsync(x =>
                        x.IsDeleted == false && x.Id == updateContract.Id);

                    if (elem == null)
                    {
                        return default;
                    }

                    //!_! Обновляем рейс
                    _mapper.Map(updateContract, elem);

                    await _context.SaveChangesAsync();

                    //!_! Добавляем исторические записи
                    await _fillHistoryService.FillHistoryAsync<Route, RouteHistory>(
                        new List<Route> { elem },
                        _context,
                        updateContract.UserId,
                        updateContract.UserName
                    );

                    await transaction.CommitAsync();

                    var routeViewModel = _mapper.Map<RouteViewModel>(elem);
                    routeViewModel.StandIds = updateContract.StandIds;
                    return routeViewModel;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while updating Route with id {updateContract.Id}");
                    await transaction.RollbackAsync();
                    return default;
                }
            }
        }

        public async Task<bool> DeleteRouteAsync(DeleteRouteContract deleteContract)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Удаляем рейс
                    var elems = _context.Routes
                        .Where(x =>
                            x.IsDeleted == false &&
                            deleteContract.Id == x.Id
                        );

                    var deletedRoutes = new List<Route>();
                    await elems.ForEachAsync(x =>
                    {
                        x.IsDeleted = true;
                        deletedRoutes.Add(x);
                    });
                    await _context.SaveChangesAsync();

                    //!_! Добавляем исторические записи
                    await _fillHistoryService.FillHistoryAsync<Route, RouteHistory>(
                        deletedRoutes,
                        _context,
                        deleteContract.UserId,
                        deleteContract.UserName
                    );

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        $"Error while deleting rute with id {deleteContract.Id}");
                    await transaction.RollbackAsync();
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> CloseRouteAsync(CloseRouteContract closeContract)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Ищем рейс по ид контракта
                    var elem = await _context.Routes.FirstOrDefaultAsync(x =>
                        x.IsDeleted == false && x.Id == closeContract.Id);

                    if (elem == null)
                    {
                        return default;
                    }

                    //!_! Обновляем рейс
                    _mapper.Map(closeContract, elem);

                    await _context.SaveChangesAsync();

                    //!_! Добавляем исторические записи
                    await _fillHistoryService.FillHistoryAsync<Route, RouteHistory>(
                        new List<Route> { elem },
                        _context,
                        closeContract.UserId,
                        closeContract.UserName
                    );

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        $"Error while closing route with id {closeContract.Id}");
                    await transaction.RollbackAsync();
                    return false;
                }
            }

            return true;
        }

        public async Task<IList<GetRoutesByWorkShiftsIdsViewModel>> GetRoutesByWorkShiftsIdsAsync(GetRoutesByWorkShiftsIdsContract getContract)
        {
            if (getContract.WorkShiftsIds == null || getContract.WorkShiftsIds.Count < 1)
                return null;

            var data = await _context.Routes
                .Where(x => x.IsDeleted == false)
                .Where(x => x.RouteCompleted)
                .Where(x => getContract.WorkShiftsIds.Contains(x.TransportWorkShiftId))
                .AsNoTracking()
                .ToListAsync();

            if (data == null || data.Count < 1)
            {
                return null;
            }

            return data.Select(x => _mapper.Map<GetRoutesByWorkShiftsIdsViewModel>(x)).ToList();
        }

        private async Task<IList<Route>> GetRoutesOnDateAsync(DateTime date)
        {
            var data = await _context.Routes
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Date.Date == date.Date)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return data.ToList();
        }
    }
}