using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services;
using GreenCity.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Transport;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Database.Models;

namespace TransportService.Database.Implements
{
    public class TransportStorage : ITransportStorage
    {
        private readonly TransportDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public TransportStorage(
            TransportDbContext context,
            FillHistoryService fillHistoryService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
        }

        public async Task<TransportViewModel> CreateTransportAsync(CreateTransportContract createContract)
        {
            var transport = _mapper.Map<Transport>(createContract);

            await _context.Transports.AddAsync(transport);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Transport, TransportHistory>(
                new List<Transport> { transport },
                _context,
                createContract.UserId,
                createContract.UserName
            );

            return _mapper.Map<TransportViewModel>(transport);
        }

        public async Task<bool> DeleteTransportsAsync(DeleteTransportContract deleteContract)
        {
            if (deleteContract.TransportIds.Count < 1)
                return true;


            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var elems = _context.Transports
                    .Where(
                        x => 1 == 1
                        && x.IsDeleted == false
                        && deleteContract.TransportIds.Contains(x.Id)).ToList();

                    foreach (var elem in elems)
                    {
                        elem.IsDeleted = true;

                    }
                    await _context.SaveChangesAsync();
                    await _fillHistoryService.FillHistoryAsync<Transport, TransportHistory>(
                        elems,
                        _context,
                        deleteContract.UserId,
                        deleteContract.UserName
                    );
                    //каскадно удаляем все расписания и карточки ремонтов
                    //заполняем историю
                    foreach (var elem in elems)
                    {
                        var shedules = _context.TransportShedules
                        .Where(x =>
                            x.IsDeleted == false &&
                            x.TransportId == elem.Id);

                        foreach (var shedule in shedules)
                        {
                            shedule.IsDeleted = true;
                        }
                        await _context.SaveChangesAsync();
                        await _fillHistoryService.FillHistoryAsync<TransportShedule, TransportSheduleHistory>(
                            await shedules.ToListAsync(),
                            _context,
                            deleteContract.UserId,
                            deleteContract.UserName
                        );
                    }
                    foreach (var elem in elems)
                    {
                        var repairs = _context.RepairCards
                        .Where(
                            x => x.IsDeleted == false
                            && x.TransportId == elem.Id);

                        foreach (var repair in repairs)
                        {
                            repair.IsDeleted = true;
                        }
                        await _context.SaveChangesAsync();
                        await _fillHistoryService.FillHistoryAsync<RepairCard, RepairCardHistory>(
                            await repairs.ToListAsync(),
                            _context,
                            deleteContract.UserId,
                            deleteContract.UserName
                        );
                    }

                    foreach (var elem in elems)
                    {
                        var d2ts = _context.Driver2Transports
                           .Where(
                               x => x.IsDeleted == false
                               && elem.Id == x.TransportId);

                        foreach (var d2t in d2ts)
                        {
                            d2t.IsDeleted = true;
                        }

                        await _context.SaveChangesAsync();

                        await _fillHistoryService.FillHistoryAsync<Driver2Transport, Driver2TransportHistory>(
                        await d2ts.ToListAsync(),
                            _context,
                            deleteContract.UserId,
                            deleteContract.UserName
                        );
                    }

                    foreach (var elem in elems)
                    {
                        var rents = _context.Rents
                           .Where(
                               x => x.IsDeleted == false
                               && elem.Id == x.TransportId);

                        foreach (var rent in rents)
                        {
                            rent.IsDeleted = true;
                        }

                        await _context.SaveChangesAsync();

                        await _fillHistoryService.FillHistoryAsync<Rent, RentHistory>(
                        await rents.ToListAsync(),
                            _context,
                            deleteContract.UserId,
                            deleteContract.UserName
                        );
                    }

                    foreach (var elem in elems)
                    {
                        var transportDutyShifts = _context.TransportDutyShifts
                           .Where(
                               x => x.IsDeleted == false
                               && elem.Id == x.TransportId);

                        foreach (var transportDutyShift in transportDutyShifts)
                        {
                            transportDutyShift.IsDeleted = true;
                        }

                        await _context.SaveChangesAsync();

                        await _fillHistoryService.FillHistoryAsync<TransportDutyShift, TransportDutyShiftHistory>(
                        await transportDutyShifts.ToListAsync(),
                            _context,
                            deleteContract.UserId,
                            deleteContract.UserName
                        );
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
                return true;
            }
        }

        public async Task<IList<TransportViewModel>> GetTransportsWithoutFilterAsync(CommonHttpFilter filter)
        {
            var data = await _context.Transports
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Id)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            return data.Select(_mapper.Map<TransportViewModel>).ToList();
        }

        public async Task<IList<TransportViewModel>> GetFilteredTransportsAsync(TransportFilterModel filter)
        {
            if (filter == null)
                return null;

            var data = await _context.Transports
                .Where(x => x.IsDeleted == false)
                .WhereIf(!filter.Name.IsNullOrEmpty(), x => x.Name.Contains(filter.Name))
                .WhereIf(filter.OrganizationIds != null && filter.OrganizationIds.Count > 0, x => x.OrganizationId.HasValue && filter.OrganizationIds.Contains(x.OrganizationId.Value))
                .WhereIf(!filter.RegNumber.IsNullOrEmpty(), x => x.RegNumber.Contains(filter.RegNumber))
                .WhereIf(filter.TransportTypes != null && filter.TransportTypes.Count > 0, x => filter.TransportTypes.Contains(x.TransportType))
                .WhereIf(!filter.BodyColor.IsNullOrEmpty(), x => x.BodyColor.Contains(filter.BodyColor))
                .WhereIf(!filter.TankFence.IsNullOrEmpty(), x => x.TankFence.Contains(filter.TankFence))
                .WhereIf(filter.CargoVolume.HasValue, x => x.CargoVolume == filter.CargoVolume.Value)
                .OrderByWithSortingModel(filter.Sorting)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            return data.Select(x => _mapper.Map<TransportViewModel>(x)).ToList();
        }

        public async Task<IList<TransportViewModel>> GetTransportsAsync(GetTransportContract getContract)
        {
            if (getContract.TransportIds.Count < 1)
                return null;

            var elems = await _context.Transports
                .Where(
                    x => x.IsDeleted == false &&
                    getContract.TransportIds.Contains(x.Id))
                .ToListAsync();

            if (elems == null || elems.Count < 1)
            {
                return null;
            }

            return elems.Select(_mapper.Map<TransportViewModel>).ToList();
        }

        public async Task<TransportViewModel> UpdateTransportsAsync(UpdateTransportContract updateContract)
        {
            if (updateContract.TransportIds.Count < 1)
                return null;

            var transport = _mapper.Map<Transport>(updateContract);

            var elems = _context.Transports
                .Where(
                    x => x.IsDeleted == false &&
                    updateContract.TransportIds.Contains(x.Id));

            foreach (var elem in elems)
            {
                transport.Id = elem.Id;
                _mapper.Map(transport, elem);
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Transport, TransportHistory>(
                 await elems.ToListAsync(),
                 _context,
                 updateContract.UserId,
                 updateContract.UserName
            );

            return _mapper.Map<TransportViewModel>(transport);
        }

    }
}
