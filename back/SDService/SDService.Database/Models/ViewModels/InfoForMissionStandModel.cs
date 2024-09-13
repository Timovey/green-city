using SDContracts.Enums;
using SDContracts.SpecificModels.ContainerStand;

namespace SDService.Database.Models.ViewModels
{
    public class InfoForMissionStandModel
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
