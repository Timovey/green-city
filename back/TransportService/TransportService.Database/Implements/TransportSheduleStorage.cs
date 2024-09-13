using AutoMapper;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using GreenCity.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportContracts.DataContracts.TransportShedule;
using TransportContracts.DateRange;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Database.Models;

namespace TransportService.Database.Implements
{
    public class TransportSheduleStorage : ITtansportSheduleStorage
    {
        private readonly TransportDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public TransportSheduleStorage(
            TransportDbContext context,
            FillHistoryService fillHistoryService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
        }

        public async Task<TransportSheduleViewModel> CreateTransportSheduleAsync(CreateTransportSheduleContract createContract)
        {
            var transportShedule = _mapper.Map<TransportShedule>(createContract);

            await _context.TransportShedules.AddAsync(transportShedule);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<TransportShedule, TransportSheduleHistory>(
                new List<TransportShedule> { transportShedule },
                _context,
                createContract.UserId,
                createContract.UserName
            );

            var transport = await _context.Transports
               .Where(x => x.IsDeleted == false && x.Id == transportShedule.TransportId)
               .OrderByDescending(x => x.Id)
               .AsNoTracking()
               .FirstAsync();

            return ToTransportSheduleViewModel(transport, transportShedule);
        }

        public async Task<bool> DeleteTransportsSheduleAsync(DeleteTransportSheduleContract deleteContract)
        {
            if (deleteContract.TransportSheduleIds.Count < 1)
                return true;

            var elems = _context.TransportShedules
                .Where(x => 
                    x.IsDeleted == false && 
                    deleteContract.TransportSheduleIds.Contains(x.Id));

            foreach (var elem in elems)
            {
                elem.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<TransportShedule, TransportSheduleHistory>(
                await elems.ToListAsync(),
                _context,
                deleteContract.UserId,
                deleteContract.UserName
            );

            return true;
        }

        public async Task<IList<TransportSheduleViewModel>> GetTransportsSheduleWithoutFilterAsync(CommonHttpFilter filter)
        {
            var transportSheduleWithTransport =
               await (from transportShedule in _context.TransportShedules
                           .Where(x =>
                               x.IsDeleted == false)
                      from transport in _context.Transports
                           .Where(x =>
                              x.IsDeleted == false &&
                              x.Id == transportShedule.TransportId)
                      .DefaultIfEmpty()
                      select new
                      {
                          TransportShedule = transportShedule,
                          Transport = transport
                      })
               .OrderByDescending(x => x.TransportShedule.Id)
               .Paging(filter.ItemsOnPage, filter.Page)
               .AsNoTracking()
               .ToListAsync();

            var result = new List<TransportSheduleViewModel>();
            foreach (var transportShedule in transportSheduleWithTransport)
            {
                result.Add(ToTransportSheduleViewModel(
                    transportShedule.Transport,
                    transportShedule.TransportShedule)
                );
            }
            return result;
        }

        public async Task<IList<TransportSheduleViewModel>> GetTransportsSheduleAsync(GetTransportSheduleContract getContract)
        {
            if (getContract.TransportSheduleIds.Count < 1)
                return null;

            var transportSheduleWithTransport =
               await (from transportShedule in _context.TransportShedules
                           .Where(x =>
                                x.IsDeleted == false &&
                                getContract.TransportSheduleIds.Contains(x.Id))
                      from transport in _context.Transports
                           .Where(x =>
                              x.IsDeleted == false &&
                              x.Id == transportShedule.TransportId)
                      .DefaultIfEmpty()
                      select new
                      {
                          TransportShedule = transportShedule,
                          Transport = transport
                      })
               .ToListAsync();

            var result = new List<TransportSheduleViewModel>();
            foreach (var transportShedule in transportSheduleWithTransport)
            {
                result.Add(ToTransportSheduleViewModel(
                    transportShedule.Transport,
                    transportShedule.TransportShedule)
                );
            }
            return result;
        }

        public async Task<IList<TransportSheduleViewModel>> GetFilteredTransportsSheduleAsync(TransportSheduleFilterModel filter)
        {
            if (filter == null)
                return null;

            var transportSheduleWithTransport =
               await (from transportShedule in _context.TransportShedules
                           .Where(x =>
                               x.IsDeleted == false)
                      from transport in _context.Transports
                           .Where(x =>
                              x.IsDeleted == false &&
                              x.Id == transportShedule.TransportId)
                      .DefaultIfEmpty()
                      select new
                      {
                          TransportShedule = transportShedule,
                          Transport = transport
                      })
                .WhereIf(filter.TransportIds != null && filter.TransportIds.Count > 0, 
                    x => filter.TransportIds.Contains(x.TransportShedule.TransportId))
                .WhereIf(filter.WorkSheduleTypes != null && filter.WorkSheduleTypes.Count > 0, 
                    x => filter.WorkSheduleTypes.Contains(x.TransportShedule.WorkSheduleType))
                .WhereIf(filter.CreateDateFrom != null, x => filter.CreateDateFrom <= x.TransportShedule.CreateDate)
                .WhereIf(filter.CreateDateTo != null, x => filter.CreateDateTo >= x.TransportShedule.CreateDate)
                .WhereIf(filter.StartDateFrom != null, x => filter.StartDateFrom <= x.TransportShedule.StartDate)
                .WhereIf(filter.StartDateTo != null, x => filter.StartDateTo >= x.TransportShedule.StartDate)
                .WhereIf(filter.EndDateFrom != null, x => filter.EndDateFrom <= x.TransportShedule.EndDate)
                .WhereIf(filter.EndDateTo != null, x => filter.EndDateTo >= x.TransportShedule.EndDate)
                .OrderByWithSortingModel(filter.Sorting)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            var result = new List<TransportSheduleViewModel>();
            foreach (var transportShedule in transportSheduleWithTransport)
            {
                result.Add(ToTransportSheduleViewModel(
                    transportShedule.Transport,
                    transportShedule.TransportShedule)
                );
            }
            return result;
        }

        public async Task<TransportSheduleViewModel> UpdateTransportSheduleAsync(UpdateTransportSheduleContract updateContract)
        {
            var transportShedule = _mapper.Map<TransportShedule>(updateContract);

            var elems = _context.TransportShedules
                .Where(
                    x => x.IsDeleted == false &&
                    x.Id == updateContract.TransportSheduleId);

            foreach (var elem in elems)
            {
                transportShedule.Id = elem.Id;
                _mapper.Map(transportShedule, elem);
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<TransportShedule, TransportSheduleHistory>(
                 await elems.ToListAsync(),
                 _context,
                 updateContract.UserId,
                 updateContract.UserName
            );

            var transport = await _context.Transports
               .Where(x => x.IsDeleted == false && x.Id == transportShedule.TransportId)
               .OrderByDescending(x => x.Id)
               .AsNoTracking()
               .FirstAsync();

            return ToTransportSheduleViewModel(transport, transportShedule);
        }

        private TransportSheduleViewModel ToTransportSheduleViewModel(
            Transport transport,
            TransportShedule transportShedule
        )
        {
            if (transportShedule == null || transport == null)
            {
                return null;
            }

            var transportMap = _mapper.Map<TransportViewModel>(transport);

            var result = _mapper.Map<TransportSheduleViewModel>(transportShedule);
            result.TransportName = transportMap.Name;
            result.TransportType = transportMap.TransportType;
            result.RegNumber = transportMap.RegNumber;
            result.TransportOrganizationId = transportMap.OrganizationId;

            result.DateRange = _mapper.Map<DateRangeBaseModel>(transportShedule);
            return result;
        }

        public async Task<TransportSheduleDateRangeViewModel> GetTransportSheduleDateRangeAsync(
            GetTransportSheduleDateRangeContract getContract)
        {
            //берем транспорт
            var transport = await _context.Transports
             .Where(x =>
                 x.IsDeleted == false &&
                 x.Id == getContract.TransportId)
                     .AsNoTracking()
                     .FirstOrDefaultAsync();

            //берем его расписания, с условием пересекания (либо дата начала
            //не меньше даты окончания поиска, причем дата окончания должна быть
            //не меньше даты начала поиска)
            //либо дата окончания не меньше даты начала поиска,
            //а дата начала не меньше даты окончания поиска
            var transportShedules = await _context.TransportShedules
             .Where(x =>
                 x.IsDeleted == false &&
                 x.TransportId == getContract.TransportId)
                    .Where(rc => (rc.StartDate <= getContract.DateTo 
                        && rc.EndDate >= getContract.DateFrom)
                        || (rc.EndDate >= getContract.DateFrom 
                        && rc.StartDate <= getContract.DateTo))
                     .AsNoTracking()
                     .ToListAsync();

            //берем его ремонты 
            var repairs = await _context.RepairCards
             .Where(
                 x => x.IsDeleted == false &&
                    x.TransportId == transport.Id)
                     .Where(rc => (rc.StartDate <= getContract.DateTo
                        && rc.EndDate >= getContract.DateFrom)
                        || (rc.EndDate >= getContract.DateFrom
                        && rc.StartDate <= getContract.DateTo))
                    .AsNoTracking()
                    .ToListAsync();

            //берем аренду транспорта
            var rents = await _context.Rents
             .Where(
                 x => x.IsDeleted == false &&
                    x.TransportId == transport.Id)
                     .Where(rc => (rc.StartDate <= getContract.DateTo
                        && rc.EndDate >= getContract.DateFrom)
                        || (rc.EndDate >= getContract.DateFrom
                        && rc.StartDate <= getContract.DateTo))
                    .AsNoTracking()
                    .ToListAsync();

            //берем дежурные смены
            // по другим фильтрам
            //т.к. это не DateRange, а один день
            var transportDutyShifts = await _context.TransportDutyShifts
             .Where(
                 x => x.IsDeleted == false &&
                    x.TransportId == transport.Id)
                     .Where(rc => (rc.Date >= getContract.DateFrom && rc.Date <= getContract.DateTo))
                    .AsNoTracking()
                    .ToListAsync();

            //ищем все закрепления водителей и самих водителей
            var d2Ts = await _context.Driver2Transports
             .Where(
                 x => x.IsDeleted == false &&
                    x.TransportId == transport.Id)
                     .Where(rc => (rc.StartDate <= getContract.DateTo
                        && rc.EndDate >= getContract.DateFrom)
                        || (rc.EndDate >= getContract.DateFrom
                        && rc.StartDate <= getContract.DateTo))
                    .AsNoTracking()
                    .ToListAsync();

            var drivers = await
               (from driver in _context.Drivers
                  .Where(x => x.IsDeleted == false)
                from d2t in _context.Driver2Transports
                .Where(x => x.IsDeleted == false
                     && x.TransportId == getContract.TransportId
                     && x.DriverId == driver.Id)
                .Where(rc => (rc.StartDate <= getContract.DateTo
                     && rc.EndDate >= getContract.DateFrom)
                     || (rc.EndDate >= getContract.DateFrom
                     && rc.StartDate <= getContract.DateTo))
                select new
                {
                    Driver = driver
                })
               .OrderByDescending(x => x.Driver.Id)
               .Distinct()
               .AsNoTracking()
               .ToListAsync();

            var res = _mapper.Map<TransportSheduleDateRangeViewModel>(transport);

            var resTransportShedules = transportShedules.Select(el => _mapper.Map<TransportSheduleViewModel>(el)).ToList();
            var resRepairCards = repairs.Select(el => _mapper.Map<RepairCardViewModel>(el)).ToList();
            var resRents = rents.Select(el => _mapper.Map<RentViewModel>(el)).ToList();
            var resTransportDutyShifts = transportDutyShifts.Select(el => _mapper.Map<TransportDutyShiftViewModel>(el)).ToList();

            var resD2Ts = d2Ts.Select(el => _mapper.Map<Driver2TransportViewModel>(el)).ToList();
            var resDrivers = drivers.Select(el => _mapper.Map<DriverSheduleViewModel>(el.Driver)).ToList();

            //распределяем так, чтобы у водителя были его закрепления
            //находим отпуска/больничные
            foreach(var driver in resDrivers)
            {
                driver.Driver2Transports = resD2Ts.Where(x => x.DriverId == driver.DriverId).ToList();
                var dv = await _context.DriverVacations
                    .Where(
                        x => x.IsDeleted == false &&
                        x.DriverId == driver.DriverId)
                          .Where(rc => (rc.StartDate <= getContract.DateTo
                            && rc.EndDate >= getContract.DateFrom)
                            || (rc.EndDate >= getContract.DateFrom
                            && rc.StartDate <= getContract.DateTo))
                        .AsNoTracking()
                        .ToListAsync();
                driver.DriverVacations = dv.Select(x => _mapper.Map<DriverVacationViewModel>(x)).ToList();
            }
            //заполняем поля транспорта
            var transportMap = _mapper.Map<TransportViewModel>(transport);
            res.TransportName = transportMap.Name;
            res.TransportType = transportMap.TransportType;
            res.RegNumber = transportMap.RegNumber;
            res.TransportOrganizationId = transportMap.OrganizationId;

            //заполняем итоговую модель
            res.DriverShedules = resDrivers;
            res.TransportShedules = resTransportShedules;
            res.RepairCards = resRepairCards;
            res.Rents = resRents;
            res.TransportDutyShifts = resTransportDutyShifts;
            return res;
        }
    }
}
