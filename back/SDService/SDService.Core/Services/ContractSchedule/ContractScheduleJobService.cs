using AutoMapper;
using Microsoft.Extensions.Logging;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using ScheduleService.Contracts.Interfaces;
using SDContracts.DataContracts.Contract.ScheduleModels.Mission;
using SDContracts.Filter;
using SDContracts.ViewModels.ContractSchedule;
using SDContracts.ViewModels.ContractSchedule.Mission;
using SDService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDService.Core.Services.ContractSchedule
{
    public class ContractScheduleJobService : IContractScheduleJobService
    {
        private readonly IScheduleServiceApi _scheduleServiceApi;
        private readonly IContractStorage _contractStorage;
        private readonly IContractScheduleMissionStorage _contractScheduleMissionStorage;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ContractScheduleJobService(
            IScheduleServiceApi scheduleServiceApi,
            IContractStorage contractStorage,
            IContractScheduleMissionStorage contractScheduleMissionStorage,
            IMapper mapper,
            ILoggerFactory factory
        )
        {
            _scheduleServiceApi = scheduleServiceApi;
            _contractStorage = contractStorage;
            _contractScheduleMissionStorage = contractScheduleMissionStorage;
            _mapper = mapper;
            _logger = factory.CreateLogger<ContractScheduleJobService>();
        }

        public Task<IDictionary<DateTime, IList<ContractScheduleMissionViewModel>>> ShowContractSchedulesGroupedByDatesAsync(ShowMissionsContract contract)
        {
            return GenerateNewContractScheduleMissionByDateAsync(
                dateFrom: contract.DateFrom,
                dateTo: contract.DateTo,
                contractIds: contract.ContractIds,
                includeNotCompleted: contract.IncludeNotCompleted,
                needDeletePrevCompleted: false,
                needSaveToDb: false,
                isFilterContractsByUserId: contract.IsFilterByUserId,
                requestId: contract.RequestId,
                createdUserId: contract.UserId,
                createdUserName: contract.UserName
            );
        }

        public Task GenerateMissionsDailyJobAsync()
        {
            return GenerateNewContractScheduleMissionByDateAsync(
                dateFrom: DateTime.UtcNow,
                includeNotCompleted: true,
                needDeletePrevCompleted: true,
                needSaveToDb: true
            );
        }

        //!_! Повесить на фронте задержку для этого метода без фильтра
        private async Task<IDictionary<DateTime, IList<ContractScheduleMissionViewModel>>> GenerateNewContractScheduleMissionByDateAsync(
            DateTime dateFrom,
            DateTime? dateTo = null,
            IList<int> contractIds = default,
            bool includeNotCompleted = false,
            bool needDeletePrevCompleted = false,
            bool needSaveToDb = false,
            bool isFilterContractsByUserId = false,
            Guid? requestId = null,
            string createdUserName = "system",
            int createdUserId = 0
        )
        {
            //!_! Если дата ДО не указана, то считаем что делаем на 1 день
            if (!dateTo.HasValue)
            {
                dateTo = dateFrom.AddDays(1);
            }

            //!_! Сбрасываем время
            dateFrom = dateFrom.Date;
            dateTo = dateTo.Value.Date;
            DateTime tmpDate = dateFrom;

            //!_! Получаем все договоры по фильтру договоров
            var contracts = await _contractStorage.GetContractsWithGroupedContainersAsync(
                contractIds,
                isFilterContractsByUserId ? createdUserId : null);

            //Собираем словарь для вывода
            var resultDictionary = new Dictionary<DateTime, IList<ContractScheduleMissionViewModel>>();
            var dbDictionaryToAdd = new Dictionary<DateTime, IList<CreateMissionContract>>();

            //!_! Делаем цикл для каждой даты
            while (tmpDate < dateTo.Value)
            {
                var resultListContracts = new List<ContractScheduleMissionViewModel>();
                var dbContractMission = new List<CreateMissionContract>();
                foreach (var contract in contracts
                    .Where(x => x.StartDate.Date <= tmpDate)
                    //Выбираем только те которые либо с автопродлением либо без даты окончания либо дата окончания больше даты на которую ищем
                    .Where(x => x.IsAutorenewSchedules || !x.EndDate.HasValue || x.EndDate.Value >= tmpDate)
                )
                {
                    var contractMissionInfo = _mapper.Map<ContractScheduleMissionViewModel>(contract);
                    var resultListStands = new List<ContractStandMissionInfoViewModel>();
                    foreach (var stand in contract.AttachedStands)
                    {
                        //Чтобы не плодить новые задачи в случае если график выпадает на дату,
                        //но при этом есть такие же невывезенные на эту же дату, то ищем также провальные - и подсовываем их
                        var existsMissions = includeNotCompleted ?
                            await _contractScheduleMissionStorage.GetFilteredContractScheduleMission(new ContractScheduleMissionFilterModel
                            {
                                ContractIds = new List<int> { contract.ContractId },
                                StandIds = new List<int> { stand.StandId },
                                MissionDateFrom = tmpDate,
                                MissionDateTo = tmpDate, //на одну дату ищем
                                IsCompleted = false
                            }) :
                            new List<ContractScheduleMissionViewModel>();

                        //тк не может на одну дату быть несколько задач с одинаковым договором с одинаковым контрактом и с одинаковой датой
                        if (existsMissions != default && existsMissions.Count > 1)
                        {
                            var errorMessage = $"Найдены дубли задач или задачи образовванные некорректно ID задач: {string.Join(',', existsMissions.Select(x => x.MissionId))}";
                            _logger.LogError(errorMessage);
                            throw new Exception(errorMessage);
                        }

                        var resultMissions = existsMissions.SingleOrDefault();

                        //не проверяем те контейнеры по которым уже есть задачи
                        //SelectMany тут отработает нормально тк один контейнер в двух площадках быть не может
                        var resultListContainers = new List<ContractContainerInfoMissionViewModel>();
                        var dbContainerMissionContracts = new List<CreateContainerMissionModel>();
                        foreach (var container in stand.AttachedContainers.Where(x =>
                            resultMissions == default ||
                            !existsMissions.SingleOrDefault().Stand.AttachedContainers
                                .Select(x => x.ContainerId)
                                .Contains(x.ContainerId)))
                        {
                            //Получаем срочные вывозы по контейнеру и по дате
                            //...Реализация...
                            //

                            var result = await _scheduleServiceApi.IsContractScheduleFireOnDateAsync(
                                CreateScheduleRequest(
                                    requestId,
                                    createdUserName,
                                    createdUserId,
                                    tmpDate,
                                    container.Container2Schedule.ContractScheduleCollection
                                    //urgentDates :
                                    //urgentCancelDates:
                                )
                            );

                            if (!result.Success)
                            {
                                _logger.LogError($"Ошибка запроса информации по дате - {string.Join(';', result.Errors)}");
                                continue;
                            }

                            if (result.Data)
                            {
                                resultListContainers.Add(_mapper.Map<ContractContainerInfoMissionViewModel>(container));
                                dbContainerMissionContracts.Add(new CreateContainerMissionModel { ContainerId = container.ContainerId });
                            }
                        }

                        if (resultListContainers.Count > 0)
                        {
                            var standMissionInfo = _mapper.Map<ContractStandMissionInfoViewModel>(stand);
                            standMissionInfo.AttachedContainers = resultListContainers;
                            contractMissionInfo.Stand = standMissionInfo;
                            resultListContracts.Add(contractMissionInfo);

                            var dbMission = new CreateMissionContract
                            {
                                ContractId = contract.ContractId,
                                StandId = stand.StandId,
                                MissionDate = tmpDate,
                                AttachedContainers = dbContainerMissionContracts,
                                UserId = createdUserId,
                                UserName = createdUserName
                            };
                            dbContractMission.Add(dbMission);
                        }
                    }
                }

                if (resultListContracts.Count > 0)
                {
                    resultDictionary.Add(tmpDate, resultListContracts);
                    dbDictionaryToAdd.Add(tmpDate, dbContractMission);
                }
                tmpDate = tmpDate.AddDays(1);
            }

            if (!needSaveToDb)
                return resultDictionary;

            foreach (var createContract in dbDictionaryToAdd)
            {
                var addResult = await _contractScheduleMissionStorage.AddMissionsWithParamsAsync(
                    createContract.Value,
                    createContract.Key,
                    createContract.Key,
                    needDeletePrevCompleted,
                    true
                );

                if (!addResult)
                {
                    throw new Exception("Ошибка генерации задач");
                }
            }

            return resultDictionary;
        }

        private GetFireInfoOnDateContractScheduleCollectionContract CreateScheduleRequest(
            Guid? requestId,
            string createdUserName,
            int createdUserId,
            DateTime date,
            IList<ContractScheduleCollectionViewModel> contractScheduleCollection,
            IList<DateTime> urgentDates = default,
            IList<DateTime> urgentCancelDates = default
        )
        {
            return new GetFireInfoOnDateContractScheduleCollectionContract
            {
                RequestId = requestId ?? Guid.NewGuid(),
                UserId = createdUserId,
                UserName = createdUserName,
                NotSlicedContractScheduleCollection = contractScheduleCollection.Select(_mapper.Map<SSContractScheduleCollectionModel>).ToList(),
                Date = date,
                UrgentDates = urgentDates,
                UrgentCancelDates = urgentCancelDates
            };
        }
    }
}
