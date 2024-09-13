using System;
using System.Threading.Tasks;

namespace MissionService.Core.BusinessLogic.Interface
{
    public interface IHangfireLogic
    {
        Task ExecAppDailyActionsAsync(
            DateTime failMissionDate,
            DateTime appGenerationDate
        );
    }
}
