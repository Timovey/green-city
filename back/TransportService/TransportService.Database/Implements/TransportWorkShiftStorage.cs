using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportContracts.DataContracts.TransportWorkShift;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Database.Models;

namespace TransportService.Database.Implements
{
    public class TransportWorkShiftStorage : ITransportWorkShiftStorage
    {
        private readonly TransportDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public TransportWorkShiftStorage(
            TransportDbContext context,
            FillHistoryService fillHistoryService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
        }

        public async Task<int> CreateTransportWorkShiftAsync(CreateTransportWorkShiftContract createContract)
        {
            var transportWorkShift = _mapper.Map<TransportWorkShift>(createContract);

            await _context.TransportWorkShifts.AddAsync(transportWorkShift);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<TransportWorkShift, TransportWorkShiftHistory>(
                new List<TransportWorkShift> { transportWorkShift },
                _context,
                createContract.UserId,
                createContract.UserName
            );

            return transportWorkShift.Id;
        }

        public async Task<bool> DeleteAllTransportWorkShiftAsync()
        {
            var elems = _context.TransportWorkShifts
                .Where(x => x.IsDeleted == false);

            foreach (var elem in elems)
            {
                elem.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<TransportWorkShift, TransportWorkShiftHistory>(
                await elems.ToListAsync(),
                _context,
                0,
                "admin"
            );

            return true;
        }

        public async Task<bool> DeleteOnDateTransportWorkShiftAsync(ActualizeWorkShitsContract contract)
        {
            var elems = _context.TransportWorkShifts
                .Where(x => x.IsDeleted == false
                    && x.DateTime == contract.DateTime);

            foreach (var elem in elems)
            {
                elem.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<TransportWorkShift, TransportWorkShiftHistory>(
                await elems.ToListAsync(),
                _context,
                contract.UserId,
                contract.UserName
            );

            return true;
        }

        public async Task<bool> DeleteTransportWorkShiftsAsync(DeleteTransportWorkShiftsContract contract)
        {
            var elems = _context.TransportWorkShifts
                .Where(x => x.IsDeleted == false
                    && contract.TransportWorkShiftsIds.Contains(x.Id));

            foreach (var elem in elems)
            {
                elem.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<TransportWorkShift, TransportWorkShiftHistory>(
                await elems.ToListAsync(),
                _context,
                contract.UserId,
                contract.UserName
            );

            return true;
        }

        public async Task<IList<TransportWorkShiftViewModel>> GetTransportWorkShiftsAsync(GetActualWorkShitsContract contract)
        {
            var transportWorkShiftsWithTransport = await (
                from transportWorkShift in _context.TransportWorkShifts
                join transport in _context.Transports
                    on transportWorkShift.TransportId equals transport.Id
                join driver in _context.Drivers
                    on transportWorkShift.DriverId equals driver.Id
                where 1 == 1
                    && transportWorkShift.IsDeleted == false
                    && transportWorkShift.DateTime.Date == contract.DateTime.Date
                    && transport.IsDeleted == false
                    && driver.IsDeleted == false
                select new
                {
                    TransportWorkShift = transportWorkShift,
                    Transport = transport,
                    Driver = driver
                })
               .OrderByDescending(x => x.TransportWorkShift.Id)
               .AsNoTracking()
               .ToListAsync();

            var result = new List<TransportWorkShiftViewModel>();
            foreach (var workShift in transportWorkShiftsWithTransport)
            {
                result.Add(ToTransportWorkShiftViewModel(
                    workShift.Transport,
                    workShift.TransportWorkShift,
                    workShift.Driver)
                );
            }
            //дополнительно фильтруем по организациям
            if (contract.IsFilterByUser && contract.OrganizationIds != null)
            {
                return result.Where(el =>
                    el.OrganizationIds.Intersect(contract.OrganizationIds).Any()).ToList();
            }
            //если у пользователя нет организаций и есть фильтрация по ним -
            //то возвращаем пустой список
            else if (contract.IsFilterByUser && contract.OrganizationIds == null)
            {
                return new List<TransportWorkShiftViewModel>();
            }
            return result;
        }

        public async Task<IList<TransportWorkShiftDropdownViewModel>> GetTransportWorkShiftsDropdownAsync(GetWorkShitsDropdownContract contract)
        {
            var transportWorkShiftsWithTransport = await (
                from transportWorkShift in _context.TransportWorkShifts
                join transport in _context.Transports
                    on transportWorkShift.TransportId equals transport.Id
                join driver in _context.Drivers
                    on transportWorkShift.DriverId equals driver.Id
                where 1 == 1
                    && transportWorkShift.IsDeleted == false
                    && transportWorkShift.DateTime.Date == contract.DateTime.Date
                    && transportWorkShift.IsInRoute == false
                    && transport.IsDeleted == false
                    && driver.IsDeleted == false
                select new
                {
                    TransportWorkShift = transportWorkShift,
                    Transport = transport,
                    Driver = driver
                })
               .OrderByDescending(x => x.TransportWorkShift.Id)
               .AsNoTracking()
               .ToListAsync();

            var result = new List<TransportWorkShiftDropdownViewModel>();
            foreach (var workShift in transportWorkShiftsWithTransport)
            {
                result.Add(ToTransportWorkShiftDropdownViewModel(
                    workShift.Transport,
                    workShift.TransportWorkShift,
                    workShift.Driver)
                );
            }

            //дополнительно фильтруем по организациям
            if (contract.IsFilterByUser && contract.OrganizationIds != null)
            {
                return result.Where(el =>
                    el.OrganizationIds.Intersect(contract.OrganizationIds).Any()).ToList();
            }
            //если у пользователя нет организаций и есть фильтрация по ним -
            //то возвращаем пустой список
            else if (contract.IsFilterByUser && contract.OrganizationIds == null)
            {
                return new List<TransportWorkShiftDropdownViewModel>();
            }
            return result;
        }

        public async Task<IList<TransportWorkShiftViewModel>> GetWorkShiftByIdsAsync(
            GetWorkShiftsByIdsContract contract,
            IList<int> orgIds = default
        )
        {
            var transportWorkShiftsWithTransport = await (
                from transportWorkShift in _context.TransportWorkShifts
                join transport in _context.Transports
                    on transportWorkShift.TransportId equals transport.Id
                join driver in _context.Drivers
                    on transportWorkShift.DriverId equals driver.Id
                where 1 == 1
                    && transportWorkShift.IsDeleted == false
                    && transport.IsDeleted == false
                    && driver.IsDeleted == false
                    && contract.WorkShiftIds.Contains(transportWorkShift.Id)
                select new
                {
                    TransportWorkShift = transportWorkShift,
                    Transport = transport,
                    Driver = driver
                })
               .OrderByDescending(x => x.TransportWorkShift.Id)
               .AsNoTracking()
               .ToListAsync();

            var result = new List<TransportWorkShiftViewModel>();
            foreach (var workShift in transportWorkShiftsWithTransport)
            {
                result.Add(ToTransportWorkShiftViewModel(
                    workShift.Transport,
                    workShift.TransportWorkShift,
                    workShift.Driver)
                );
            }
            //дополнительно фильтруем по организациям
            if (contract.IsFilterByUser && orgIds != default)
            {
                return result.Where(el =>
                    el.OrganizationIds.Intersect(orgIds).Any()).ToList();
            }
            //если у пользователя нет организаций и есть фильтрация по ним -
            //то возвращаем пустой список
            else if (contract.IsFilterByUser && orgIds == default)
            {
                return new List<TransportWorkShiftViewModel>();
            }
            return result;
        }

        public async Task<bool> ChangeWorkshiftRouteStatusAsync(ChangeWorkshiftRouteStatusContract contract)
        {
            var workshifts = await _context.TransportWorkShifts.
                WhereIf(contract.WorkshiftIds.Any(),
                    x => x.IsDeleted == false &&
                    contract.WorkshiftIds.Contains(x.Id))
                .ToListAsync();
            foreach (var ws in workshifts)
            {
                ws.IsInRoute = contract.RouteStatus;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<TransportWorkShiftViewModel>> GetWorkShiftsForDoneTaskListAsync(
            GetWorkShiftsForDoneTaskListContract contract)
        {
            var transportWorkShiftsWithTransport = await (
                    from transportWorkShift in _context.TransportWorkShifts
                    join transport in _context.Transports
                        on transportWorkShift.TransportId equals transport.Id
                    join driver in _context.Drivers
                        on transportWorkShift.DriverId equals driver.Id
                    where 1 == 1
                          && transportWorkShift.IsDeleted == false
                          && transportWorkShift.DateTime.Date >= contract.DateFrom.Date
                          && transportWorkShift.DateTime.Date <= contract.DateTo.Date
                          && transport.IsDeleted == false
                          && driver.IsDeleted == false
                    select new
                    {
                        TransportWorkShift = transportWorkShift,
                        Transport = transport,
                        Driver = driver
                    })
                .AsNoTracking()
                .WhereIf(contract.CarIds == default || contract.CarIds.Count > 0,
                    x => contract.CarIds.Contains(x.Transport.Id)
                )
                .OrderByDescending(x => x.TransportWorkShift.Id)
                .ToListAsync();

            var result = new List<TransportWorkShiftViewModel>();
            foreach (var workShift in transportWorkShiftsWithTransport)
            {
                result.Add(ToTransportWorkShiftViewModel(
                    workShift.Transport,
                    workShift.TransportWorkShift,
                    workShift.Driver)
                );
            }
            return result;
        }

        private TransportWorkShiftViewModel ToTransportWorkShiftViewModel(
            Transport transport,
            TransportWorkShift transportWorkShift,
            Driver driver
        )
        {
            if (transportWorkShift == null || transport == null)
            {
                return null;
            }

            var transportMap = _mapper.Map<TransportViewModel>(transport);

            var result = _mapper.Map<TransportWorkShiftViewModel>(transportWorkShift);
            result.TransportName = transportMap.Name;
            result.TransportType = transportMap.TransportType;
            result.RegNumber = transportMap.RegNumber;
            result.TransportOrganizationId = transportMap.OrganizationId;
            result.TankTypes = transportMap.TankTypes;
            result.DriverFio = $"{driver.DriverSurname} {driver.DriverName} {driver.DriverPatronymic}";

            return result;
        }

        private TransportWorkShiftDropdownViewModel ToTransportWorkShiftDropdownViewModel(
           Transport transport,
           TransportWorkShift transportWorkShift,
           Driver driver
        )
        {
            if (transportWorkShift == null || transport == null)
            {
                return null;
            }

            var result = _mapper.Map<TransportWorkShiftDropdownViewModel>(transportWorkShift);

            string driverShortName = driver.DriverName.IsNullOrEmpty() ? "" : $"{driver.DriverName.FirstOrDefault()}. ";
            string driverShortPatr = driver.DriverPatronymic.IsNullOrEmpty() ? "" : $"{driver.DriverPatronymic.FirstOrDefault()}.";

            string driverData = $"{driver.DriverSurname} {driverShortName}{driverShortPatr}";
            string transportData = $"{transport.Name} {transport.RegNumber}";

            result.WorkshiftData = driverData + transportData;

            return result;
        }
    }
}
