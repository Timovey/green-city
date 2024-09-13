using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http.Helpers;
using GreenCity.Http;
using System.Net;
using TransportContracts.DataContracts.TransportShedule;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DataContracts.Transport;
using ScheduleService.Contracts.Interfaces;
//неоднозначность между TransportType из System.Net и Enums
using ScheduleService.Contracts.DataContracts.DateRange;
using TransportService.Core.EnumConverters;
using TransportService.Core.Helpers;
using TransportContracts.DataContracts.TransportWorkShift;
using AuthContracts.Interfaces;
using AuthContracts.DataContracts;

namespace TransportService.Core.BusinessLogic
{
    public class TransportSheduleLogic
    {
        private readonly ITtansportSheduleStorage _transportSheduleStorage;
        private readonly ITransportStorage _transportStorage;
        private readonly IScheduleServiceApi _scheduleServiceApí;
        private readonly IAuthApi _authApi;
        private readonly ITransportWorkShiftStorage _transportWorkShiftStorage;
        private readonly IMapper _mapper;

        public TransportSheduleLogic(
            ITtansportSheduleStorage transportSheduleStorage,
            ITransportStorage transportStorage,
            IScheduleServiceApi scheduleServiceApi,
            IAuthApi authApi,
            ITransportWorkShiftStorage transportWorkShiftStorage,
            IMapper mapper
        )
        {
            _transportSheduleStorage = transportSheduleStorage;
            _transportStorage = transportStorage;
            _scheduleServiceApí = scheduleServiceApi;
            _authApi = authApi;
            _transportWorkShiftStorage = transportWorkShiftStorage;
            _mapper = mapper;
        }

        public async Task<CommonHttpResponse> FillTransportNextWorkDateAsync(ActualizeWorkShitsContract contract)
        {
            DateTime startDate = contract.DateTime.HasValue ? contract.DateTime.Value : DateTime.Now;
            
            DateTime endDate = startDate;
            startDate = DateRangeHelper.Normalize(startDate);
            endDate = DateRangeHelper.Normalize(endDate);
            try
            {
                //получаем весь транспорт
                var allTransports = await _transportStorage.GetTransportsWithoutFilterAsync(new CommonHttpFilter());

                //получаем все организации
                var allOrgRes = await _authApi.GetOrgsContractAsync(new GetOrgsContract
                {
                    IsFilterByUserId = false,
                    RequestId = contract.RequestId,
                    UserId = contract.UserId,
                    UserName = contract.UserName,
                });
                var allOrgIds = new List<int>();
                if(allOrgRes.Success)
                {
                    allOrgIds = allOrgRes.Data.Select(el => el.OrganizationId).ToList();
                }

                //создаем список вспомогательных моделей, которые мы потом
                //промаппим к контракт на создание рабочих смен
                var teams = new List<TeamHelperModel>();
                //проходимся по транспорту и заполняем список работающего транспорта
                //нужно рассматривать с разных сторон организаций, только если есть аренда
                foreach (var transport in allTransports)
                {
                    //со стороны каждой организации просматриваем транспорт
                    foreach(var orgId in allOrgIds)
                    {
                        //получаем расписание транспорта на одинь день
                        var res = await GetTransportSheduleDateRangeAsync(
                            new GetTransportSheduleDateRangeContract()
                            {
                                DateFrom = startDate,
                                DateTo = endDate,
                                TransportId = transport.Id,
                                RequestId = contract.RequestId,
                                UserId = contract.UserId,
                                UserName = contract.UserName,
                                OrganizationId = orgId
                            });
                        if (!res.Success)
                        {
                            continue;
                        }
                        var transportRes = res.Data;
                        foreach (var key in transportRes.ResponseErrors.Keys)
                        {
                            //отбираем, чтобы на этот день работол водитель
                            //кроме графика 5/2
                            if (transportRes.DatesTransport[key] == TransportWorkDateType.WorkThreeThree
                                || transportRes.DatesTransport[key] == TransportWorkDateType.WorkSixThree
                                || transportRes.DatesTransport[key] == TransportWorkDateType.RepairFirst
                                || transportRes.DatesTransport[key] == TransportWorkDateType.RepairSecond)
                            {
                                //прохоимся по всем водителям
                                foreach(var (driverId, driverWorkDateType) in transportRes.DatesDrivers.First().Value)
                                {
                                    //проверяем водителей на то, что они работают
                                    //и не по графику 5/2
                                    if (driverWorkDateType == DriverWorkDateType.WorkFirst ||
                                        driverWorkDateType == DriverWorkDateType.WorkSecond ||
                                        driverWorkDateType == DriverWorkDateType.WorkThreeThree)
                                    {
                                        teams.Add(new TeamHelperModel
                                        {
                                            DateTime = startDate,
                                            DriverId = driverId,
                                            DriverWorkDateType = driverWorkDateType,
                                            OrganizationId = orgId,
                                            TransportId = transport.Id,
                                        });
                                    }

                                }
                            }
                        }
                    }
                }
                //преобразуем в контракты
                var contracts = from team in teams
                                group team by new
                                {
                                    team.DateTime,
                                    team.DriverId,
                                    team.DriverWorkDateType,
                                    team.TransportId
                                } into g
                                select new CreateTransportWorkShiftContract
                                {
                                    DateTime = g.Key.DateTime,
                                    DriverId = g.Key.DriverId,
                                    DriverWorkDateType = g.Key.DriverWorkDateType,
                                    TransportId = g.Key.TransportId,
                                    OrganizationIds = g.Select(x => x.OrganizationId).ToList()
                                };


                //сначала слечует очистить от одинаковых данных

                //далее следует сравнить записи в БД на этот день с новосформированными сущностями
                //получаем из БД записи
                var storedWorkShifts = await _transportWorkShiftStorage.GetTransportWorkShiftsAsync(new GetActualWorkShitsContract()
                {
                    DateTime = startDate,
                    RequestId = contract.RequestId,
                    UserId = contract.UserId,
                    UserName = contract.UserName
                });

                //заводим словарь хранимых записей, если данные устарели
                //и не нашлось эквивалентных - то удаляем
                var actualStoredWorkshifts = new List<int>();
                
                foreach (var c in contracts)
                {
                    bool isEqual = false;
                    foreach (var ws in storedWorkShifts)
                    {
                        if(ws.Equals(c))
                        {
                            isEqual= true;
                            actualStoredWorkshifts.Add(ws.Id);
                            break;
                        }
                    }
                    if(!isEqual)
                    {
                        await _transportWorkShiftStorage.CreateTransportWorkShiftAsync(c);
                    }
                }

                //делаем разность множест и если остаются записи,
                //для которых не было актуальных аналогов, то удаляем
                var deletedWorkshifts = storedWorkShifts.Select(el => el.Id).Except(actualStoredWorkshifts).ToList();
                
                if (deletedWorkshifts.Count > 0) {
                    var result = await _transportWorkShiftStorage.DeleteTransportWorkShiftsAsync(new DeleteTransportWorkShiftsContract
                    {
                        RequestId = contract.RequestId,
                        UserId = contract.UserId,
                        UserName = contract.UserName,
                        TransportWorkShiftsIds = deletedWorkshifts
                    });
                    if(!result)
                    {
                        return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.InternalServerError, initialError:
                            "Ошибка при удалении неактуальных смен");
                    }
                }

                return CommonHttpHelper.BuildSuccessResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(
                   HttpStatusCode.InternalServerError,
                   ex.ToExceptionDetails(),
                   $"Ошибка выполнения метода {nameof(FillTransportNextWorkDateAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<DictinaryDateRangeViewModel>> GetTransportSheduleDateRangeAsync(GetTransportSheduleDateRangeContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(initialError: "Пустой запрос");

            //проверка на то, что дата начала не больше даты окончания
            if (contract.DateTo.Date < contract.DateFrom.Date)
            {
                return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(initialError:
                    "Дата окончания должна быть не меньше, чем дата начала");
            }

            if ((contract.DateTo - contract.DateFrom).TotalDays > 365)
            {
                return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(initialError:
                    "График формируется на диапазон, не больший, чем 1 год");
            }

            try
            {
                List<int> taxOrganizations = new List<int>();
                //в случае получения экипажа мы смотрим на организацию
                if (contract.OrganizationId.HasValue)
                {
                    taxOrganizations.Add(contract.OrganizationId.Value);
                }
                else
                {
                    //создаем контракт
                    var orgContract = new GetOrgsContract()
                    {
                        IsFilterByUserId = true,
                        RequestId = contract.RequestId,
                        UserId = contract.UserId,
                        UserName = contract.UserName
                    };
                    //получаем организации таксировщика
                    var taxOrganizationsRes = await _authApi.GetOrgsContractAsync(orgContract);

                    //проверяем на успешность и конвертируем
                    if (!taxOrganizationsRes.Success)
                    {
                        return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(taxOrganizationsRes.StatusCode,
                         taxOrganizationsRes.Errors.ToList());
                    }
                    //получаем id организаций для удобного использования
                    taxOrganizations = taxOrganizationsRes.Data.Select(el => el.OrganizationId).ToList();
                }
                

                //нормализуем контракт, убираем время из даты
                contract.DateFrom = DateRangeHelper.Normalize(contract.DateFrom);
                contract.DateTo = DateRangeHelper.Normalize(contract.DateTo);

                //получаем красные даты и выходные дни,в которые придется работать
                var redDates = await _scheduleServiceApí.GetActualRedDatesAsync();
                var weekendWorkDates = await _scheduleServiceApí.GetActualWeekendWorkDatesAsync();
                //проверяем на успешность и конвертируем
                if (!redDates.Success)
                {
                    return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(redDates.StatusCode,
                        redDates.Errors.ToList());
                }
                //получаем только даты
                //фильтруем по указанному диапазону
                //нормализуем даты
                var redDatesRes = redDates.Data.Where(x => x.Date >= contract.DateFrom && x.Date <= contract.DateTo)
                    .Select(x => DateRangeHelper.Normalize(x.Date)).ToList();

                //проверяем на успешность и конвертируем
                if (!weekendWorkDates.Success)
                {
                    return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(weekendWorkDates.StatusCode,
                        weekendWorkDates.Errors.ToList());
                }
                //получаем только даты
                //фильтруем по указанному диапазону
                //нормализуем даты
                var weekendWorkDatesRes = weekendWorkDates.Data.Where(x => x.Date >= contract.DateFrom && x.Date <= contract.DateTo)
                    .Select(x => DateRangeHelper.Normalize(x.Date)).ToList();


                //проверяем - есть ли такой транспорт
                var checkTransport = await _transportStorage.GetTransportsAsync(new GetTransportContract()
                {
                    TransportIds = new List<int>() { contract.TransportId }
                });
                if(checkTransport == null || checkTransport.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(initialError:
                        "Нет транспорта с таким Id");
                }

                //получаем большую view, в которой есть вся информация
                var transportDateRange = await _transportSheduleStorage.GetTransportSheduleDateRangeAsync(contract);

                //создаем bool флаг
                //состоит ли транспорт в организации таксировщика
                //если нет организации - считаем, что состоит
                bool orgIsHave = transportDateRange.TransportOrganizationId.HasValue ?
                    taxOrganizations.Contains(transportDateRange.TransportOrganizationId.Value) :
                    true;

                //приводим в удобный формат (только дата) дежурные смены машины
                //при этом нормализуем даты
                IList<DateTime> transportDutyShifts = transportDateRange.TransportDutyShifts.Select(el => DateRangeHelper.Normalize(el.Date)).ToList();

                //извлекаем карточки ремонта/ТО
                //НУЖНО ОСТАВИТЬ ОБРАТНУЮ СВЯЗЬ И УСТАНОВИТЬ GIUD В Базовую сущность, чтобы извлекать событие (ТО, ремонт)
                List <BaseDateRangeModel> repairCards = new();
                foreach(var rc in transportDateRange.RepairCards)
                {
                    var mapRC = _mapper.Map<BaseDateRangeModel>(rc.DateRange);
                    rc.Guid = mapRC.Id;
                    repairCards.Add(mapRC);
                }
                //создаем контракт
                var repairContract = new GetDateRangeContract()
                {
                    UserId = contract.UserId,
                    UserName = contract.UserName,
                    RequestId = contract.RequestId,
                    DateRanges = repairCards
                };
                // скармливаем слайсеру
                var slicedRepairCars = await _scheduleServiceApí.GetSlisedDateRangeAsync(repairContract);

                //проверяем на успешность и конвертируем
                if (!slicedRepairCars.Success)
                {
                    return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(slicedRepairCars.StatusCode, 
                        slicedRepairCars.Errors.ToList());
                }
                var slicedRepairCarsRes = slicedRepairCars.Data.ToList();

                //извлекаем расписание транспорта
                //НУЖНО ОСТАВИТЬ ОБРАТНУЮ СВЯЗЬ И УСТАНОВИТЬ GIUD В Базовую сущность, чтобы извлекать событие 
                List<BaseDateRangeModel> transportShedules = new();
                foreach (var ts in transportDateRange.TransportShedules)
                {
                    var mapTS = _mapper.Map<BaseDateRangeModel>(ts.DateRange);
                    ts.Guid = mapTS.Id;
                    transportShedules.Add(mapTS);
                }
                //создаем контракт
                var transportShedulesContract = new GetDateRangeContract()
                {
                    UserId = contract.UserId,
                    UserName = contract.UserName,
                    RequestId = contract.RequestId,
                    DateRanges = transportShedules
                };
                // скармливаем слайсеру
                var slicedTransportShedules = await _scheduleServiceApí.GetSlisedDateRangeAsync(transportShedulesContract);
                //проверяем на успешность и конвертируем
                if (!slicedTransportShedules.Success)
                {
                    return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(slicedTransportShedules.StatusCode, 
                        slicedTransportShedules.Errors.ToList());
                }
                var slicedTransportShedulesRes = slicedTransportShedules.Data.ToList();

                //извлекаем аренду
                //НУЖНО ОСТАВИТЬ ОБРАТНУЮ СВЯЗЬ И УСТАНОВИТЬ GIUD В Базовую сущность, чтобы извлекать организацию
                List<BaseDateRangeModel> rents = new();
                foreach (var rent in transportDateRange.Rents)
                {
                    var mapRent = _mapper.Map<BaseDateRangeModel>(rent.DateRange);
                    rent.Guid = mapRent.Id;
                    rents.Add(mapRent);
                }
                //создаем контракт
                var rentContract = new GetDateRangeContract()
                {
                    UserId = contract.UserId,
                    UserName = contract.UserName,
                    RequestId = contract.RequestId,
                    DateRanges = rents
                };
                // скармливаем слайсеру
                var slicedRents = await _scheduleServiceApí.GetSlisedDateRangeAsync(rentContract);

                //проверяем на успешность и конвертируем
                if (!slicedRents.Success)
                {
                    return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(slicedRents.StatusCode,
                        slicedRents.Errors.ToList());
                }
                var slicedRentsRes = slicedRents.Data.ToList();

                //словари, которые будем возвращать
                Dictionary<DateTime, TransportWorkDateType> responseDatesTransport = new();
                Dictionary<DateTime, Dictionary<int, DriverWorkDateType>> responseDatesDrivers = new();
                //фактически, словарь с ошибками
                Dictionary<DateTime, DateWorkedDriverType> responseErrors = new();

                //еще один словарь, который наложится поверх транспортного с арендой
                //фактически маска, если в ней аренда - то перезатираем расписание
                //если work - оставляем как есть расписание
                //если неизвестно - то его и проставляем
                Dictionary<DateTime, RentDateType> rentDatesTransport = new();

                //еще 1 словарь - если мы для транспорта перезатираем ремонтом
                //- то в словаре хранятся оригинальное расписание
                Dictionary<DateTime, TransportWorkDateType> origDatesTransport = new();

                for (DateTime date = contract.DateFrom; date <= contract.DateTo; date = date.AddDays(1))
                {
                    //проставляем ключи-даты в словарь с транспортом
                    responseDatesTransport.Add(date, TransportWorkDateType.NotShedule);
                    //проставляем ключи-даты в словарь с арендой
                    //если машина в организации - ставим все рабочее
                    //если машина не в организации - ставим неизвестно
                    rentDatesTransport.Add(date, orgIsHave ? RentDateType.Work : RentDateType.Unknown);
                    //проставляем ключи-даты в словарь с водителями
                    responseDatesDrivers.Add(date, new Dictionary<int, DriverWorkDateType>());
                    //проставляем словарь ошибок
                    responseErrors.Add(date, DateWorkedDriverType.Unknown);

                    //проставляем ключи-даты в словарь с транспортом с его ориг расписанием
                    origDatesTransport.Add(date, TransportWorkDateType.Unknown);
                }

                //заносим данные о расписании транспорта 
                foreach (var transportSh in slicedTransportShedulesRes)
                {
                    //проверяем, не захватили мы данные, которые не входят в диапазон алгоритма
                    DateTime startDate = transportSh.StartDate < contract.DateFrom ? contract.DateFrom : transportSh.StartDate;
                    DateTime endDate = transportSh.EndDate > contract.DateTo ? contract.DateTo : transportSh.EndDate;
                    //приводим к короткому формату (обрезаем минуты и т.д.)
                    startDate = DateRangeHelper.Normalize(startDate);
                    endDate = DateRangeHelper.Normalize(endDate);

                    //узнаем тип графика из оригинальной сущности
                    var work = transportDateRange.TransportShedules.Where(x => x.Guid == transportSh.Id).FirstOrDefault();

                    //проходимся по диапазону дат
                    for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        //если нашелся оригинальный тип графика
                        if(work != null)
                        {
                            //если нашлось, то отдаем конвертеру и присваиваем
                            responseDatesTransport[date] = work.WorkSheduleType.ToTransportWorkType();
                            //проставляем оригинальное расписание для водителей
                            origDatesTransport[date] = work.WorkSheduleType.ToTransportWorkType();
                        }  
                    }   
                }

                //заносим данные о карточках ремонта / ТО
                foreach (var transportRepair in slicedRepairCarsRes)
                {
                    //проверяем, не захватили мы данные, которые не входят в диапазон алгоритма
                    DateTime startDate = transportRepair.StartDate < contract.DateFrom ? contract.DateFrom : transportRepair.StartDate;
                    DateTime endDate = transportRepair.EndDate > contract.DateTo ? contract.DateTo : transportRepair.EndDate;
                    //приводим к короткому формату (обрезаем минуты и т.д.)
                    startDate = DateRangeHelper.Normalize(startDate);
                    endDate = DateRangeHelper.Normalize(endDate);

                    //узнаем тип ТО/Ремонт из оригинальной сущности
                    var repair = transportDateRange.RepairCards.Where(x => x.Guid == transportRepair.Id).FirstOrDefault();

                    for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        if (repair != null)
                        {
                            //если нашлось, то отдаем конвертеру
                            var convSh = repair.Event.ToTransportRepairType(repair.WorkShift);
                            //проверяем, если получили не ТО и не ремонт, то не следует перезатирать данные
                            if (convSh != TransportWorkDateType.NotShedule && convSh != TransportWorkDateType.Unknown)
                            {
                                responseDatesTransport[date] = convSh;
                            }
                        }
                    }
                }

                //проверяем аренду транспорта 
                foreach (var rent in slicedRentsRes)
                {
                    //проверяем, не захватили мы данные, которые не входят в диапазон алгоритма
                    DateTime startDate = rent.StartDate < contract.DateFrom ? contract.DateFrom : rent.StartDate;
                    DateTime endDate = rent.EndDate > contract.DateTo ? contract.DateTo : rent.EndDate;

                    //приводим к короткому формату (обрезаем минуты и т.д.)
                    startDate = DateRangeHelper.Normalize(startDate);
                    endDate = DateRangeHelper.Normalize(endDate);

                    //узнаем тип графика из оригинальной сущности
                    var rentOrig = transportDateRange.Rents.Where(x => x.Guid == rent.Id).FirstOrDefault();

                    //проходимся по диапазону дат
                    for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        if (rentOrig != null)
                        {
                            //если транспорт в организации таксировщика
                            //и сдается в аренду не огранизации таксировщика
                            if (orgIsHave && !taxOrganizations.Contains(rentOrig.OrganizationId))
                            {
                                rentDatesTransport[date] = RentDateType.InRent;
                            }
                            //если транспорт не в организации или в организации таксировщика
                            //и сдается в аренду организации таксировщика
                            else if (taxOrganizations.Contains(rentOrig.OrganizationId))
                            {
                                rentDatesTransport[date] = RentDateType.Work;
                            }
                        }
                    }
                }

                //применяем маску аренды
                for (DateTime date = contract.DateFrom; date <= contract.DateTo; date = date.AddDays(1))
                {
                    responseDatesTransport[date] =responseDatesTransport[date].ToTransportRentWorkType(rentDatesTransport[date]);
                }

                //проходится по каждому из водителей и заполняем словарь
                foreach (var driver in transportDateRange.DriverShedules)
                {
                    //извлекаем расписание водителя
                    //НУЖНО ОСТАВИТЬ ОБРАТНУЮ СВЯЗЬ И УСТАНОВИТЬ GIUD В Базовую сущность
                    List<BaseDateRangeModel> driverShedules = new();
                    foreach (var d2t in driver.Driver2Transports)
                    {
                        var mapD2T = _mapper.Map<BaseDateRangeModel>(d2t.DateRange);
                        d2t.Guid = mapD2T.Id;
                        driverShedules.Add(mapD2T);
                    }
                    //создаем контракт
                    var driverShedulesContract = new GetDateRangeContract()
                    {
                        UserId = contract.UserId,
                        UserName = contract.UserName,
                        RequestId = contract.RequestId,
                        DateRanges = driverShedules
                    };
                    // скармливаем слайсеру
                    var slicedDriverShedules = await _scheduleServiceApí.GetSlisedDateRangeAsync(driverShedulesContract);

                    //проверяем на успешность и конвертируем
                    if (!slicedDriverShedules.Success)
                    {
                        return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(slicedDriverShedules.StatusCode,
                            slicedDriverShedules.Errors.ToList());
                    }
                    var slicedDriverShedulesRes = slicedDriverShedules.Data.ToList();

                    //извлекаем отпуска/больничные водителя
                    //НУЖНО ОСТАВИТЬ ОБРАТНУЮ СВЯЗЬ И УСТАНОВИТЬ GIUD В Базовую сущность, чтобы извлекать событие 
                    List<BaseDateRangeModel> driverVacations = new();
                    foreach (var dv in driver.DriverVacations)
                    {
                        var mapDV = _mapper.Map<BaseDateRangeModel>(dv.DateRange);
                        dv.Guid = mapDV.Id;
                        driverVacations.Add(mapDV);
                    }

                    //создаем контракт
                    var driverVacationsContract = new GetDateRangeContract()
                    {
                        UserId = contract.UserId,
                        UserName = contract.UserName,
                        RequestId = contract.RequestId,
                        DateRanges = driverVacations
                    };
                    // скармливаем слайсеру
                    var slicedDriverVacations = await _scheduleServiceApí.GetSlisedDateRangeAsync(driverVacationsContract);

                    //проверяем на успешность и конвертируем
                    if (!slicedDriverVacations.Success)
                    {
                        return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(slicedDriverVacations.StatusCode,
                            slicedDriverVacations.Errors.ToList());
                    }
                    var slicedDriverVacationsRes = slicedDriverVacations.Data.ToList();

                    //на основе расписаний заполняем словарь
                    foreach (var shedules in slicedDriverShedulesRes)
                    {                                 
                        //проверяем, не захватили мы данные, которые не входят в диапазон алгоритма
                        DateTime startDate  = shedules.StartDate < contract.DateFrom ? contract.DateFrom : shedules.StartDate;
                        DateTime endDate = shedules.EndDate > contract.DateTo ? contract.DateTo : shedules.EndDate;

                        //приводим к короткому формату (обрезаем минуты и т.д.)
                        startDate = DateRangeHelper.Normalize(startDate);
                        endDate = DateRangeHelper.Normalize(endDate);

                        //получаем оригинал расписания водителя и конвертируем
                        var driverSheduleType = driver.Driver2Transports.Where(x => x.Guid == shedules.Id).FirstOrDefault();

                        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                        {
                            //получаем по дате словарь с водителями и их занятостью
                            var dictDrivers = responseDatesDrivers[date];
                            //получаем график транспорта на эту дату
                            var transportWorkType = responseDatesTransport[date];
                            //получаем оригинальный график транспорта на эту дату(без ремонта)
                            var origTransportWorkType = origDatesTransport[date];
                            //конвертируем с учетом графика транспорта на этот день
                            //в конвертер необходимо передать данные о красных днях и выходных
                            // и дежурных сменах для графика 5/2
                            //а также учесть оригинальное расписание, если идет ремонт весь день
                            if (driverSheduleType != null)
                            {
                                if (dictDrivers.ContainsKey(driver.DriverId)) {
                                    dictDrivers[driver.DriverId] =
                                        transportWorkType.ToDriverWorkType(date, driverSheduleType.DateRange.StartDate,
                                            redDatesRes, weekendWorkDatesRes, transportDutyShifts, driverSheduleType.WorkShift, origTransportWorkType);
                                }
                                else
                                {
                                    dictDrivers.Add(driver.DriverId,
                                        transportWorkType.ToDriverWorkType(date, driverSheduleType.DateRange.StartDate,
                                            redDatesRes, weekendWorkDatesRes, transportDutyShifts, driverSheduleType.WorkShift, origTransportWorkType));
                                }
                            }
                            //если не нашлось на данное число расписания
                            else
                            {
                                dictDrivers.Add(driver.DriverId, DriverWorkDateType.Unknown);
                            }
                        }
                    }

                    //на основе обработанных отпусков водителя
                    //перезатираем данные расписаний водителя
                    foreach (var vacation in slicedDriverVacationsRes)
                    {
                        //проверяем, не захватили мы данные, которые не входят в диапазон алгоритма
                        DateTime startDate = vacation.StartDate < contract.DateFrom ? contract.DateFrom : vacation.StartDate;
                        DateTime endDate = vacation.EndDate > contract.DateTo ? contract.DateTo : vacation.EndDate;

                        //приводим к короткому формату (обрезаем минуты и т.д.)
                        startDate = DateRangeHelper.Normalize(startDate);
                        endDate = DateRangeHelper.Normalize(endDate);

                        //получаем оригинал отпуска водителя и конвертируем
                        var driverVacationType = driver.DriverVacations.Where(x => x.Guid == vacation.Id).FirstOrDefault();
                        

                        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                        {
                            //получаем по дате словарь с водителями и их занятостью
                            var dictDrivers = responseDatesDrivers[date];
                            //получаем график транспорта на эту дату
                            var transportWorkType = responseDatesTransport[date];
                            //перезатираем данные, если есть отпуск/болезнь водителя
                            //и есть расписание транспорта на этот день
                            //или транспорт чинится
                            if (driverVacationType != null 
                                && !(transportWorkType == TransportWorkDateType.NotDrivers
                                || transportWorkType == TransportWorkDateType.Unknown
                                || transportWorkType == TransportWorkDateType.NotShedule))
                            {
                                var type = driverVacationType.VacationType.ToDriverVacationType();
                                //если конверт прошел нормально - перезатираем
                                if(type != DriverWorkDateType.Unknown)
                                {
                                    //перед затиранием, если есть сменный водитель,
                                    //устанавливаем его на этот день по этому графику
                                    if(driverVacationType.ReplacementDriverId.HasValue && 
                                        dictDrivers.ContainsKey(driver.DriverId))
                                    {
                                        if(dictDrivers.ContainsKey(driverVacationType.ReplacementDriverId.Value))
                                        {
                                            dictDrivers[driverVacationType.ReplacementDriverId.Value] =
                                                dictDrivers[driver.DriverId];
                                        }
                                        else
                                        {
                                            dictDrivers.Add(driverVacationType.ReplacementDriverId.Value,
                                                dictDrivers[driver.DriverId]);
                                        }
                                        
                                    }
                                    dictDrivers[driver.DriverId] = type;
                                }
                            }
                        }
                    }
                }
                //когда мы заполнили все словари следует пройтись по транспорту и проставить неопределенные моменты
                foreach (var (key, value) in responseDatesTransport)
                {
                    var dictDrivers = responseDatesDrivers[key];
                    responseDatesTransport[key] = value.CompareAndChangeTransportSheduleType(key,
                        dictDrivers, redDatesRes, weekendWorkDatesRes, transportDutyShifts);
                }
                //когда мы заполнили все словари следует заполнить словарь ошибок
                foreach (var (key, value) in responseErrors)
                {
                    var dictDrivers = responseDatesDrivers[key];
                    var transportState = responseDatesTransport[key];
                    responseErrors[key] = transportState.FillErrors(dictDrivers);
                }
                var response = new DictinaryDateRangeViewModel()
                {
                      OrganizationId = contract.OrganizationId,
                      DatesTransport = responseDatesTransport,
                      DatesDrivers = responseDatesDrivers,
                      ResponseErrors = responseErrors
                };

                return CommonHttpHelper.BuildSuccessResponse(response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<DictinaryDateRangeViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetTransportSheduleDateRangeAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<TransportSheduleViewModel>> CreateTransportSheduleAsync(CreateTransportSheduleContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<TransportSheduleViewModel>(initialError: "Пустой запрос");

            try
            {
                
                var transport = await _transportStorage.GetTransportsAsync(new GetTransportContract()
                { TransportIds = new List<int> { contract.TransportId } }
                );
                if(transport == null || transport.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<TransportSheduleViewModel>(initialError: "Нет такого транспорта");
                } 
                
                //нормализуем диапазон
                contract.DateRange.Normalize();

                //проверка на то, что дата начала раньше даты окончания
                if (contract.DateRange.EndDate.Value.Date < contract.DateRange.StartDate.Date)
                {
                    return CommonHttpHelper.BuildErrorResponse<TransportSheduleViewModel>(initialError:
                        "Дата окончания должна быть не меньше, чем дата начала");
                }
                var result = await _transportSheduleStorage.CreateTransportSheduleAsync(contract);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<TransportSheduleViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(CreateTransportSheduleAsync)} ReqId : {contract.RequestId}");
            }
        }

        public async Task<CommonHttpResponse<TransportSheduleViewModel>> UpdateTransportShedulesAsync(UpdateTransportSheduleContract contract)
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<TransportSheduleViewModel>(initialError: "Пустой запрос");

            try
            {
                var transport = await _transportStorage.GetTransportsAsync(new GetTransportContract()
                { TransportIds = new List<int> { contract.TransportId } }
                );
                if (transport == null || transport.Count == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<TransportSheduleViewModel>(initialError: "Нет такого транспорта");
                }
                
                //нормализуем диапазон
                contract.DateRange.Normalize();

                //проверка на то, что дата начала раньше даты окончания
                if (contract.DateRange.EndDate.Value.Date < contract.DateRange.StartDate.Date)
                {
                    return CommonHttpHelper.BuildErrorResponse<TransportSheduleViewModel>(initialError:
                        "Дата окончания должна быть не меньше, чем дата начала");
                }
                var result = await _transportSheduleStorage.UpdateTransportSheduleAsync(contract);
                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<TransportSheduleViewModel>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(UpdateTransportShedulesAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<TransportSheduleViewModel>>> GetTransportShedulesAsync(
            GetTransportSheduleContract contract
        )
        {
            if (contract?.TransportSheduleIds == null)
                return CommonHttpHelper.BuildErrorResponse<IList<TransportSheduleViewModel>>(initialError: "Пустой запрос");

            try
            {
                var transportShedule = await _transportSheduleStorage.GetTransportsSheduleAsync(contract);
                if (transportShedule == null)
                    return CommonHttpHelper.BuildErrorResponse<IList<TransportSheduleViewModel>>(HttpStatusCode.NotFound, null, "Запись не найдена");

                return CommonHttpHelper.BuildSuccessResponse(transportShedule, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportSheduleViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetTransportShedulesAsync)}");
            }
        }

        public async Task<CommonHttpResponse<IList<TransportSheduleViewModel>>> GetFilteredTransportShedulesAsync(
            TransportSheduleFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<TransportSheduleViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = filter.IsFilter ? await _transportSheduleStorage.GetFilteredTransportsSheduleAsync(filter) :
                    await _transportSheduleStorage.GetFilteredTransportsSheduleAsync(filter);

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<TransportSheduleViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredTransportShedulesAsync)}");
            }
        }

        public async Task<CommonHttpResponse> DeleteTransportShedulesAsync(DeleteTransportSheduleContract contract)
        {
            if (contract?.TransportSheduleIds == null)
                return CommonHttpHelper.BuildErrorResponse(initialError: "Пустой запрос");

            try
            {
                if (await _transportSheduleStorage.DeleteTransportsSheduleAsync(contract))
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
                    $"Ошибка выполнения метода {nameof(DeleteTransportShedulesAsync)}");
            }
        }
    }
}
