using MissionService.Contracts.Enums;

namespace MissionService.Contracts.ViewModels.Mission
{
    public class CommonMissionViewModel
    {
        public int Id { get; set; }

        /// <summary> Комментарий к вывозу </summary>
        public string MissionComment { get; set; }

        public MissionStatus Status { get; set; }

        public string StatusComment { get; set; }

        public string ContractClients { get; set; }

        public int StandId { get; set; }

        public string Address { get; set; }

        public int ContainerId { get; set; }

        public string ContainerName { get; set; }

        public MissionTrashType TrashType { get; set; }

        public int ContainerTypeId { get; set; }

        public string ContainerTypeName { get; set; }

        public decimal PlanVolume { get; set; }

        public decimal? FactVolume { get; set; }
    }
}
