using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using Microsoft.Extensions.DependencyInjection;
using SDContracts.DataContracts.Contract.ScheduleModels.Mission;
using SDContracts.ViewModels.ContractSchedule.Mission;
using SDService.Core.Services.ContractSchedule;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SDService.Core.BusinessLogic
{
    public class MissionSingletonLogic
    {
        private readonly object _locker = new object();

        private readonly IServiceProvider _serviceProvider;

        public MissionSingletonLogic(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<CommonHttpResponse<IDictionary<DateTime, IList<ContractScheduleMissionViewModel>>>> ShowContractSchedulesGroupedByDatesAsync(
            ShowMissionsContract contract
        )
        {
            if (contract == null)
                return CommonHttpHelper.BuildErrorResponse<IDictionary<DateTime, IList<ContractScheduleMissionViewModel>>>(initialError: "Пустой запрос");

            try
            {
                //Локер тк потенцианльно очень затратный алгоритм
                lock (_locker)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var missionJobsService = scope.ServiceProvider.GetRequiredService<IContractScheduleJobService>();

                        var result = missionJobsService.ShowContractSchedulesGroupedByDatesAsync(contract).GetAwaiter().GetResult();

                        return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IDictionary<DateTime, IList<ContractScheduleMissionViewModel>>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(ShowContractSchedulesGroupedByDatesAsync)}");
            }
        }
    }
}
