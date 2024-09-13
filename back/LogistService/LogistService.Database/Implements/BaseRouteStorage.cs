using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using GreenCity.Http;
using LogistService.Contracts.DataContracts.BaseRoute;
using LogistService.Contracts.Filters;
using LogistService.Contracts.ViewModels;
using LogistService.Database.Interfaces;
using LogistService.Database.Models;
using LogistService.Database.Models.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LogistService.Database.Implements
{
    public class BaseRouteStorage : IBaseRouteStorage
    {
        private readonly LogistDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly ILogger<BaseRouteStorage> _logger;
        private readonly IMapper _mapper;

        public BaseRouteStorage(
            LogistDbContext context,
            FillHistoryService fillHistoryService,
            ILogger<BaseRouteStorage> logger,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BaseRouteViewModel> CreateBaseRouteAsync(CreateDbBaseRouteContract createContract)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Создаем бм
                    var baseRoute = _mapper.Map<BaseRoute>(createContract);
                    var addedResult = await _context.AddAsync(baseRoute);
                    await _context.SaveChangesAsync();

                    //!_! Добавляем исторические записи
                    await _fillHistoryService.FillHistoryAsync<BaseRoute, BaseRouteHistory>(
                        new List<BaseRoute> { baseRoute },
                        _context,
                        createContract.CreatedUserId,
                        createContract.CreatedUserName
                    );

                    //!_! Получаем список записей связок Route2Stand
                    var baseRoute2Stands =
                        GetBaseRoute2StandDbModels(addedResult.Entity.Id, createContract.StandIdsWithPriorities);

                    //!_! Вставляем записи в таблицу связку Route2Stand
                    await _context.AddRangeAsync(baseRoute2Stands);
                    await _context.SaveChangesAsync();

                    //!_! Добавляем исторические записи
                    await _fillHistoryService.FillHistoryAsync<BaseRoute2Stand, BaseRoute2StandHistory>(
                        baseRoute2Stands,
                        _context,
                        createContract.CreatedUserId,
                        createContract.CreatedUserName
                    );

                    await transaction.CommitAsync();

                    return CreateBaseRouteViewModel(baseRoute,
                        createContract.StandIdsWithPriorities.Select(s => Tuple.Create(s.priority, s.standId))
                            .ToList());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while creating baseRoute with name {createContract.Name}");
                    await transaction.RollbackAsync();
                    return default;
                }
            }
        }

        public async Task<IList<BaseRouteViewModel>> GetBaseRoutesAsync(GetBaseRouteContract getContract)
        {
            if (getContract.Ids.Count < 1)
                return null;

            //!_! Получаем базовые маршртуты с ид прикрепленных КП, затем группируем по ид БМ
            var baseRoutesWithStands = await (
                    from baseRoute in _context.BaseRoutes
                        .Where(
                            x => x.IsDeleted == false &&
                                 getContract.Ids.Contains(x.Id)
                        )
                    from baseRoute2Stand in _context.BaseRoute2Stands
                        .Where(
                            x => x.IsDeleted == false &&
                                 x.BaseRouteId == baseRoute.Id
                        )
                    select new
                    {
                        BaseRoute = baseRoute,
                        BaseRoute2Stand = baseRoute2Stand
                    })
                .GroupBy(x => x.BaseRoute.Id)
                .Select(x => new
                {
                    BaseRoute = x.Select(xx => xx.BaseRoute).FirstOrDefault(),
                    Stands = x.Select(xx => Tuple.Create(xx.BaseRoute2Stand.Priority, xx.BaseRoute2Stand.StandId))
                        .ToList()
                })
                .AsNoTracking()
                .ToListAsync();

            if (baseRoutesWithStands == null || baseRoutesWithStands.Count < 1)
            {
                return null;
            }

            //!_! Преобразуем полученные данные в необходимые модели, добавляем в каждую список моделей КП, в котором заполнены идшники и приоритеты
            return baseRoutesWithStands.Select(x => CreateBaseRouteViewModel(x.BaseRoute, x.Stands)).ToList();
        }

        public async Task<IList<BaseRouteViewModel>> GetBaseRoutesWithoutFilterAsync(CommonHttpFilter filter)
        {
            var data = await _context.BaseRoutes
                .Where(x => x.IsDeleted == false)
                .WhereIf(filter.IsFilterByCreatedUser, x => x.CreatedUserId == filter.UserId)
                .OrderByDescending(x => x.Id)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            return data.Select(_mapper.Map<BaseRouteViewModel>).ToList();
        }

        public async Task<IList<BaseRouteViewModel>> GetFilteredBaseRoutesAsync(BaseRouteFilterModel filter)
        {
            if (filter == null)
                return null;

            var data = await _context.BaseRoutes
                .Where(x => x.IsDeleted == false)
                .WhereIf(filter.IsFilterByCreatedUser, x => x.CreatedUserId == filter.UserId)
                .WhereIf(!filter.Name.IsNullOrEmpty(), x => x.Name.Contains(filter.Name))
                //.OrderByWithSortingModel(filter.Sorting)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            return data.Select(x => _mapper.Map<BaseRouteViewModel>(x)).ToList();
        }

        public async Task<BaseRouteViewModel> UpdateBaseRouteAsync(UpdateDbBaseRouteContract updateContract)
        {
            var deletedBaseRoute2Stands = new List<BaseRoute2Stand>();
            BaseRoute baseRoute;
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    baseRoute = _mapper.Map<BaseRoute>(updateContract);
                    //!_! Ищем БМ по ид контракта
                    var elem = await _context.BaseRoutes.FirstOrDefaultAsync(x =>
                        x.IsDeleted == false && x.Id == baseRoute.Id);
                    if (elem == null)
                    {
                        return default;
                    }

                    //!_! Обновляем БМ
                    _mapper.Map(baseRoute, elem);
                    await _context.SaveChangesAsync();

                    //!_! Добавляем исторические записи
                    await _fillHistoryService.FillHistoryAsync<BaseRoute, BaseRouteHistory>(
                        new List<BaseRoute> { elem },
                        _context,
                        updateContract.CreatedUserId,
                        updateContract.CreatedUserName
                    );

                    //!_! Ищем прикрепленные КП
                    var deletedStands = _context.BaseRoute2Stands
                        .Where(x =>
                            x.IsDeleted == false &&
                            x.BaseRouteId == updateContract.Id
                        );

                    //!_! Удаляем прикрепленные КП
                    await deletedStands.ForEachAsync(x =>
                    {
                        x.IsDeleted = true;
                        deletedBaseRoute2Stands.Add(x);
                    });

                    await _context.SaveChangesAsync();

                    //!_! Получаем список записей связок Route2Stand
                    var insertedBaseRoute2Stands =
                        GetBaseRoute2StandDbModels(updateContract.Id, updateContract.StandIdsWithPriorities);

                    //!_! Вставляем записи в таблицу связку Route2Stand
                    await _context.AddRangeAsync(insertedBaseRoute2Stands);
                    await _context.SaveChangesAsync();

                    //!_! Добавляем исторические записи для удаленных и вставленных связок Route2Stand
                    await _fillHistoryService.FillHistoryAsync<BaseRoute2Stand, BaseRoute2StandHistory>(
                        deletedBaseRoute2Stands.Union(insertedBaseRoute2Stands).ToList(),
                        _context,
                        updateContract.CreatedUserId,
                        updateContract.CreatedUserName
                    );

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while updating baseRoute with id {updateContract.Id}");
                    await transaction.RollbackAsync();
                    return default;
                }
            }

            return CreateBaseRouteViewModel(baseRoute,
                updateContract.StandIdsWithPriorities.Select(s => Tuple.Create(s.priority, s.standId)).ToList());
        }

        public async Task<bool> DeleteBaseRoutesAsync(DeleteBaseRouteContract deleteContract)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //!_! Удаляем базовые маршруты
                    var elems = _context.BaseRoutes
                        .Where(x =>
                            x.IsDeleted == false &&
                            deleteContract.Ids.Contains(x.Id)
                        );

                    //создаю новый список тк в исторической таблице по другому не проставляется is_deleted=1
                    var deletedBaseRoutes = new List<BaseRoute>();
                    await elems.ForEachAsync(x =>
                    {
                        x.IsDeleted = true;
                        deletedBaseRoutes.Add(x);
                    });
                    await _context.SaveChangesAsync();

                    //!_! Добавляем исторические записи
                    await _fillHistoryService.FillHistoryAsync<BaseRoute, BaseRouteHistory>(
                        deletedBaseRoutes,
                        _context,
                        deleteContract.UserId,
                        deleteContract.UserName
                    );

                    //!_! Ищем прикрепленные КП
                    var deletedStands = _context.BaseRoute2Stands
                        .Where(x =>
                            x.IsDeleted == false &&
                            deleteContract.Ids.Contains(x.BaseRouteId)
                        );

                    var deletedBaseRoute2Stands = new List<BaseRoute2Stand>();
                    //!_! Удаляем прикрепленные КП
                    await deletedStands.ForEachAsync(x =>
                    {
                        x.IsDeleted = true;
                        deletedBaseRoute2Stands.Add(x);
                    });
                    await _context.SaveChangesAsync();

                    //!_! Добавляем исторические записи для удаленных связок Route2Stand
                    await _fillHistoryService.FillHistoryAsync<BaseRoute2Stand, BaseRoute2StandHistory>(
                        deletedBaseRoute2Stands,
                        _context,
                        deleteContract.UserId,
                        deleteContract.UserName
                    );

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        $"Error while deleting baseRoutes with ids {string.Join(',', deleteContract.Ids)}");
                    await transaction.RollbackAsync();
                    return false;
                }
            }

            return true;
        }

        private List<BaseRoute2Stand> GetBaseRoute2StandDbModels(
            int baseRouteId,
            IList<(int priority, int standId)> standIdsWithPriorities)
        {
            var baseRoute2Stands = new List<BaseRoute2Stand>();
            if (standIdsWithPriorities != null && standIdsWithPriorities.Count > 0)
            {
                foreach (var stand in standIdsWithPriorities)
                {
                    baseRoute2Stands.Add(new BaseRoute2Stand
                        { BaseRouteId = baseRouteId, Priority = stand.priority, StandId = stand.standId });
                }
            }

            return baseRoute2Stands;
        }

        private BaseRouteViewModel CreateBaseRouteViewModel(
            BaseRoute baseRoute,
            IList<Tuple<int, int>> stands
        )
        {
            if (baseRoute == null || stands == null)
            {
                return null;
            }

            var baseRouteView = _mapper.Map<BaseRouteViewModel>(baseRoute);
            baseRouteView.Stands = stands;

            return baseRouteView;
        }
    }
}