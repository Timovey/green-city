using MissionService.Contracts.DataContracts.App;
using MissionService.Database.Models.DbModels;
using System.Collections.Generic;
using System.Net;

namespace MissionService.Core.Services.Interface
{
    public interface IAppActionsValidationService
    {
        (bool IsSuccess, string Error, HttpStatusCode RecomendedErrorCode) ValidateUpdateApp(
            UpdateAppByContractClient contract,
            IList<(App App, IList<Mission> AttachedMissions)> existApps
        );

        (bool IsSuccess, string Error, HttpStatusCode RecomendedErrorCode) ValidateDeleteApp(
            DeleteAppsContract contract,
            IList<(App App, IList<Mission> AttachedMissions)> existApps
        );

        (bool IsSuccess, string Error, HttpStatusCode RecomendedErrorCode) ValidateRegenerationApps(
            RunAppGenerationInSingleThreadContract contract
        );
    }
}
