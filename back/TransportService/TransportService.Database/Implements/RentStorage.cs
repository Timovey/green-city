using AutoMapper;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using GreenCity.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Rent;
using TransportContracts.DateRange;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Database.Models;

namespace TransportService.Database.Implements
{
    public class RentStorage : IRentStorage
    {
        private readonly TransportDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public RentStorage(
            TransportDbContext context,
            FillHistoryService fillHistoryService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
        }

        public async Task<RentViewModel> CreateRentAsync(CreateRentContract createContract)
        {
            var rent = _mapper.Map<Rent>(createContract);

            await _context.Rents.AddAsync(rent);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Rent, RentHistory>(
                new List<Rent> { rent },
                _context,
                createContract.UserId,
                createContract.UserName
            );

            var transport = await _context.Transports
               .Where(x => x.IsDeleted == false && x.Id == rent.TransportId)
               .OrderByDescending(x => x.Id)
               .AsNoTracking()
               .FirstAsync();

            return ToRentViewModel(transport, rent);
        }

        public async Task<bool> DeleteRentsAsync(DeleteRentContract deleteContract)
        {
            if (deleteContract.RentIds.Count < 1)
                return true;

            var elems = _context.Rents
                .Where(x =>
                    x.IsDeleted == false &&
                    deleteContract.RentIds.Contains(x.Id));

            foreach (var elem in elems)
            {
                elem.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Rent, RentHistory>(
                await elems.ToListAsync(),
                _context,
                deleteContract.UserId,
                deleteContract.UserName
            );

            return true;
        }

        public async Task<IList<RentViewModel>> GetRentsWithoutFilterAsync(CommonHttpFilter filter)
        {
            var rentWithTransport =
               await (from rent in _context.Rents
                           .Where(x =>
                               x.IsDeleted == false)
                      from transport in _context.Transports
                           .Where(x =>
                              x.IsDeleted == false &&
                              x.Id == rent.TransportId)
                      .DefaultIfEmpty()
                      select new
                      {
                          Rent = rent,
                          Transport = transport
                      })
               .OrderByDescending(x => x.Rent.Id)
               .Paging(filter.ItemsOnPage, filter.Page)
               .AsNoTracking()
               .ToListAsync();

            var result = new List<RentViewModel>();
            foreach (var rent in rentWithTransport)
            {
                result.Add(ToRentViewModel(
                    rent.Transport,
                    rent.Rent)
                );
            }
            return result;
        }

        public async Task<IList<RentViewModel>> GetRentsAsync(GetRentContract getContract)
        {
            if (getContract.RentIds.Count < 1)
                return null;

            var rentWithTransport =
               await (from rent in _context.Rents
                           .Where(x =>
                                x.IsDeleted == false &&
                                getContract.RentIds.Contains(x.Id))
                      from transport in _context.Transports
                           .Where(x =>
                              x.IsDeleted == false &&
                              x.Id == rent.TransportId)
                      .DefaultIfEmpty()
                      select new
                      {
                          Rent = rent,
                          Transport = transport
                      })
               .ToListAsync();

            var result = new List<RentViewModel>();
            foreach (var rent in rentWithTransport)
            {
                result.Add(ToRentViewModel(
                    rent.Transport,
                    rent.Rent)
                );
            }
            return result;
        }

        public async Task<IList<RentViewModel>> GetFilteredRentsAsync(RentFilterModel filter)
        {
            if (filter == null)
                return null;

            var rentWithTransport =
               await (from rent in _context.Rents
                           .Where(x =>
                               x.IsDeleted == false)
                      from transport in _context.Transports
                           .Where(x =>
                              x.IsDeleted == false &&
                              x.Id == rent.TransportId)
                      .DefaultIfEmpty()
                      select new
                      {
                          Rent = rent,
                          Transport = transport
                      })
                .WhereIf(filter.TransportIds != null && filter.TransportIds.Count > 0,
                    x => filter.TransportIds.Contains(x.Rent.TransportId))
                .WhereIf(filter.OrganizationIds != null && filter.OrganizationIds.Count > 0,
                    x => filter.OrganizationIds.Contains(x.Rent.OrganizationId))
                .WhereIf(filter.CreateDateFrom != null, x => filter.CreateDateFrom <= x.Rent.CreateDate)
                .WhereIf(filter.CreateDateTo != null, x => filter.CreateDateTo >= x.Rent.CreateDate)
                .WhereIf(filter.StartDateFrom != null, x => filter.StartDateFrom <= x.Rent.StartDate)
                .WhereIf(filter.StartDateTo != null, x => filter.StartDateTo >= x.Rent.StartDate)
                .WhereIf(filter.EndDateFrom != null, x => filter.EndDateFrom <= x.Rent.EndDate)
                .WhereIf(filter.EndDateTo != null, x => filter.EndDateTo >= x.Rent.EndDate)
                .OrderByWithSortingModel(filter.Sorting)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            var result = new List<RentViewModel>();
            foreach (var rent in rentWithTransport)
            {
                result.Add(ToRentViewModel(
                    rent.Transport,
                    rent.Rent)
                );
            }
            return result;
        }

        private RentViewModel ToRentViewModel(
            Transport transport,
            Rent rent
        )
        {
            if (rent == null || transport == null)
            {
                return null;
            }

            var transportMap = _mapper.Map<TransportViewModel>(transport);

            var result = _mapper.Map<RentViewModel>(rent);
            result.TransportName = transportMap.Name;
            result.TransportType = transportMap.TransportType;
            result.RegNumber = transportMap.RegNumber;
            result.TransportOrganizationId = transportMap.OrganizationId;

            result.DateRange = _mapper.Map<DateRangeBaseModel>(rent);
            return result;
        }
    }
}
