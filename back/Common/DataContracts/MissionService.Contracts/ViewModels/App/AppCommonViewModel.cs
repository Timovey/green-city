using MissionService.Contracts.Enums;

namespace MissionService.Contracts.ViewModels.App
{
    public class AppCommonViewModel
    {
        public int Id { get; set; }

        public string ContractClientName { get; set; }

        public string ContractNumber { get; set; }

        public AppComputedStatus AppStatus { get; set; }

        public string Comment { get; set; }
    }
}
