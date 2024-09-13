using AutoMapper;
using DispatcherService.Contracts.DataContracts.AuxiliaryWorker;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.ViewModels;
using DispatcherService.Database.Interfaces;
using DispatcherService.Database.Models;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using GreenCity.Db.Services.History.Interface;
using GreenCity.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DispatcherService.Database.Implements
{
    public class AuxiliaryWorkerStorage : IAuxiliaryWorkerStorage
    {
        private readonly DispatcherDbContext _context;
        private readonly IFillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public AuxiliaryWorkerStorage(
            DispatcherDbContext context,
            IFillHistoryService fillHistoryService,
            IMapper mapper
        )
        {
            _context = context;
            _fillHistoryService = fillHistoryService;
            _mapper = mapper;
        }

        public async Task<AuxiliaryWorkerViewModel> CreateAuxiliaryWorkerAsync(CreateAuxiliaryWorkerContract createContract)
        {
            var auxiliaryWorker = _mapper.Map<AuxiliaryWorker>(createContract);
            await _context.AuxiliaryWorkers.AddAsync(auxiliaryWorker);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<AuxiliaryWorker, AuxiliaryWorkerHistory>(
                new List<AuxiliaryWorker> { auxiliaryWorker },
                _context,
                createContract.UserId,
                createContract.UserName
            );

            return _mapper.Map<AuxiliaryWorkerViewModel>(auxiliaryWorker);
        }

        public async Task<bool> DeleteAuxiliaryWorkerAsync(DeleteAuxiliaryWorkerContract deleteContract)
        {
            var elem = await _context.AuxiliaryWorkers
                .Where(x =>
                    x.IsDeleted == false &&
                    deleteContract.Id == x.Id).FirstOrDefaultAsync();
          
            if(elem == null)
            {
                return false;
            }
            elem.IsDeleted = true;

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<AuxiliaryWorker, AuxiliaryWorkerHistory>(
                new List<AuxiliaryWorker> { elem },
                _context,
                deleteContract.UserId,
                deleteContract.UserName
            );

            return true;
        }

        public async Task<IList<AuxiliaryWorkerViewModel>> GetAuxiliaryWorkersWithoutFilterAsync(CommonHttpFilter filter)
        {
            var data = await _context.AuxiliaryWorkers
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Id)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            return data.Select(_mapper.Map<AuxiliaryWorkerViewModel>).ToList();
        }

        public async Task<IList<AuxiliaryWorkerViewModel>> GetFilteredAuxiliaryWorkersAsync(AuxiliaryWorkerFilterModel filter)
        {
            if (filter == null)
                return null;

            var data = await _context.AuxiliaryWorkers
                .Where(x => x.IsDeleted == false)
                .WhereIf(filter.IsFired.HasValue, x => x.IsFired == filter.IsFired.Value)
                .WhereIf(filter.FiredDateFrom != null, x => x.FiredDate >= filter.FiredDateFrom.Value)
                .WhereIf(filter.FiredDateTo != null, x => x.FiredDate <= filter.FiredDateTo.Value)
                .WhereIf(!filter.Name.IsNullOrEmpty(), x => x.Name.Contains(filter.Name))
                .WhereIf(!filter.Surname.IsNullOrEmpty(), x => x.Surname.Contains(filter.Surname))
                .WhereIf(filter.AcceptWorkDateFrom != null, x => x.AcceptWorkDate >= filter.AcceptWorkDateFrom.Value)
                .WhereIf(filter.AcceptWorkDateTo != null, x => x.AcceptWorkDate <= filter.AcceptWorkDateTo.Value)
                .WhereIf(!filter.ContactNumber.IsNullOrEmpty(), x => x.ContactNumber.Contains(filter.ContactNumber))
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
            return data.Select(x => _mapper.Map<AuxiliaryWorkerViewModel>(x)).ToList();
        }

        public async Task<AuxiliaryWorkerViewModel> GetAuxiliaryWorkerAsync(GetAuxiliaryWorkerContract getContract)
        {
            if (getContract.Id < 1)
                return null;

            var elem = await _context.AuxiliaryWorkers
                .Where(
                    x => x.IsDeleted == false &&
                    getContract.Id ==  x.Id)
                .FirstOrDefaultAsync();

            if (elem == null)
            {
                return null;
            }

            return _mapper.Map<AuxiliaryWorkerViewModel>(elem);
        }

        public async Task<AuxiliaryWorkerViewModel> UpdateAuxiliaryWorkerAsync(UpdateAuxiliaryWorkerContract updateContract)
        {
            var auxiliaryWorker = _mapper.Map<AuxiliaryWorker>(updateContract);

            var elems = _context.AuxiliaryWorkers
                .Where(
                    x => x.IsDeleted == false &&
                    x.Id == updateContract.Id);

            foreach (var elem in elems)
            {
                auxiliaryWorker.Id = elem.Id;
                _mapper.Map(auxiliaryWorker, elem);
            }

            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<AuxiliaryWorker, AuxiliaryWorkerHistory>(
                 await elems.ToListAsync(),
                 _context,
                 updateContract.UserId,
                 updateContract.UserName
            );

            return _mapper.Map<AuxiliaryWorkerViewModel>(elems.First());
        }
    }
}
