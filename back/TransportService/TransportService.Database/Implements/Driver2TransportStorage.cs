using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using GreenCity.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Driver2Transport;
using TransportContracts.DateRange;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Database.Models;

namespace TransportService.Database.Implements
{
    public class Driver2TransportStorage : IDriver2TransportStorage
    {
        private readonly TransportDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public Driver2TransportStorage(
            TransportDbContext context,
            FillHistoryService fillHistoryService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
        }

        public async Task<Driver2TransportViewModel> CreateDriver2TransportAsync(CreateDriver2TransportContract createContract)
        {
            var driver2Transport = _mapper.Map<Driver2Transport>(createContract);

            await _context.Driver2Transports.AddAsync(driver2Transport);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Driver2Transport, Driver2TransportHistory>(
                new List<Driver2Transport> { driver2Transport },
                _context,
                createContract.UserId,
                createContract.UserName
            );

            var driverAndTransport =
                await (from transport in _context.Transports
                        .Where(x =>
                            x.IsDeleted == false &&
                            x.Id == driver2Transport.TransportId)
                        .DefaultIfEmpty()
                from driver in _context.Drivers
                        .Where(x =>
                            x.IsDeleted == false &&
                            x.Id == driver2Transport.DriverId)
                        .DefaultIfEmpty()
                select new
                {
                    Driver = driver,
                    Transport = transport,
                })
                .AsNoTracking()
                .FirstAsync();

            var res = ToDriver2TransportViewModel(
                driverAndTransport.Driver, 
                driverAndTransport.Transport, 
                driver2Transport
            );
            return res;
        }

        public async Task<bool> DeleteDrivers2TransportAsync(DeleteDriver2TransportContract deleteContract)
        {
            if (deleteContract.Driver2TransportIds.Count < 1)
                return true;

            var elems = _context.Driver2Transports
                .Where(
                    x => 1 == 1
                    && x.IsDeleted == false
                    && deleteContract.Driver2TransportIds.Contains(x.Id));

            foreach (var elem in elems)
            {
                elem.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Driver2Transport, Driver2TransportHistory>(
                await elems.ToListAsync(),
                _context,
                deleteContract.UserId,
                deleteContract.UserName
            );

            return true;
        }

        public async Task<IList<Driver2TransportViewModel>> GetDrivers2TransportWithoutFilterAsync(CommonHttpFilter filter)
        {

            var driver2TransportWithDriverwithTranport =
                await (from driver2Transport in _context.Driver2Transports
                            .Where(x =>
                                x.IsDeleted == false)
                       from transport in _context.Transports
                            .Where(x =>
                                x.IsDeleted == false &&
                                x.Id == driver2Transport.TransportId)
                            .DefaultIfEmpty()
                       from driver in _context.Drivers
                            .Where(x =>
                                x.IsDeleted == false &&
                                x.Id == driver2Transport.DriverId)
                            .DefaultIfEmpty()
                       select new
                       {
                           Driver = driver,
                           Transport = transport,
                           Driver2Transport = driver2Transport
                       })
                .OrderByDescending(x => x.Driver2Transport.Id)
                .AsNoTracking()
                .Paging(filter.ItemsOnPage, filter.Page)
                .ToListAsync();

            return driver2TransportWithDriverwithTranport.Select(x =>
                ToDriver2TransportViewModel(
                    x.Driver,
                    x.Transport,
                    x.Driver2Transport
                )).ToList();
        }

        public async Task<IList<Driver2TransportViewModel>> GetDrivers2TransportAsync(GetDriver2TransportContract getContract)
        {
            var exists = await _context.Driver2Transports.AnyAsync();
            if (!exists)
                return null;
            var driver2TransportWithDriverwithTranport =
                await (from driver2Transport in _context.Driver2Transports
                            .Where(x =>
                                x.IsDeleted == false &&
                                getContract.Driver2TransportIds.Contains(x.Id))
                       from transport in _context.Transports
                            .Where(x =>
                                x.IsDeleted == false &&
                                x.Id == driver2Transport.TransportId)
                            .DefaultIfEmpty()
                       from driver in _context.Drivers
                            .Where(x =>
                                x.IsDeleted == false &&
                                x.Id == driver2Transport.DriverId)
                            .DefaultIfEmpty()
                       select new
                       {
                           Driver = driver,
                           Transport = transport,
                           Driver2Transport = driver2Transport
                       })
                .AsNoTracking()
                .ToListAsync();

            var result = new List<Driver2TransportViewModel>();
            foreach (var driver2Transport in driver2TransportWithDriverwithTranport)
            {
                result.Add(ToDriver2TransportViewModel(
                    driver2Transport.Driver,
                    driver2Transport.Transport,
                    driver2Transport.Driver2Transport
                ));
            }

            return result;
        }

        public async Task<IList<Driver2TransportViewModel>> GetFilteredDrivers2TransportAsync(Driver2TransportFilterModel filter)
        {
            if (filter == null)
                return null;

            var driver2TransportWithDriverwithTranport =
                await (from driver2Transport in _context.Driver2Transports
                            .Where(x =>
                                x.IsDeleted == false)
                       from transport in _context.Transports
                            .Where(x =>
                                x.IsDeleted == false &&
                                x.Id == driver2Transport.TransportId)
                            .DefaultIfEmpty()
                       from driver in _context.Drivers
                            .Where(x =>
                                x.IsDeleted == false &&
                                x.Id == driver2Transport.DriverId)
                            .DefaultIfEmpty()
                       select new
                       {
                           Driver = driver,
                           Transport = transport,
                           Driver2Transport = driver2Transport
                       })
                .WhereIf(filter.TransportIds != null && filter.TransportIds.Count > 0, x => filter.TransportIds.Contains(x.Transport.Id))
                .WhereIf(filter.DriverIds != null && filter.DriverIds.Count > 0, x => filter.DriverIds.Contains(x.Driver.Id))
                .WhereIf(!filter.DriverName.IsNullOrEmpty(), x => filter.DriverName.Contains(x.Driver.DriverName))
                .WhereIf(!filter.DriverSurname.IsNullOrEmpty(), x => filter.DriverSurname.Contains(x.Driver.DriverSurname))
                .WhereIf(filter.DriverPositionTypes != null && filter.DriverPositionTypes.Count > 0, x => filter.DriverPositionTypes.Contains(x.Driver.DriverPositionType))
                .WhereIf(filter.TransportOrganizationIds != null && filter.TransportOrganizationIds.Count > 0, x => x.Transport.OrganizationId.HasValue && filter.TransportOrganizationIds.Contains(x.Transport.OrganizationId.Value))
                .WhereIf(filter.DriverOrganizationIds != null && filter.DriverOrganizationIds.Count > 0, x => x.Driver.OrganizationId.HasValue && filter.DriverOrganizationIds.Contains(x.Driver.OrganizationId.Value))
                .WhereIf(!filter.TransportName.IsNullOrEmpty(), x => x.Transport.Name.Contains(filter.TransportName))
                .WhereIf(!filter.RegNumber.IsNullOrEmpty(), x => filter.RegNumber.Contains(x.Transport.RegNumber))
                .WhereIf(filter.TransportTypes != null && filter.TransportTypes.Count > 0, x => filter.TransportTypes.Contains(x.Transport.TransportType))
                .WhereIf(filter.CreateDateFrom != null, x => filter.CreateDateFrom  <= x.Driver2Transport.CreateDate)
                .WhereIf(filter.CreateDateTo != null, x => filter.CreateDateTo >= x.Driver2Transport.CreateDate)
                .WhereIf(filter.StartDateFrom != null, x => filter.StartDateFrom <= x.Driver2Transport.StartDate)
                .WhereIf(filter.StartDateTo != null, x => filter.StartDateTo >= x.Driver2Transport.StartDate)
                .WhereIf(filter.EndDateFrom != null, x => filter.EndDateFrom <= x.Driver2Transport.EndDate)
                .WhereIf(filter.EndDateTo != null, x => filter.EndDateTo >= x.Driver2Transport.EndDate)
                .OrderByWithSortingModel(filter.Sorting, "Driver2Transport")
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            return driver2TransportWithDriverwithTranport.Select(x =>
                ToDriver2TransportViewModel(
                    x.Driver,
                    x.Transport,
                    x.Driver2Transport
                )).ToList();

        }

        public async Task<Driver2TransportViewModel> UpdateDriver2TransportAsync(UpdateDriver2TransportContract updateContract)
        {
            var driver2Transport = _mapper.Map<Driver2Transport>(updateContract);

            var elems = _context.Driver2Transports
                .Where(
                    x => x.IsDeleted == false &&
                    x.Id == updateContract.Driver2TransportId);

            foreach (var elem in elems)
            {
                driver2Transport.Id = elem.Id;
                _mapper.Map(driver2Transport, elem);
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Driver2Transport, Driver2TransportHistory>(
                 await elems.ToListAsync(),
                 _context,
                 updateContract.UserId,
                 updateContract.UserName
            );
            var driverAndTransport =
              await (from transport in _context.Transports
                      .Where(x =>
                          x.IsDeleted == false &&
                          x.Id == driver2Transport.TransportId)
                      .DefaultIfEmpty()
                     from driver in _context.Drivers
                               .Where(x =>
                                   x.IsDeleted == false &&
                                   x.Id == driver2Transport.DriverId)
                               .DefaultIfEmpty()
                     select new
                     {
                         Driver = driver,
                         Transport = transport,
                     })
              .AsNoTracking()
              .FirstAsync();

            var res = ToDriver2TransportViewModel(
                driverAndTransport.Driver,
                driverAndTransport.Transport,
                driver2Transport
            );
            return res;
        }

        private Driver2TransportViewModel ToDriver2TransportViewModel(
          Driver driver,
          Transport transport,
          Driver2Transport driver2Transport
        )
        {
            //если соединили записи с несуществующими id
            if (driver == null || transport == null)
            {
                return null;
            }

            var driverMap = _mapper.Map<DriverViewModel>(driver);
            var transportMap = _mapper.Map<TransportViewModel>(transport);

            var result = _mapper.Map<Driver2TransportViewModel>(driver2Transport);
            result.TransportName = transportMap.Name;
            result.TransportType = transportMap.TransportType;
            result.RegNumber = transportMap.RegNumber;
            result.TransportOrganizationId = transportMap.OrganizationId;

            result.DriverName = driverMap.DriverName;
            result.DriverSurname = driverMap.DriverSurname;
            result.DriverPatronymic = driverMap.DriverPatronymic;
            result.DriverPositionType = driverMap.DriverPositionType;
            result.DriverOrganizationId = driverMap.OrganizationId;

            result.DateRange = _mapper.Map<DateRangeBaseModel>(driver2Transport);
            return result;
        }
    }
}
