using AutoMapper;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using GreenCity.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportContracts.DataContracts.DriverVacation;
using TransportContracts.DateRange;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Database.Models;


namespace TransportService.Database.Implements
{
    public class DriverVacationStorage : IDriverVacationStorage
    {
        private readonly TransportDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public DriverVacationStorage(
            TransportDbContext context,
            FillHistoryService fillHistoryService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
        }

        public async Task<DriverVacationViewModel> CreateDriverVacationAsync(CreateDriverVacationContract createContract)
        {
            var driverVacation = _mapper.Map<DriverVacation>(createContract);
            await _context.DriverVacations.AddAsync(driverVacation);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<DriverVacation, DriverVacationHistory>(
                new List<DriverVacation> { driverVacation },
                _context,
                createContract.UserId,
                createContract.UserName
            );

            var driver = await _context.Drivers
               .Where(x => x.IsDeleted == false && x.Id == driverVacation.DriverId)
               .OrderByDescending(x => x.Id)
               .AsNoTracking()
               .FirstAsync();

            var replacementDriver = await _context.Drivers
               .Where(x => x.IsDeleted == false && x.Id == driverVacation.ReplacementDriverId)
               .OrderByDescending(x => x.Id)
               .AsNoTracking()
               .FirstOrDefaultAsync();

            return ToDriverVacationViewModel(driver, driverVacation, replacementDriver);
        }

        public async Task<bool> DeleteDriverVacationsAsync(DeleteDriverVacationContract deleteContract)
        {
            if (deleteContract.DriverVacationIds.Count < 1)
                return true;

            var elems = _context.DriverVacations
                .Where(
                    x => 1 == 1
                    && x.IsDeleted == false
                    && deleteContract.DriverVacationIds.Contains(x.Id));

            foreach (var elem in elems)
            {
                elem.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<DriverVacation, DriverVacationHistory>(
                await elems.ToListAsync(),
                _context,
                deleteContract.UserId,
                deleteContract.UserName
            );

            return true;
        }

        public async Task<IList<DriverVacationViewModel>> GetDriverVacationsWithoutFilterAsync(CommonHttpFilter filter)
        {
            var driverVacationsWithDriver =
                await (from driverVacation in _context.DriverVacations
                            .Where(x =>
                                x.IsDeleted == false)
                       from driver in _context.Drivers
                            .Where(x =>
                                x.IsDeleted == false &&
                                x.Id == driverVacation.DriverId)
                            .DefaultIfEmpty()
                       from replacementDriver in _context.Drivers
                       .Where(x =>
                           x.IsDeleted == false &&
                           x.Id == driverVacation.ReplacementDriverId)
                       .DefaultIfEmpty()
                       select new
                       {
                           Driver = driver,
                           DriverVacation = driverVacation,
                           ReplacementDriver = replacementDriver
                       })
                .OrderByDescending(x => x.DriverVacation.Id)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            var result = new List<DriverVacationViewModel>();
            foreach (var driverVacation in driverVacationsWithDriver)
            {
                result.Add(ToDriverVacationViewModel(
                    driverVacation.Driver, 
                    driverVacation.DriverVacation, 
                    driverVacation.ReplacementDriver)
                );
            }
            return result;
        }

        public async Task<IList<DriverVacationViewModel>> GetFilteredDriverVacationsAsync(DriverVacationFilterModel filter)
        {
            if (filter == null)
                return null;

            var driverVacationsWithDriver =
                await (from driverVacation in _context.DriverVacations
                            .Where(x =>
                                x.IsDeleted == false)
                       from driver in _context.Drivers
                            .Where(x =>
                                x.IsDeleted == false &&
                                x.Id == driverVacation.DriverId)
                            .DefaultIfEmpty()
                       from replacementDriver in _context.Drivers
                       .Where(x =>
                           x.IsDeleted == false &&
                           x.Id == driverVacation.ReplacementDriverId)
                       .DefaultIfEmpty()
                       select new
                       {
                           Driver = driver,
                           DriverVacation = driverVacation,
                           ReplacementDriver = replacementDriver
                       })
                .WhereIf(filter.DriverIds != null && filter.DriverIds.Count > 0, 
                    x => filter.DriverIds.Contains(x.DriverVacation.DriverId))
                .WhereIf(filter.ReplacementDriverIds != null && filter.ReplacementDriverIds.Count > 0,
                    x => x.DriverVacation.ReplacementDriverId.HasValue && filter.ReplacementDriverIds.
                        Contains(x.DriverVacation.ReplacementDriverId.Value))
                .WhereIf(filter.VacationTypes != null && filter.VacationTypes.Count > 0, 
                    x => filter.VacationTypes.Contains(x.DriverVacation.VacationType))
                .WhereIf(filter.CreateDateFrom != null, x => filter.CreateDateFrom <= x.DriverVacation.CreateDate)
                .WhereIf(filter.CreateDateTo != null, x => filter.CreateDateTo >= x.DriverVacation.CreateDate)
                .WhereIf(filter.StartDateFrom != null, x => filter.StartDateFrom <= x.DriverVacation.StartDate)
                .WhereIf(filter.StartDateTo != null, x => filter.StartDateTo >= x.DriverVacation.StartDate)
                .WhereIf(filter.EndDateFrom != null, x => filter.EndDateFrom <= x.DriverVacation.EndDate)
                .WhereIf(filter.EndDateTo != null, x => filter.EndDateTo >= x.DriverVacation.EndDate)
                .OrderByWithSortingModel(filter.Sorting)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            var result = new List<DriverVacationViewModel>();
            foreach (var driverVacation in driverVacationsWithDriver)
            {
                result.Add(ToDriverVacationViewModel(
                    driverVacation.Driver,
                    driverVacation.DriverVacation,
                    driverVacation.ReplacementDriver)
                );
            }
            return result;
        }

        public async Task<IList<DriverVacationViewModel>> GetDriverVacationsAsync(GetDriverVacationContract getContract)
        {
            if (getContract.DriverVacationIds.Count < 1)
                return null;

            var driverVacationsWithDriver =
                await (from driverVacation in _context.DriverVacations
                            .Where(x =>
                                x.IsDeleted == false &&
                                getContract.DriverVacationIds.Contains(x.Id))
                       from driver in _context.Drivers
                            .Where(x =>
                                x.IsDeleted == false &&
                                x.Id == driverVacation.DriverId)
                            .DefaultIfEmpty()
                       from replacementDriver in _context.Drivers
                       .Where(x =>
                           x.IsDeleted == false &&
                           x.Id == driverVacation.ReplacementDriverId)
                       .DefaultIfEmpty()
                       select new
                       {
                           Driver = driver,
                           DriverVacation = driverVacation,
                           ReplacementDriver = replacementDriver
                       }).ToListAsync();

            if (driverVacationsWithDriver == null || driverVacationsWithDriver.Count < 1)
            {
                return null;
            }

            var result = new List<DriverVacationViewModel>();
            foreach (var driverVacation in driverVacationsWithDriver)
            {
                result.Add(ToDriverVacationViewModel(
                    driverVacation.Driver,
                    driverVacation.DriverVacation,
                    driverVacation.ReplacementDriver)
                );
            }
            return result;
        }

        public async Task<DriverVacationViewModel> UpdateDriverVacationsAsync(UpdateDriverVacationContract updateContract)
        {
            var driverVacation = _mapper.Map<DriverVacation>(updateContract);

            var elems = _context.DriverVacations
                .Where(
                    x => x.IsDeleted == false &&
                    x.Id == updateContract.DriverVacationId);

            foreach (var elem in elems)
            {
                driverVacation.Id = elem.Id;
                _mapper.Map(driverVacation, elem);
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<DriverVacation, DriverVacationHistory>(
                 await elems.ToListAsync(),
                 _context,
                 updateContract.UserId,
                 updateContract.UserName
            );

            var driver = await _context.Drivers
                .Where(x => x.IsDeleted == false && x.Id == driverVacation.DriverId)
                .OrderByDescending(x => x.Id)
                .AsNoTracking()
                .FirstAsync();

            var replacementDriver = await _context.Drivers
               .Where(x => x.IsDeleted == false && x.Id == driverVacation.ReplacementDriverId)
               .OrderByDescending(x => x.Id)
               .AsNoTracking()
               .FirstOrDefaultAsync();

            return ToDriverVacationViewModel(driver, driverVacation, replacementDriver);
        }

        private DriverVacationViewModel ToDriverVacationViewModel(
            Driver driver,
            DriverVacation driverVacation,
            Driver replacementDriver = null
        )
        {
            if (driver == null || driverVacation == null)
            {
                return null;
            }

            var driverMap = _mapper.Map<DriverViewModel>(driver);
            var replacementDriverMap = _mapper.Map<DriverViewModel>(replacementDriver);

            var result = _mapper.Map<DriverVacationViewModel>(driverVacation);

            result.DriverName = driverMap.DriverName;
            result.DriverSurname = driverMap.DriverSurname;
            result.DriverPatronymic = driverMap.DriverPatronymic;
            result.DriverPositionType = driverMap.DriverPositionType;
            result.DriverOrganizationId = driverMap.OrganizationId;

            if(replacementDriverMap != null)
            {
                result.ReplacementDriverName = replacementDriverMap.DriverName;
                result.ReplacementDriverSurname = replacementDriverMap.DriverSurname;
                result.ReplacementDriverPatronymic = replacementDriverMap.DriverPatronymic;
                result.ReplacementDriverPositionType = replacementDriverMap.DriverPositionType;
                result.ReplacementDriverOrganizationId = replacementDriverMap.OrganizationId;
            }

            result.DateRange = _mapper.Map<DateRangeBaseModel>(driverVacation);
            return result;
        }
    }
}
