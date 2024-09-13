using AuthContracts.Constants;
using ExternalApi.Interface.Models.Sd.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ExternalApiService.Extensions
{
    internal static class SdExtensions
    {
        //!_! График на завтра можно создавать только до 14 00 сегодняшнего дня
        private const int _createMissionValidationHours = 10;
        private const int _ulTimeOffset = 4;

        internal static (bool IsSuccess, string Error) ValidateContractSchedule(
            this IList<ExtCreateContainer2ScheduleRequest> container2Schedules,
            ClaimsPrincipal user
        )
        {
            //!_! Если у пользователя есть роль админ или особая роль работы с заявками и графиками - то можно когда угодно создавать графики
            if (user.IsInOneOfRoles(AuthRolesConstants.ADMIN, AuthRolesConstants.ABSOLUTE_APP_ACTION_ROLE))
                return (true, default);

            if (container2Schedules == default || container2Schedules.Count == 0)
                return (true, default);

            //Момент времени когда был отправлен запрос на создание или  обновление по Ульяновскому времени
            var validationDate = DateTime.UtcNow.AddHours(_ulTimeOffset);

            var toValidateDate = container2Schedules
                .Where(x => x.ContractScheduleCollection != default && x.ContractScheduleCollection.Count > 0)
                .SelectMany(x => x.ContractScheduleCollection)
                .Where(x => x.ScheduleDateFrom.HasValue)
                .Where(x => (x.ScheduleDateFrom.Value.Date - validationDate).Hours < _createMissionValidationHours)
                .Select(x => x.ScheduleDateFrom)
                .FirstOrDefault();

            if (toValidateDate != default)
                return (false, "Графики на завтра или ранее можно создавать только до 14:00");

            return (true, default);
        }
    }
}
