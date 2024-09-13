using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http.Helpers;
using GreenCity.Http;
using TransportService.Database.Interfaces;
using TransportContracts.ViewModels;
using TransportContracts.DataContracts.TransportWorkShift;
using System.Net;
using AuthContracts.DataContracts;
using AuthContracts.Interfaces;
using TransportService.Core.Helpers;

namespace TransportService.Core.BusinessLogic
{
    public class TransportWorkShiftLogic
    {
        private readonly ITransportWorkShiftStorage _transportWorkShiftStorage;
        private readonly IAuthApi _authApi;
        private readonly IMapper _mapper;

        public TransportWorkShiftLogic(
            ITransportWorkShiftStorage transportWorkShiftStorage,
            IAuthApi authApi,
            IMapper mapper
        )
        {
            _transportWorkShiftStorage = transportWorkShiftStorage;
            _authApi = authApi;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<IList<TransportWorkShiftViewModel>>> GetWorkShiftsAsync(
          GetActualWorkShitsContract contract
        )
        {
            if(contract == null)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftViewModel>>(initialError: "Пустой запрос");
            }
            //нормализуем контракт
            contract.DateTime = DateRangeHelper.Normalize(contract.DateTime);
            try
            {
                //если есть фильтрация по организациям пользователя
                if(contract.IsFilterByUser)
                {
                    var getUserOrgsResult = await GetUserOrgsAsync(contract);

                    if (!getUserOrgsResult.Success)
                    {
                        return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftViewModel>>(
                            initialError: "Ошибка при получении ораганизаций",
                            extErrors: getUserOrgsResult.Errors?.ToList()
                        );
                    }

                    //получаем id организаций для удобного использования
                    //и устанавливаем организации в контракт
                    contract.OrganizationIds = getUserOrgsResult.Data;
                }

                var result = await _transportWorkShiftStorage.GetTransportWorkShiftsAsync(contract);
                if (result == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetWorkShiftsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<TransportWorkShiftDropdownViewModel>>> GetWorkShiftsDropdownAsync(
            GetWorkShitsDropdownContract contract
        )
        {
            if (contract == null)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftDropdownViewModel>>(initialError: "Пустой запрос");
            }
            //нормализуем контракт
            contract.DateTime = DateRangeHelper.Normalize(contract.DateTime);
            try
            {
                //если есть фильтрация по организациям пользователя
                if (contract.IsFilterByUser)
                {
                    var getUserOrgsResult = await GetUserOrgsAsync(contract);

                    if (!getUserOrgsResult.Success)
                    {
                        return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftDropdownViewModel>>(
                            initialError: "Ошибка при получении ораганизаций",
                            extErrors: getUserOrgsResult.Errors?.ToList()
                        );
                    }

                    //получаем id организаций для удобного использования
                    //и устанавливаем организации в контракт
                    contract.OrganizationIds = getUserOrgsResult.Data;
                }

                var result = await _transportWorkShiftStorage.GetTransportWorkShiftsDropdownAsync(contract);
                if (result == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftDropdownViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftDropdownViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetWorkShiftsDropdownAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<TransportWorkShiftViewModel>>> GetWorkShiftsByIdsAsync(
            GetWorkShiftsByIdsContract contract
        )
        {
            if (contract == null)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftViewModel>>(initialError: "Пустой запрос");
            }

            IList<int>? orgIds = default;

            try
            {
                //если есть фильтрация по организациям пользователя
                if (contract.IsFilterByUser)
                {
                    var getUserOrgsResult = await GetUserOrgsAsync(contract);

                    if (!getUserOrgsResult.Success)
                    {
                        return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftViewModel>>(
                            initialError: "Ошибка при получении ораганизаций",
                            extErrors: getUserOrgsResult.Errors?.ToList()
                        );
                    }

                    orgIds = getUserOrgsResult.Data;
                }

                var result = await _transportWorkShiftStorage.GetWorkShiftByIdsAsync(contract, orgIds);
                if (result == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetWorkShiftsDropdownAsync)}");
            }
        }

        private async Task<CommonHttpResponse<IList<int>>> GetUserOrgsAsync(CommonHttpContract contract)
        {
            //получаем экипажи по организациям юзера
            //создаем контракт
            var orgContract = new GetOrgsContract()
            {
                IsFilterByUserId = true,
                RequestId = contract.RequestId,
                UserId = contract.UserId,
                UserName = contract.UserName
            };
            //получаем организации пользователя
            var userOrganizationsRes = await _authApi.GetOrgsContractAsync(orgContract);

            //проверяем на успешность и конвертируем
            if (!userOrganizationsRes.Success)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<int>>(
                    userOrganizationsRes.StatusCode,
                    userOrganizationsRes.Errors.ToList()
                );
            }

            return CommonHttpHelper.BuildSuccessResponse<IList<int>>(
                userOrganizationsRes.Data.Select(el => el.OrganizationId)
                .Distinct()
                .ToList()
            );
        }

        public async Task<CommonHttpResponse> ChangeWorkshiftRouteStatusAsync(
            ChangeWorkshiftRouteStatusContract contract
        ) 
        {
            if (contract == null || !contract.WorkshiftIds.Any())
            {
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");
            }
            try
            {
                if (await _transportWorkShiftStorage.ChangeWorkshiftRouteStatusAsync(contract))
                {
                    return CommonHttpHelper.BuildSuccessResponse(HttpStatusCode.OK);
                }
                return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.InternalServerError, 
                    initialError: "Ошибка при смене статуса рабочих смен");
            }
            catch ( Exception ex )
            {
                return CommonHttpHelper.BuildErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetWorkShiftsDropdownAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<TransportWorkShiftViewModel>>> GetWorkShiftsForDoneTaskListAsync(
            GetWorkShiftsForDoneTaskListContract contract)
        {
            if (contract == null)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftViewModel>>(
                    initialError: "Пустой запрос");
            }

            try
            {
                var workShifts = await _transportWorkShiftStorage.GetWorkShiftsForDoneTaskListAsync(contract);

                if (workShifts == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftViewModel>>(
                        HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(workShifts, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportWorkShiftViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetWorkShiftsForDoneTaskListAsync)}");
            }
        }
    }
}
