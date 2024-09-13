using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http.Helpers;
using GreenCity.Http;
using System.Net;
using DispatcherService.Database.Interfaces;
using DispatcherService.Contracts.ViewModels;
using DispatcherService.Contracts.DataContracts.AuxiliaryWorker;
using DispatcherService.Contracts.Filters;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace DispatcherService.Core.BusinessLogic
{
    public class AuxiliaryWorkerLogic
    {
        private readonly IAuxiliaryWorkerStorage _auxiliaryWorkerStorage;
        private readonly IMapper _mapper;

        public AuxiliaryWorkerLogic(
            IAuxiliaryWorkerStorage auxiliaryWorkerStorage,
            IMapper mapper
        )
        {
            _auxiliaryWorkerStorage = auxiliaryWorkerStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<AuxiliaryWorkerViewModel>> CreateAuxiliaryWorkerAsync(CreateAuxiliaryWorkerContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<AuxiliaryWorkerViewModel>(initialError: "Пустой запрос");

            try
            {
                var result = await _auxiliaryWorkerStorage.CreateAuxiliaryWorkerAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<AuxiliaryWorkerViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateAuxiliaryWorkerAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<AuxiliaryWorkerViewModel>> UpdateAuxiliaryWorkerAsync(UpdateAuxiliaryWorkerContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<AuxiliaryWorkerViewModel>(initialError: "Пустой запрос");

            try
            {
                //если мы разуволили сотрудника - убираем дату увольнения
                if (!contract.IsFired)
                {
                    contract.FiredDate = null;
                }
                var result = await _auxiliaryWorkerStorage.UpdateAuxiliaryWorkerAsync(contract);
                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<AuxiliaryWorkerViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(UpdateAuxiliaryWorkerAsync)}");
            }
        }

        public async Task<CommonHttpResponse<AuxiliaryWorkerViewModel>> GetAuxiliaryWorkerAsync(
            GetAuxiliaryWorkerContract contract
        )
        {
            if (contract?.Id <= 0)
                return CommonHttpHelper.BuildErrorResponse<AuxiliaryWorkerViewModel>(initialError: "Ид не может быть меньше 1");

            try
            {
                var auxiliaryWorker = await _auxiliaryWorkerStorage.GetAuxiliaryWorkerAsync(contract);
                if (auxiliaryWorker == null)
                    return CommonHttpHelper.BuildErrorResponse<AuxiliaryWorkerViewModel>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(auxiliaryWorker, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<AuxiliaryWorkerViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetAuxiliaryWorkerAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<AuxiliaryWorkerViewModel>>> GetFilteredAuxiliaryWorkersAsync(
            AuxiliaryWorkerFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<AuxiliaryWorkerViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _auxiliaryWorkerStorage.GetFilteredAuxiliaryWorkersAsync(filter) :
                    await _auxiliaryWorkerStorage.GetAuxiliaryWorkersWithoutFilterAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<AuxiliaryWorkerViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredAuxiliaryWorkersAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteAuxiliaryWorkerAsync(DeleteAuxiliaryWorkerContract contract)
        {
            if (contract?.Id <= 0)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Ид не может быть меньше 1");

            try
            {
                if (await _auxiliaryWorkerStorage.DeleteAuxiliaryWorkerAsync(contract))
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
                    $"Ошибка выполнения метода {nameof(DeleteAuxiliaryWorkerAsync)}");
            }
        }
    }
}
