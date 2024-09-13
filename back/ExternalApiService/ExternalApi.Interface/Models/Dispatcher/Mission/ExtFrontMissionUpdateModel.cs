using ExternalApi.Interface.Models.Mission.App;
using ExternalApi.Interface.Models.Sd.Container;

namespace ExternalApi.Interface.Models.Dispatcher.Mission
{
    public class ExtFrontMissionUpdateModel
    {
        public int Id { get; set; }

        /// <summary> Комментарий к вывозу </summary>
        public string MissionComment { get; set; }

        public ExtMissionStatus Status { get; set; }

        public string StatusComment { get; set; }

        public string ContractClients { get; set; }

        public int StandId { get; set; }

        public string Address { get; set; }

        public int ContainerId { get; set; }

        public string ContainerName { get; set; }

        public ExtTrashType TrashType { get; set; }

        public int ContainerTypeId { get; set; }

        public string ContainerTypeName { get; set; }

        public decimal PlanVolume { get; set; }

        public decimal FactVolume { get; set; }
    }
}
