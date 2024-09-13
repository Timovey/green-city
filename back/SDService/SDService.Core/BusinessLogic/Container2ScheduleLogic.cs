using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using SDContracts.Filter;
using SDContracts.ViewModels;
using SDService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SDService.Core.BusinessLogic
{
    public class Container2ScheduleLogic
    {
        private readonly IContainer2ScheduleStorage _container2ScheduleStorage;

        public Container2ScheduleLogic(
            IContainer2ScheduleStorage container2ScheduleStorage
        )
        {
            _container2ScheduleStorage = container2ScheduleStorage;
        }

        public async Task<CommonHttpResponse<IList<Container2ScheduleForMissionsViewModel>>> GetFilteredContainer2SchedulesForMissionsAsync(
            Container2ScheduleFilterModel filter
        )
        {
            if (filter == null)
                return CommonHttpHelper.BuildErrorResponse<IList<Container2ScheduleForMissionsViewModel>>(initialError: "Пустой запрос");

            try
            {
                var result = await _container2ScheduleStorage.GetFilteredContainer2ScheduleForMissionsAsync(
                    filter.Date,
                    filter.ContractClientIds,
                    filter.ContractIds,
                    filter.EntityType
                );

                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<IList<Container2ScheduleForMissionsViewModel>>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GetFilteredContainer2SchedulesForMissionsAsync)} ReqId : {filter.RequestId}");
            }
        }
    }
}
