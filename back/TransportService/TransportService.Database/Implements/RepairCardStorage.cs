using AutoMapper;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using GreenCity.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportContracts.DataContracts.RepairCard;
using TransportContracts.DateRange;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Database.Models;

namespace TransportService.Database.Implements
{
    public class RepairCardStorage : IRepairCardStorage
    {
        private readonly TransportDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public RepairCardStorage(
            TransportDbContext context,
            FillHistoryService fillHistoryService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
        }

        public async Task<RepairCardViewModel> CreateRepairCardAsync(CreateRepairCardContract createContract)
        {
            var repairCard = _mapper.Map<RepairCard>(createContract);

            await _context.RepairCards.AddAsync(repairCard);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<RepairCard, RepairCardHistory>(
                new List<RepairCard> { repairCard },
                _context,
                createContract.UserId,
                createContract.UserName
            );

            var transport = await _context.Transports
               .Where(x => x.IsDeleted == false && x.Id == repairCard.TransportId)
               .OrderByDescending(x => x.Id)
               .AsNoTracking()
               .FirstAsync();

            return ToRepairCardViewModel(transport, repairCard);
        }

        public async Task<bool> DeleteRepairCardsAsync(DeleteRepairCardContract deleteContract)
        {
            if (deleteContract.RepairCardIds.Count < 1)
                return true;

            var elems = _context.RepairCards
                .Where(
                    x => x.IsDeleted == false
                    && deleteContract.RepairCardIds.Contains(x.Id));

            foreach (var elem in elems)
            {
                elem.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<RepairCard, RepairCardHistory>(
                await elems.ToListAsync(),
                _context,
                deleteContract.UserId,
                deleteContract.UserName
            );

            return true;
        }

        public async Task<IList<RepairCardViewModel>> GetRepairCardsWithoutFilterAsync(CommonHttpFilter filter)
        {
            var repairCardWithTransport =
               await (from repairCard in _context.RepairCards
                           .Where(x =>
                               x.IsDeleted == false)
                      from transport in _context.Transports
                           .Where(x =>
                              x.IsDeleted == false &&
                              x.Id == repairCard.TransportId)
                      .DefaultIfEmpty()
                      select new
                      {
                         RepairCard = repairCard,
                         Transport = transport
                      })
               .OrderByDescending(x => x.RepairCard.Id)
               .Paging(filter.ItemsOnPage, filter.Page)
               .AsNoTracking()
               .ToListAsync();

            var result = new List<RepairCardViewModel>();
            foreach (var repairCard in repairCardWithTransport)
            {
                result.Add(ToRepairCardViewModel(
                    repairCard.Transport,
                    repairCard.RepairCard)
                );
            }
            return result;
        }

        public async Task<IList<RepairCardViewModel>> GetFilteredRepairCardsAsync(RepairCardFilterModel filter)
        {
            if (filter == null)
                return null;

            var repairCardWithTransport =
               await (from repairCard in _context.RepairCards
                           .Where(x =>
                               x.IsDeleted == false)
                      from transport in _context.Transports
                           .Where(x =>
                              x.IsDeleted == false &&
                              x.Id == repairCard.TransportId)
                      .DefaultIfEmpty()
                      select new
                      {
                          RepairCard = repairCard,
                          Transport = transport
                      })
                .WhereIf(filter.Events != null && filter.Events.Count > 0, x => filter.Events.Contains(x.RepairCard.Event))
                .WhereIf(filter.OrganizationIds != null && filter.OrganizationIds.Count > 0,
                    x => x.RepairCard.OrganizationId.HasValue 
                        && filter.OrganizationIds.Contains(x.RepairCard.OrganizationId.Value))
                .WhereIf(filter.TransportIds != null && filter.TransportIds.Count > 0, 
                    x => filter.TransportIds.Contains(x.RepairCard.TransportId))
                .WhereIf(filter.WorkShifts != null && filter.WorkShifts.Count > 0, 
                    x => filter.WorkShifts.Contains(x.RepairCard.WorkShift))
                .WhereIf(filter.CreateDateFrom != null, x => filter.CreateDateFrom <= x.RepairCard.CreateDate)
                .WhereIf(filter.CreateDateTo != null, x => filter.CreateDateTo >= x.RepairCard.CreateDate)
                .WhereIf(filter.StartDateFrom != null, x => filter.StartDateFrom <= x.RepairCard.StartDate)
                .WhereIf(filter.StartDateTo != null, x => filter.StartDateTo >= x.RepairCard.StartDate)
                .WhereIf(filter.EndDateFrom != null, x => filter.EndDateFrom <= x.RepairCard.EndDate)
                .WhereIf(filter.EndDateTo != null, x => filter.EndDateTo >= x.RepairCard.EndDate)
                .OrderByWithSortingModel(filter.Sorting)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            var result = new List<RepairCardViewModel>();
            foreach (var repairCard in repairCardWithTransport)
            {
                result.Add(ToRepairCardViewModel(
                    repairCard.Transport,
                    repairCard.RepairCard)
                );
            }
            return result;
        }

        public async Task<IList<RepairCardViewModel>> GetRepairCardsAsync(GetRepairCardContract getContract)
        {
            if (getContract.RepairCardIds.Count < 1)
                return null;

            var repairCardWithTransport =
               await (from repairCard in _context.RepairCards
                           .Where(x =>
                               x.IsDeleted == false &&
                               getContract.RepairCardIds.Contains(x.Id))
                      from transport in _context.Transports
                           .Where(x =>
                              x.IsDeleted == false &&
                              x.Id == repairCard.TransportId)
                      .DefaultIfEmpty()
                      select new
                      {
                          RepairCard = repairCard,
                          Transport = transport
                      })
               .ToListAsync();

            var result = new List<RepairCardViewModel>();
            foreach (var repairCard in repairCardWithTransport)
            {
                result.Add(ToRepairCardViewModel(
                    repairCard.Transport,
                    repairCard.RepairCard)
                );
            }
            return result;
        }

        public async Task<RepairCardViewModel> UpdateRepairCardsAsync(UpdateRepairCardContract updateContract)
        {
            if (updateContract.RepairCardIds.Count < 1)
                return null;

            var repairCard = _mapper.Map<RepairCard>(updateContract);

            var elems = _context.RepairCards
                .Where(
                    x => x.IsDeleted == false &&
                    updateContract.RepairCardIds.Contains(x.Id));

            foreach (var elem in elems)
            {
                repairCard.Id = elem.Id;
                _mapper.Map(repairCard, elem);
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<RepairCard, RepairCardHistory>(
                 await elems.ToListAsync(),
                 _context,
                 updateContract.UserId,
                 updateContract.UserName
            );

            var transport = await _context.Transports
                .Where(x => x.IsDeleted == false && x.Id == repairCard.TransportId)
                .OrderByDescending(x => x.Id)
                .AsNoTracking()
                .FirstAsync();

            return ToRepairCardViewModel(transport, repairCard);
        }

        private RepairCardViewModel ToRepairCardViewModel(
            Transport transport,
            RepairCard repairCard
        )
        {
            if (repairCard == null || transport == null)
            {
                return null;
            }

            var transportMap = _mapper.Map<TransportViewModel>(transport);

            var result = _mapper.Map<RepairCardViewModel>(repairCard);
            result.TransportName = transportMap.Name;
            result.TransportType = transportMap.TransportType;
            result.RegNumber = transportMap.RegNumber;
            result.TransportOrganizationId = transportMap.OrganizationId;

            result.DateRange = _mapper.Map<DateRangeBaseModel>(repairCard);
            return result;
        }
    }
}
