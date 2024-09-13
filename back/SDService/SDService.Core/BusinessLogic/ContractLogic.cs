using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using SDContracts.DataContracts.Contract;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels;
using SDContracts.ViewModels.Contract;
using SDService.Core.Extensions;
using SDService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SDService.Core.BusinessLogic
{
    public class ContractLogic
    {
        private readonly IContractStorage _contractStorage;
        private readonly IContractClientStorage _contractClientStorage;
        private readonly IMapper _mapper;

        public ContractLogic(
            IContractStorage contractStorage,
            IContractClientStorage contractClientStorage,
            IMapper mapper
        )
        {
            _contractClientStorage = contractClientStorage;
            _contractStorage = contractStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<int>> CreateContractAsync(CreateContractContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            //!_! Проверяем наличие контрагента
            if (!await _contractClientStorage.CheckContracClientById(contract.ContractClientId))
                return CommonHttpHelper.BuildErrorResponse<int>(HttpStatusCode.NotFound, initialError:
                    $"Контрагент с таким идентификатором не найден ИД {contract.ContractClientId}");

            //!_! Проставляем дату приоритетов надеемся на то что фронт высылает в нужном порядке
            if (contract.AttachedContainers != default)
            {
                foreach (var container in contract.AttachedContainers)
                {
                    container.ContractScheduleCollection.AddCreationDate();
                }
            }
            var result = await _contractStorage.CreateContractAsync(contract);

            return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse> UpdateContractsAsync(UpdateContractContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            //!_! Проверяем наличие контрагента
            if (!await _contractClientStorage.CheckContracClientById(contract.ContractClientId))
                return CommonHttpHelper.BuildErrorResponse(initialError:
                    $"Контрагент с таким идентификатором не найден {contract.ContractClientId}");

            //!_! Проставляем дату приоритетов надеемся на то что фронт высылает в нужном порядке
            if (contract.AttachedContainers != default)
            {
                foreach (var container in contract.AttachedContainers)
                {
                    container.ContractScheduleCollection.AddCreationDate();
                }
            }
            if (await _contractStorage.UpdateContractAsync(contract))
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена или возникла ошибка (смотри логи)");
        }

        public async Task<CommonHttpResponse> BulkUpdateContractsAsync(BulkUpdateContractContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            if (await _contractStorage.BulkUpdateContractAsync(contract))
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена или возникла ошибка (смотри логи)");
        }

        public async Task<CommonHttpResponse<ContractViewModel>> GetContractAsync(
            GetContractContract contract
        )
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<ContractViewModel>(initialError: "Пустой запрос");

            try
            {
                var result = await _contractStorage.GetContractAsync(contract);
                if (result == null)
                    return CommonHttpHelper.BuildErrorResponse<ContractViewModel>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<ContractViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetContractAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<ContractViewModel>>> GetFilteredContractsAsync(
            ContractFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<ContractViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _contractStorage.GetFilteredContractsAsync(filter) :
                    await _contractStorage.GetContractsWithoutFilterAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<ContractViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredContractsAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteContractsAsync(DeleteContractContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            if (await _contractStorage.DeleteContractsAsync(contract))
            {
                return CommonHttpHelper.BuildSuccessResponse();
            }

            return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.NotFound, null, "Запись не найдена или возникла ошибка (смотри логи)");
        }

        public async Task<CommonHttpResponse<IList<ContractDropDownViewModel>>> GetDropDownContractsAsync(
            GetDropDownContractsFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<ContractDropDownViewModel>>(initialError: "Пустой запрос");

            var result = await _contractStorage.GetDropDownContractsAsync(filter);

            return CommonHttpHelper.BuildSuccessResponse<IList<ContractDropDownViewModel>>(
                result.Select(x => new ContractDropDownViewModel { Id = x.Id, ContractNumber = x.ContractNumber }).ToList(),
                HttpStatusCode.OK
            );
        }
    }
}
