using AutoMapper;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using GreenCity.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportContracts.DataContracts.TransportDutyShift;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Database.Models;

namespace TransportService.Database.Implements
{
    public class TransportDutyShiftStorage : ITransportDutyShiftStorage
    {
        private readonly TransportDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public TransportDutyShiftStorage(
            TransportDbContext context,
            FillHistoryService fillHistoryService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
        }

        public async Task<TransportDutyShiftViewModel> CreateTransportDutyShiftAsync(CreateTransportDutyShiftContract createContract)
        {
            var transportDutyShift = _mapper.Map<TransportDutyShift>(createContract);

            await _context.TransportDutyShifts.AddAsync(transportDutyShift);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<TransportDutyShift, TransportDutyShiftHistory>(
                new List<TransportDutyShift> { transportDutyShift },
                _context,
                createContract.UserId,
                createContract.UserName
            );

            var transport = await _context.Transports
               .Where(x => x.IsDeleted == false && x.Id == transportDutyShift.TransportId)
               .OrderByDescending(x => x.Id)
               .AsNoTracking()
               .FirstAsync();

            return ToTransportDutyShiftViewModel(transport, transportDutyShift);
        }

        public async Task<bool> DeleteTransportDutyShiftsAsync(DeleteTransportDutyShiftContract deleteContract)
        {
            if (deleteContract.TransportDutyShiftIds.Count < 1)
                return true;

            var elems = _context.TransportDutyShifts
                .Where(x =>
                    x.IsDeleted == false &&
                    deleteContract.TransportDutyShiftIds.Contains(x.Id));

            foreach (var elem in elems)
            {
                elem.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<TransportDutyShift, TransportDutyShiftHistory>(
                await elems.ToListAsync(),
                _context,
                deleteContract.UserId,
                deleteContract.UserName
            );

            return true;
        }

        public async Task<IList<TransportDutyShiftViewModel>> GetTransportDutyShiftsWithoutFilterAsync(CommonHttpFilter filter)
        {
            var transportDutyShiftWithTransport =
               await (from transportDutyShift in _context.TransportDutyShifts
                           .Where(x =>
                               x.IsDeleted == false)
                      from transport in _context.Transports
                           .Where(x =>
                              x.IsDeleted == false &&
                              x.Id == transportDutyShift.TransportId)
                      .DefaultIfEmpty()
                      select new
                      {
                          TransportDutyShift = transportDutyShift,
                          Transport = transport
                      })
               .OrderByDescending(x => x.TransportDutyShift.Id)
               .Paging(filter.ItemsOnPage, filter.Page)
               .AsNoTracking()
               .ToListAsync();

            var result = new List<TransportDutyShiftViewModel>();
            foreach (var transportDutyShift in transportDutyShiftWithTransport)
            {
                result.Add(ToTransportDutyShiftViewModel(
                    transportDutyShift.Transport,
                    transportDutyShift.TransportDutyShift)
                );
            }
            return result;
        }

        public async Task<IList<TransportDutyShiftViewModel>> GetTransportDutyShiftsAsync(GetTransportDutyShiftContract getContract)
        {
            if (getContract.TransportDutyShiftIds.Count < 1)
                return null;

            var transportDutyShiftWithTransport =
               await (from transportDutyShift in _context.TransportDutyShifts
                           .Where(x =>
                                x.IsDeleted == false &&
                                getContract.TransportDutyShiftIds.Contains(x.Id))
                      from transport in _context.Transports
                           .Where(x =>
                              x.IsDeleted == false &&
                              x.Id == transportDutyShift.TransportId)
                      .DefaultIfEmpty()
                      select new
                      {
                          TransportDutyShift = transportDutyShift,
                          Transport = transport
                      })
               .ToListAsync();

            var result = new List<TransportDutyShiftViewModel>();
            foreach (var transportDutyShift in transportDutyShiftWithTransport)
            {
                result.Add(ToTransportDutyShiftViewModel(
                    transportDutyShift.Transport,
                    transportDutyShift.TransportDutyShift)
                );
            }
            return result;
        }

        public async Task<IList<TransportDutyShiftViewModel>> GetFilteredTransportDutyShiftsAsync(TransportDutyShiftFilterModel filter)
        {
            if (filter == null)
                return null;

            var transportDutyShiftWithTransport =
               await (from transportDutyShift in _context.TransportDutyShifts
                           .Where(x =>
                               x.IsDeleted == false)
                      from transport in _context.Transports
                           .Where(x =>
                              x.IsDeleted == false &&
                              x.Id == transportDutyShift.TransportId)
                      .DefaultIfEmpty()
                      select new
                      {
                          TransportDutyShift = transportDutyShift,
                          Transport = transport
                      })
                .WhereIf(filter.TransportIds != null && filter.TransportIds.Count > 0,
                    x => filter.TransportIds.Contains(x.TransportDutyShift.TransportId))
                .WhereIf(filter.DateFrom != null, x => filter.DateFrom <= x.TransportDutyShift.Date)
                .WhereIf(filter.DateTo != null, x => filter.DateTo >= x.TransportDutyShift.Date)
                .OrderByWithSortingModel(filter.Sorting)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            var result = new List<TransportDutyShiftViewModel>();
            foreach (var transportDutyShift in transportDutyShiftWithTransport)
            {
                result.Add(ToTransportDutyShiftViewModel(
                    transportDutyShift.Transport,
                    transportDutyShift.TransportDutyShift)
                );
            }
            return result;
        }

        private TransportDutyShiftViewModel ToTransportDutyShiftViewModel(
            Transport transport,
            TransportDutyShift transportDutyShift
        )
        {
            if (transportDutyShift == null || transport == null)
            {
                return null;
            }

            var transportMap = _mapper.Map<TransportViewModel>(transport);

            var result = _mapper.Map<TransportDutyShiftViewModel>(transportDutyShift);
            result.TransportName = transportMap.Name;
            result.TransportType = transportMap.TransportType;
            result.RegNumber = transportMap.RegNumber;
            result.TransportOrganizationId = transportMap.OrganizationId;

            return result;
        }
    }
}
