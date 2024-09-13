using MissionService.Contracts.DataContracts.App;
using MissionService.Contracts.Enums;
using MissionService.Core.Extensions;
using MissionService.Core.Services.Interface;
using MissionService.Database.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MissionService.Core.Services
{
    public class AppActionsValidationService : IAppActionsValidationService
    {
        public (bool IsSuccess, string Error, HttpStatusCode RecomendedErrorCode) ValidateUpdateApp(
            UpdateAppByContractClient contract,
            IList<(App App, IList<Mission> AttachedMissions)> existApps
        )
        {
            if (!contract.UpdatePermission.CanCreateOrChangeMissionByTime(contract.Date))
            {
                return (false, $"Слишком поздно для изменения заявки на {contract.Date.Date}", HttpStatusCode.BadRequest);
            }

            if (existApps == default || existApps.Count == 0)
            {
                return (false, "Данные по такой заявке не найдены", HttpStatusCode.NotFound);
            }

            if (existApps.Count > 1)
            {
                return (false, "Данные по заявке задублированы!", HttpStatusCode.InternalServerError);
            }

            var existApp = existApps.Single();
            if (existApp.App == default ||
                existApp.AttachedMissions == default ||
                existApp.AttachedMissions.Count == 0 ||
                existApp.AttachedMissions.Where(x => x.Date.Date != existApp.App.Date.Date).FirstOrDefault() != default)
            {
                return (false, "Ошибка при получении данных по заявке, некорректные данные", HttpStatusCode.BadRequest);
            }

            if (contract.Date.Date < DateTime.UtcNow.Date)
            {
                return (false, "Заявку нельзя перенести в прошлое", HttpStatusCode.BadRequest);
            }

            //!_! Пользователю обладающему абсолютной ролью можно редактировать чужие заявки
            if (contract.UpdatePermission == AppActionsPermission.Absolute)
            {
                return (true, default, default);
            }

            if (existApp.App.CreatedUserId != contract.UserId)
            {
                return (false, "Нельзя редактировать чужие заявки", HttpStatusCode.BadRequest);
            }

            return (true, default, default);
        }

        public (bool IsSuccess, string Error, HttpStatusCode RecomendedErrorCode) ValidateDeleteApp(
            DeleteAppsContract contract,
            IList<(App App, IList<Mission> AttachedMissions)> existApps
        )
        {
            if (existApps == default || existApps.Count == 0)
            {
                return (false, "Заявки не найдены", HttpStatusCode.NotFound);
            }

            if (contract.DeletePermission == AppActionsPermission.Absolute)
            {
                return (true, default, default);
            }

            var notAllowedToDeleteApp = existApps.Where(x => x.App.CreatedUserId != contract.UserId);

            if (notAllowedToDeleteApp.Any())
            {
                return (false, $"Нельзя удалять чужие заявки - {string.Join(',', notAllowedToDeleteApp)}", HttpStatusCode.BadRequest);
            }

            return (true, default, default);
        }

        public (bool IsSuccess, string Error, HttpStatusCode RecomendedErrorCode) ValidateRegenerationApps(
            RunAppGenerationInSingleThreadContract contract
        )
        {
            if (contract == default)
            {
                return (false, "Пустой запрос", HttpStatusCode.InternalServerError);
            }

            if (contract.Date.Date < DateTime.UtcNow.Date)
            {
                return (false, "Генерация в прошлое запрещена", HttpStatusCode.BadRequest);
            }

            return (true, default, default);
        }
    }
}
