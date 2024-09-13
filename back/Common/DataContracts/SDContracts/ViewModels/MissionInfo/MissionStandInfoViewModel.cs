using SDContracts.Enums;
using SDContracts.SpecificModels.ContainerStand;

namespace SDContracts.ViewModels.MissionInfo
{
    /// <summary> Информация по КП от АО для формирования миссии </summary>
    public class MissionStandInfoViewModel
    {
        public int StandId { get; set; }

        /// <summary> Координаты </summary>
        public StandCoordinatesModel Coordinates { get; set; }

        public string Address { get; set; }

        public StandType StandType { get; set; }

        public string Contacts { get; set; }

        public TrashType TrashType { get; set; }
    }
}
