using MissionService.Contracts.Enums;
using System;

namespace MissionService.Core.Extensions
{
    public static class AppPermissionsExtensions
    {
        //!_! Миссию на завтра создать можно только до 14 00 сегодняшнего дня
        private const int _createMissionValidationHours = 10;
        private const int _ulTimeOffset = 4;

        public static bool CanCreateOrChangeMissionByTime(
            this AppActionsPermission createPermission,
            DateTime date,
            DateTime? validationDate = default
        )
        {
            if (validationDate == default)
                validationDate = DateTime.UtcNow.AddHours(_ulTimeOffset);

            if (createPermission == AppActionsPermission.Unknown)
                return false;

            if (createPermission == AppActionsPermission.Absolute &&
                date.Date < validationDate.Value.Date)
            {
                return false;
            }

            if (createPermission != AppActionsPermission.Absolute &&
                (date.Date - validationDate.Value).Hours < _createMissionValidationHours)
            {
                return false;
            }

            return true;
        }
    }
}
