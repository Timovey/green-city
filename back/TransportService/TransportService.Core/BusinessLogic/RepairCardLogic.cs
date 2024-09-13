using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http.Helpers;
using GreenCity.Http;
using System.Net;
using TransportContracts.DataContracts.RepairCard;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportContracts.DataContracts.Transport;
using TransportContracts.DataContracts.Enums;
using TransportService.Core.Helpers;

namespace TransportService.Core.BusinessLogic
{
    public class RepairCardLogic
    {
        private readonly IRepairCardStorage _repairCardStorage;
        private readonly ITransportStorage _transportStorage;
        private readonly ITtansportSheduleStorage _transportSheduleStorage;
        private readonly IMapper _mapper;

        public RepairCardLogic(
            IRepairCardStorage repairCardStorage,
            ITransportStorage transportStorage,
            ITtansportSheduleStorage transportSheduleStorage,
            IMapper mapper
        )
        {
            _repairCardStorage = repairCardStorage;
            _transportStorage = transportStorage;
            _transportSheduleStorage = transportSheduleStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse<RepairCardViewModel>> CreateRepairCardAsync(CreateRepairCardContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError: "Пустой запрос");

            try
            {
                var transports = await _transportStorage.GetTransportsAsync(new GetTransportContract()
                {
                    TransportIds = new List<int> { contract.TransportId }
                });
                if (transports == null || transports.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError: "Нет такого транспорта");
                }
                //нормализуем диапазон
                contract.DateRange.Normalize();

                //проверка на то, что дата начала раньше даты окончания
                if (contract.DateRange.EndDate.Value.Date < contract.DateRange.StartDate.Date)
                {
                    return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError:
                        "Дата окончания должна быть не меньше, чем дата начала");
                }
                if ((contract.WorkShift == WorkShift.First
                    || contract.WorkShift == WorkShift.Second) && contract.Event != RepairType.Repair)
                {
                    return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError:
                            "В определенную смену может производится только ремонт");
                }
                if ((contract.WorkShift == WorkShift.First
                    || contract.WorkShift == WorkShift.Second) && (contract.DateRange.EndDate -
                    contract.DateRange.StartDate).Value.TotalDays > 1)
                {
                    return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError:
                            "Смену ремонта можно проставлять, если ремонт длится 1 день");
                }
                ////проверка на ремонт в 1 и 2 смену только мусоровозов
                //if (contract.WorkShift == WorkShift.First 
                //    || contract.WorkShift == WorkShift.Second)
                //{
                //    if(transports.First().TransportType == TransportType.BunkerTruck 
                //        || transports.First().TransportType == TransportType.DumpTruck)
                //    {
                //        return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError: 
                //            "Чинится в определенную смену могут только мусоровозы");
                //    }

                //    if(contract.DateRange.EndDate.Value.Date != contract.DateRange.StartDate.Date)
                //    {
                //        return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError:
                //          "Если ремонт длится более 1 дня, то указывать смену не нужно");
                //    }
                //}
                //если не заполнена смена - значит чиним весь день
                if (contract.WorkShift == WorkShift.Unknown)
                {
                    contract.WorkShift = WorkShift.AllDay;
                }

                var result = await _repairCardStorage.CreateRepairCardAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateRepairCardAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<RepairCardViewModel>> UpdateRepairCardsAsync(UpdateRepairCardContract contract)
        {
            if (contract?.RepairCardIds == null)
                return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError: "Пустой запрос");

            if(contract.RepairCardIds.Count == 0)
            {
                return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError: "Не выбраны обновляемые сущности");
            }
            try
            {
                var transports = await _transportStorage.GetTransportsAsync(new GetTransportContract()
                {
                    TransportIds = new List<int> { contract.TransportId }
                });
                if (transports == null || transports.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError: "Нет такого транспорта");
                }
                //нормализуем диапазон
                contract.DateRange.Normalize();

                //проверка на то, что дата начала раньше даты окончания
                if (contract.DateRange.EndDate.Value.Date < contract.DateRange.StartDate.Date)
                {
                    return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError:
                        "Дата окончания должна быть не меньше, чем дата начала");
                }
                if ((contract.WorkShift == WorkShift.First
                    || contract.WorkShift == WorkShift.Second) && contract.Event != RepairType.Repair)
                {
                    return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError:
                            "В определенную смену может производится только ремонт");
                }
                if ((contract.WorkShift == WorkShift.First
                    || contract.WorkShift == WorkShift.Second) && (contract.DateRange.EndDate - 
                    contract.DateRange.StartDate).Value.TotalDays > 1)
                {
                    return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError:
                            "Смену ремонта можно проставлять, если ремонт длится 1 день");
                }

                ////проверка на ремонт в 1 и 2 смену только мусоровозов
                //if (contract.WorkShift == WorkShift.First
                //    || contract.WorkShift == WorkShift.Second)
                //{
                //    if (transports.First().TransportType == TransportType.BunkerTruck
                //        || transports.First().TransportType == TransportType.DumpTruck)
                //    {
                //        return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError:
                //            "Чинится в определенную смену могут только мусоровозы");
                //    }

                //    if (contract.DateRange.EndDate.Value.Date != contract.DateRange.StartDate.Date)
                //    {
                //        return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(initialError:
                //          "Если ремонт длится более 1 дня, то указывать смену не нужно");
                //    }
                //}
                //если не заполнена смена - значит чиним весь день
                if (contract.WorkShift == WorkShift.Unknown)
                {
                    contract.WorkShift = WorkShift.AllDay;
                }

                var result = await _repairCardStorage.UpdateRepairCardsAsync(contract);
                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<RepairCardViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(UpdateRepairCardsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<RepairCardViewModel>>> GetRepairCardsAsync(
            GetRepairCardContract contract
        )
        {
            if (contract?.RepairCardIds == null)
                return CommonHttpHelper.BuildErrorResponse<IList<RepairCardViewModel>>(initialError: "Пустой запрос");

            try
            {
                var transport = await _repairCardStorage.GetRepairCardsAsync(contract);
                if (transport == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<RepairCardViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(transport, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<RepairCardViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetRepairCardsAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<RepairCardViewModel>>> GetFilteredRepairCardsAsync(
            RepairCardFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<RepairCardViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _repairCardStorage.GetFilteredRepairCardsAsync(filter) :
                    await _repairCardStorage.GetFilteredRepairCardsAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<RepairCardViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredRepairCardsAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteRepairCardsAsync(DeleteRepairCardContract contract)
        {
            if (contract?.RepairCardIds == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _repairCardStorage.DeleteRepairCardsAsync(contract))
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
                    $"Ошибка выполнения метода {nameof(DeleteRepairCardsAsync)}");
            }
        }
    }
}
