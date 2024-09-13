using GreenCity.Http;
using GreenCity.Http.Helpers;
using ScheduleService.Contracts.DataContracts.ContractSchedule;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using ScheduleService.Core.Services.ContractSchedule;
using ScheduleService.Core.Services.DateRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Core.BusinessLogic
{
    public class CronScheduleLogic
    {
        private readonly IContractScheduleService _contractScheduleService;
        private readonly IDateRangeSlicer _slicer;

        public CronScheduleLogic(
            IContractScheduleService contractScheduleService,
            IDateRangeSlicer slicer
        )
        {
            _contractScheduleService = contractScheduleService;
            _slicer = slicer;
        }

        /// <summary>
        /// Метод для тестирования графиков
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public async Task<CommonHttpResponse<IList<DateTime>>> GetNextFireContractScheduleDatesAsync(
            GetContractScheduleContract contract,
            IList<DateTime> urgentDates = default,
            IList<DateTime> urgentCancelDates = default
        )
        {
            if (contract?.Schedule?.ContractScheduleSettings == default)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<DateTime>>(initialError: "Пустой запрос, либо не хватает расписания или настроек");
            }

            var result = new HashSet<DateTime>();

            var currentTime = contract.FromDate.Date;
            while ((currentTime.Date.Year < 2070 && result.Count < contract.ExamplesCount))
            {
                if (contract.ToDate.HasValue && currentTime.Date > contract.ToDate.Value.Date)
                    break;

                var nextFire = await _contractScheduleService.GetNextFireContractScheduleDateOrEmptyAsync(contract.Schedule, currentTime, urgentDates, urgentCancelDates);
                if (nextFire == default)
                {
                    currentTime = currentTime.Date.AddDays(1);
                    continue;
                }

                if (nextFire.Errors != default && nextFire.Errors.Count > 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<IList<DateTime>>(
                        initialError: $"Ошибка выполнения метода поиска ближайшей даты работы графика",
                        extErrors: nextFire.Errors.ToList());
                }

                result.Add(nextFire.Date.Date);
                currentTime = currentTime.Date == nextFire.Date.Date ? currentTime.Date.AddDays(1) : nextFire.Date.Date;
            }

            var resultList = result.ToList();
            if (contract.ToDate.HasValue)
                resultList = resultList.Where(x => x.Date <= contract.ToDate.Value.Date).ToList();

            return CommonHttpHelper.BuildSuccessResponse<IList<DateTime>>(resultList, System.Net.HttpStatusCode.OK);
        }

        public async Task<CommonHttpResponse<bool>> IsContractScheduleCollectionFireOnDateAsync(
            GetFireInfoOnDateContractScheduleCollectionContract contract
        )
        {
            if (contract == default)
            {
                return CommonHttpHelper.BuildErrorResponse<bool>(initialError: "Пустой запрос, либо не хватает расписания");
            }

            //Нарезаем на слоты пришедшее расписание
            var slicedSlots = _slicer.GetSortedTimeSlots(contract.NotSlicedContractScheduleCollection);

            var result = await _contractScheduleService.IsContractScheduleCollectionFireOnDateAsync(slicedSlots, contract.Date, contract.UrgentDates, contract.UrgentCancelDates);
            if (result.Errors != default && result.Errors.Count > 0)
            {
                return CommonHttpHelper.BuildErrorResponse<bool>(System.Net.HttpStatusCode.InternalServerError, result.Errors.ToList());
            }

            return CommonHttpHelper.BuildSuccessResponse(result.IsFire, System.Net.HttpStatusCode.OK);
        }
    }
}
