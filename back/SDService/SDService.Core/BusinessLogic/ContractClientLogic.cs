using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using SDContracts.DataContracts.ContractClient;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels;
using SDContracts.ViewModels.Container.ContractClient;
using SDContracts.ViewModels.Contract;
using SDService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SDService.Core.BusinessLogic
{
    public class ContractClientLogic
    {
        private readonly IContractClientStorage _contractClientStorage;
        private readonly IMapper _mapper;

        public ContractClientLogic(
            IContractClientStorage contractClientStorage,
            IMapper mapper
        )
        {
            _contractClientStorage = contractClientStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<int>> CreateContractClientAsync(CreateContractClientContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            try
            {
                var result = await _contractClientStorage.CreateContractClientAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<int>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateContractClientAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse> UpdateContractClientAsync(UpdateContractClientContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _contractClientStorage.UpdateContractClientAsync(contract))
                {
                    return CommonHttpHelper.BuildSuccessResponse();
                }

                return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена");
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(UpdateContractClientAsync)}");
            }
        }

        public async Task<CommonHttpResponse<ContractClientViewModel>> GetContractClientAsync(
            GetContractClientContract contract
        )
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<ContractClientViewModel>(initialError: "Пустой запрос");

            try
            {
                var transport = await _contractClientStorage.GetContractClientAsync(contract.ContractClientId);
                if (transport == null)
                    return CommonHttpHelper.BuildErrorResponse<ContractClientViewModel>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(transport, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<ContractClientViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetContractClientAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<ContractClientViewModel>>> GetFilteredContractClientAsync(
            ContractClientFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<ContractClientViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _contractClientStorage.GetFilteredContractClientsAsync(filter) :
                    await _contractClientStorage.GetContractClientsWithoutFilterAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<ContractClientViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredContractClientAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteContractClientAsync(DeleteContractClientContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _contractClientStorage.DeleteContractClientAsync(contract))
                {
                    return CommonHttpHelper.BuildSuccessResponse();
                }

                return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена");
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(DeleteContractClientAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<ContractClientDropDownViewModel>>> GetDropDownContractClientAsync(
            GetDropDownContractClientFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<ContractClientDropDownViewModel>>(initialError: "Пустой запрос");

            var result = await _contractClientStorage.GetDropDownContracClientAsync(filter);

            return CommonHttpHelper.BuildSuccessResponse<IList<ContractClientDropDownViewModel>>(
                result.Select(x => new ContractClientDropDownViewModel { Id = x.Id, Name = x.ClientName }).ToList(),
                HttpStatusCode.OK
            );
        }
    }
}
