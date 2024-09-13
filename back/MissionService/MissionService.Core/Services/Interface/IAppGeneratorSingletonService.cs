using MissionService.Contracts.DataContracts.App;

namespace MissionService.Core.Services.Interface
{
    public interface IAppGeneratorSingletonService
    {
        (bool IsSuccess, string[] Errors) RunContractScheduleAppGenerationInSingleThread(
            RunAppGenerationInSingleThreadContract contract
        );
    }
}
