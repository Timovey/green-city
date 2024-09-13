using MissionService.Contracts.Enums;

namespace MissionService.Contracts.ViewModels.App
{
    public class ContainerInAppInfo
    {
        public int ContainerId { get; set; }

        public string ContainerName { get; set; }

        public MissionStatus MissionStatus { get; set; }
    }
}
