using SDContracts.Enums;

namespace SDService.Database.Models.ViewModels.Container
{
    public class InfoForMissionModel
    {
        public int ContainerId { get; set; }

        public int? ContractClientId { get; set; }

        public string ContractClientName { get; set; }

        public InfoForMissionStandModel StandInfo { get; set; }

        public string ContainerName { get; set; }

        public TrashType TrashType { get; set; }

        public int ContainerTypeId { get; set; }

        public string ContainerTypeName { get; set; }

        public decimal Volume { get; set; }
    }
}
