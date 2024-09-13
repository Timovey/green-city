using MissionService.Contracts.Enums;

namespace MissionService.Contracts.ViewModels.Mission
{
    public class MissionForRouteViewModel
    {
        public int Id { get; set; }

        public string ContractClients { get; set; }

        public int StandId { get; set; }

        public string Address { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public string CoordinatesComment { get; set; }

        public MissionStandType StandType { get; set; }

        public string Contacts { get; set; }

        public int ContainerId { get; set; }

        public string ContainerName { get; set; }

        public MissionTrashType TrashType { get; set; }

        public int ContainerTypeId { get; set; }

        public decimal PlanVolume { get; set; }
    }
}
