using SDContracts.Enums;
using SDContracts.ViewModels.MissionInfo;
using System.Collections.Generic;

namespace SDContracts.ViewModels
{
    /// <summary> Информация от АО для формирования миссии </summary>
    public class MissionInfoViewModel
    {
        public int ContainerId { get; set; }

        public IList<string> ContractClientNames { get; set; }

        public MissionStandInfoViewModel StandInfo { get; set; }

        public string ContainerName { get; set; }

        public TrashType TrashType { get; set; }

        public int ContainerTypeId { get; set; }

        public string ContainerTypeName { get; set; }

        public decimal Volume { get; set; }
    }
}
