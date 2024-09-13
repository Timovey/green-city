using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Db.Extensions;
using GreenCity.Http;
using Microsoft.EntityFrameworkCore;
using SDContracts.DataContracts.ContractClient;
using SDContracts.Enums;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels;
using SDService.Database.Interfaces;
using SDService.Database.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDService.Database.Implements
{
    public class ContractClientStorage : IContractClientStorage
    {
        private readonly SdDbContext _context;
        private readonly IMapper _mapper;

        public ContractClientStorage(SdDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateContractClientAsync(CreateContractClientContract createContract)
        {
            var contractClient = _mapper.Map<ContractClient>(createContract);

            await _context.ContractClients.AddAsync(contractClient);
            await _context.SaveChangesAsync();

            await InsertInHistory(contractClient, createContract.UserId, createContract.UserName);

            return contractClient.Id;
        }

        public async Task<bool> DeleteContractClientAsync(DeleteContractClientContract deleteContract)
        {
            var elem = await _context.ContractClients.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == deleteContract.ContractClientId);
            if (elem == null)
            {
                return false;
            }

            elem.IsDeleted = true;
            _context.ContractClients.Attach(elem);
            _context.Entry(elem).Property(x => x.IsDeleted).IsModified = true;

            await InsertInHistory(elem, deleteContract.UserId, deleteContract.UserName);

            return true;
        }

        public async Task<ContractClientViewModel> GetContractClientAsync(int contractClientId)
        {
            var elem = await _context.ContractClients.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == contractClientId);
            if (elem == null)
            {
                return null;
            }

            return _mapper.Map<ContractClientViewModel>(elem);
        }

        public async Task<IList<ContractClientViewModel>> GetContractClientsWithoutFilterAsync(CommonHttpFilter filter)
        {
            var data = await _context.ContractClients
                .Where(x => x.IsDeleted == false)
                .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                .WhereIf(filter.IsFilterByCreatedUser, x => x.CreatedUserId == filter.UserId)
                .OrderByDescending(x => x.Id)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            return data.Select(_mapper.Map<ContractClientViewModel>).ToList();
        }

        public async Task<IList<ContractClientViewModel>> GetFilteredContractClientsAsync(ContractClientFilterModel filter)
        {
            var data = await _context.ContractClients
                .WhereIf(filter.IsFilterByCreatedUser, x => x.CreatedUserId == filter.UserId)
                .WhereIf(!filter.ClientName.IsNullOrEmpty(), x => x.ClientName.Contains(filter.ClientName))
                .WhereIf(!filter.ClientContact.IsNullOrEmpty(), x => x.ClientContact.Contains(filter.ClientContact))
                .WhereIf(!filter.Email.IsNullOrEmpty(), x => x.Email.Contains(filter.Email))
                .WhereIf(!filter.Inn.IsNullOrEmpty(), x => x.Inn.Contains(filter.Inn))
                .WhereIf(!filter.Kpp.IsNullOrEmpty(), x => x.Kpp.Contains(filter.Kpp))
                .WhereIf(!filter.Ogrn.IsNullOrEmpty(), x => x.Ogrn.Contains(filter.Ogrn))
                .WhereIf(!filter.Rschet.IsNullOrEmpty(), x => x.Rschet.Contains(filter.Rschet))
                .WhereIf(!filter.Bank.IsNullOrEmpty(), x => x.Bank.Contains(filter.Bank))
                .WhereIf(!filter.Bik.IsNullOrEmpty(), x => x.Bik.Contains(filter.Bik))
                .WhereIf(!filter.Kschet.IsNullOrEmpty(), x => x.Kschet.Contains(filter.Kschet))
                .WhereIf(!filter.LegalAddress.IsNullOrEmpty(), x => x.LegalAddress.Contains(filter.LegalAddress))
                .WhereIf(!filter.PostalAddress.IsNullOrEmpty(), x => x.PostalAddress.Contains(filter.PostalAddress))
                .WhereIf(!filter.SupervizorName.IsNullOrEmpty(), x => x.SupervizorName.Contains(filter.SupervizorName))
                .WhereIf(!filter.SupervizorPosition.IsNullOrEmpty(), x => x.SupervizorPosition.Contains(filter.SupervizorPosition))
                .WhereIf(!filter.ResponsibleTkoPersonName.IsNullOrEmpty(), x => x.ResponsibleTkoPersonName.Contains(filter.ResponsibleTkoPersonName))
                .WhereIf(!filter.ResponsibleTkoPersonPhone.IsNullOrEmpty(), x => x.ResponsibleTkoPersonPhone.Contains(filter.ResponsibleTkoPersonPhone))
                .WhereIf(filter.IsValidated.HasValue, x => x.IsValidated == filter.IsValidated.Value)
                .Where(x => x.IsDeleted == false)
                .Where(x => x.EntityType == (byte)MissionInfoEntityType.Real)
                //.OrderByWithSortingModel(filter.Sorting)
                .Paging(filter.ItemsOnPage, filter.Page)
                .AsNoTracking()
                .ToListAsync();

            return data.Select(x => _mapper.Map<ContractClientViewModel>(x)).ToList();
        }

        public async Task<bool> UpdateContractClientAsync(UpdateContractClientContract updateContract)
        {
            var contractClient = _mapper.Map<ContractClient>(updateContract);

            var elem = await _context.ContractClients.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == contractClient.Id);
            if (elem == null)
            {
                return false;
            }

            _mapper.Map(contractClient, elem);
            await _context.SaveChangesAsync();

            await InsertInHistory(elem, updateContract.UserId, updateContract.UserName);

            return true;
        }

        public async Task<bool> CheckContracClientById(int id)
        {
            var result = await _context.ContractClients
                .Where(x =>
                    x.IsDeleted == false &&
                    x.Id == id)
                .AsNoTracking()
                .AnyAsync();

            return result;
        }

        public async Task<IList<ContractClient>> GetDropDownContracClientAsync(GetDropDownContractClientFilterModel filter)
        {
            var result = await _context.ContractClients
                .Where(x => x.IsDeleted == false)
                .WhereIf(filter.EntityTypes != default && filter.EntityTypes.Count > 0,
                    x => filter.EntityTypes.Contains((MissionInfoEntityType)x.EntityType)
                )
                .WhereIf(!filter.Name.IsNullOrEmpty(), x => x.ClientName.Contains(filter.Name))
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        private async Task InsertInHistory(
            ContractClient contractClient,
            int changeUserId = 0,
            string changeUserName = "no name"
        )
        {
            if (contractClient == null)
                return;

            var history = _mapper.Map<ContractClientHistory>(contractClient);

            history.ChangeDt = DateTime.UtcNow;
            history.DataId = contractClient.Id;
            history.ChangeUserId = changeUserId;
            history.ChangeUserName = changeUserName;

            await _context.ContractClientHistories.AddAsync(history);
            await _context.SaveChangesAsync();
        }
    }
}
