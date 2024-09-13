using System;
using System.Threading.Tasks;
using MissionService.Contracts.DataContracts.App;
using MissionService.Core.BusinessLogic.Interface;
using MissionService.Core.Services.Interface;

namespace MissionService.Core.BusinessLogic;

public class HangfireLogic : IHangfireLogic
{
    private readonly IMissionLogic _missionLogic;
    private readonly IAppGeneratorSingletonService _appGeneratorSingletonService;

    public HangfireLogic(IMissionLogic missionLogic, IAppGeneratorSingletonService appGeneratorSingletonService)
    {
        _missionLogic = missionLogic;
        _appGeneratorSingletonService = appGeneratorSingletonService;
    }

    public async Task ExecAppDailyActionsAsync(
        DateTime failMissionDate,
        DateTime appGenerationDate
    )
    {
        //!_! Проваливаем все незавершенные миссии
        await _missionLogic.FailMissionsAsync(failMissionDate);

        //!_! Генерируем новые заявки
        _appGeneratorSingletonService.RunContractScheduleAppGenerationInSingleThread(new RunAppGenerationInSingleThreadContract
        {
            Date = appGenerationDate.Date,
            ContractClients = default,
            ContractIds = default,
            UserId = 0,
            UserName = "Mission hangfire job"
        });
    }
}