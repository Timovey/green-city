using ExternalApi.Interface.Models.Mission.App;
using SDContracts.Enums;
using System.Collections.Generic;

namespace ExternalApiService.Extensions
{
    internal static class AppExtensions
    {
        public static IList<MissionInfoEntityType> GetAvailableEntityTypes(this ExtAppType appType)
        {
            if (appType == ExtAppType.Unknown ||
                appType == ExtAppType.ByContractClient
            )
                return new List<MissionInfoEntityType> { MissionInfoEntityType.Real };

            return new List<MissionInfoEntityType> { MissionInfoEntityType.Real, MissionInfoEntityType.FakeForMissionInfo };
        }
    }
}
