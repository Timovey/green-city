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
using TransportContracts.DataContracts.Driver;
using TransportContracts.DataContracts.Enums;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Database.Models;

namespace TransportService.Database.Implements
{
    public class DriverStorage : IDriverStorage
    {
        private readonly TransportDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public DriverStorage(
            TransportDbContext context,
            FillHistoryService fillHistoryService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
        }

        public async Task<DriverViewModel> CreateDriverAsync(CreateDriverContract createContract)
        {
            var driver = _mapper.Map<Driver>(createContract);
            await _context.Drivers.AddAsync(driver);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Driver, DriverHistory>(
                new List<Driver> { driver },
                _context,
                createContract.UserId,
                createContract.UserName
            );

            return _mapper.Map<DriverViewModel>(driver);
        }

        public async Task<bool> DeleteDriversAsync(DeleteDriverContract deleteContract)
        {
            if (deleteContract.DriverIds.Count < 1)
                return true;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var elems = _context.Drivers
                      .Where(
                          x => 1 == 1
                          && x.IsDeleted == false
                          && deleteContract.DriverIds.Contains(x.Id)).ToList();

                    foreach (var elem in elems)
                    {
                        elem.IsDeleted = true;
                    }
                    await _context.SaveChangesAsync();

                    await _fillHistoryService.FillHistoryAsync<Driver, DriverHistory>(
                        elems,
                        _context,
                        deleteContract.UserId,
                        deleteContract.UserName
                    );

                    //каскадно удаляем все привязки водителей и отпуска
                    //заполняем историю
                    foreach (var elem in elems)
                    {
                        var vacations = _context.DriverVacations
                           .Where(
                               x => x.IsDeleted == false
                               && elem.Id == x.DriverId);

                        foreach (var vacation in vacations)
                        {
                            vacation.IsDeleted = true;
                        }

                        await _context.SaveChangesAsync();
                        await _fillHistoryService.FillHistoryAsync<DriverVacation, DriverVacationHistory>(
                        await vacations.ToListAsync(),
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
                               && elem.Id == x.DriverId);

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
                    //изменяем записи, где есть сменный водитель
                    foreach (var elem in elems)
                    {
                        var replDriverVacations = _context.DriverVacations
                        .Where(
                            x => x.IsDeleted == false &&
                            x.ReplacementDriverId == elem.Id);

                        foreach (var vac in replDriverVacations)
                        {
                            vac.ReplacementDriverId = null;
                        }
                        await _context.SaveChangesAsync();

                        await _fillHistoryService.FillHistoryAsync<DriverVacation, DriverVacationHistory>(
                             await replDriverVacations.ToListAsync(),
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

        public async Task<IList<DriverViewModel>> GetDriversWithoutFilterAsync(CommonHttpFilter filter)
        {
            var data = await _context.Drivers
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Id)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            return data.Select(_mapper.Map<DriverViewModel>).ToList();
        }

        public async Task<IList<DriverViewModel>> GetFilteredDriversAsync(DriverFilterModel filter)
        {
            if (filter == null)
                return null;

            var data = await _context.Drivers
                .Where(x => x.IsDeleted == false)
                .WhereIf(!filter.TableNumber.IsNullOrEmpty(), x => x.TableNumber.Contains(filter.TableNumber))
                .WhereIf(filter.IsFired.HasValue, x => x.IsFired == filter.IsFired.Value)
                .WhereIf(filter.FiredDateFrom != null, x => x.FiredDate >= filter.FiredDateFrom.Value)
                .WhereIf(filter.FiredDateTo != null, x => x.FiredDate <= filter.FiredDateTo.Value)
                .WhereIf(!filter.DriverName.IsNullOrEmpty(), x => x.DriverName.Contains(filter.DriverName))
                .WhereIf(!filter.DriverSurname.IsNullOrEmpty(), x => x.DriverSurname.Contains(filter.DriverSurname))
                .WhereIf(filter.AcceptWorkDateFrom != null, x => x.AcceptWorkDate >= filter.AcceptWorkDateFrom.Value)
                .WhereIf(filter.AcceptWorkDateTo != null, x => x.AcceptWorkDate <= filter.AcceptWorkDateTo.Value)
                .WhereIf(filter.DriverPositionTypes != null && filter.DriverPositionTypes.Count > 0, x => filter.DriverPositionTypes.Contains(x.DriverPositionType))
                .WhereIf(!filter.SkillClass.IsNullOrEmpty(), x => x.SkillClass.Contains(filter.SkillClass))
                //.WhereIf(!filter.DrivingSkills.IsNullOrEmpty(), x => x.DrivingSkillsJson.SafeDeserialize<IList<DrivingSkillsType>>(null).ToList().Count > 0)
                .WhereIf(!filter.ContactNumber.IsNullOrEmpty(), x => x.ContactNumber.Contains(filter.ContactNumber))
                .WhereIf(!filter.DriversLicense.IsNullOrEmpty(), x => x.DriversLicense.Contains(filter.DriversLicense))
                .WhereIf(!filter.BirthPlace.IsNullOrEmpty(), x => x.BirthPlace.Contains(filter.BirthPlace))
                .WhereIf(!filter.RegAddress.IsNullOrEmpty(), x => x.RegAddress.Contains(filter.RegAddress))
                .WhereIf(!filter.FactAddress.IsNullOrEmpty(), x => x.FactAddress.Contains(filter.FactAddress))
                .WhereIf(!filter.PassportSerie.IsNullOrEmpty(), x => x.PassportSerie.Contains(filter.PassportSerie))
                .WhereIf(!filter.PassportNumber.IsNullOrEmpty(), x => x.PassportNumber.Contains(filter.PassportNumber))
                .WhereIf(!filter.Issuer.IsNullOrEmpty(), x => x.Issuer.Contains(filter.Issuer))
                .WhereIf(filter.OrganizationIds != null && filter.OrganizationIds.Count > 0, x => x.OrganizationId.HasValue && filter.OrganizationIds.Contains(x.OrganizationId.Value))
                .WhereIf(filter.BirthDateFrom != null, x => x.BirthDate >= filter.BirthDateFrom.Value)
                .WhereIf(filter.BirthDateTo != null, x => x.BirthDate <= filter.BirthDateTo.Value)
                .WhereIf(filter.IssueDateFrom != null, x => x.IssueDate >= filter.IssueDateFrom.Value)
                .WhereIf(filter.IssueDateTo != null, x => x.IssueDate <= filter.IssueDateTo.Value)
                .OrderByWithSortingModel(filter.Sorting)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            //добавили фильтр сюда т.к. в ef core 7 запрос linq не может выполнятся на стороне клиента в 
            //конструкциях whereif и where
            return data.Where(el => CheckDrivingSkills(filter.DrivingSkills, el.DrivingSkillsJson)).Select(x => _mapper.Map<DriverViewModel>(x)).ToList();
        }

        private bool CheckDrivingSkills(IList<DrivingSkillsType> drivingSkillsTypes, string drivingSkillsJson)
        {
            if(drivingSkillsTypes == null || drivingSkillsTypes.Count == 0)
            {
                return true;
            }
            var storedDrivingSkills = drivingSkillsJson.SafeDeserialize<IList<DrivingSkillsType>>(null);
            foreach (var ds in drivingSkillsTypes)
            {
                if (storedDrivingSkills.Contains(ds))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IList<DriverViewModel>> GetDriversAsync(GetDriverContract getContract)
        {
            if (getContract.DriverIds.Count < 1)
                return null;

            var elems = await _context.Drivers
                .Where(
                    x => x.IsDeleted == false &&
                    getContract.DriverIds.Contains(x.Id))
                .ToListAsync();

            if (elems == null || elems.Count < 1)
            {
                return null;
            }

            return elems.Select(_mapper.Map<DriverViewModel>).ToList();
        }

        public async Task<DriverViewModel> UpdateDriversAsync(UpdateDriverContract updateContract)
        {
            var driver = _mapper.Map<Driver>(updateContract);

            var elems = _context.Drivers
                .Where(
                    x => x.IsDeleted == false &&
                    x.Id == updateContract.DriverId);

            foreach (var elem in elems)
            {
                driver.Id = elem.Id;
                _mapper.Map(driver, elem);
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<Driver, DriverHistory>(
                 await elems.ToListAsync(),
                 _context,
                 updateContract.UserId,
                 updateContract.UserName
            );

            return _mapper.Map<DriverViewModel>(elems.First());
        }
    }
}
