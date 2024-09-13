using GreenCity.Http;

namespace SDContracts.DataContracts.MissionInfo
{
    public class FullDataModelForCreateFakeMissionInfoContract : CommonHttpContract
    {
        public string StandAddress { get; set; }

        public string ContainerTypeName { get; set; }

        public decimal CustomVolume { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string CoordinatesComment { get; set; }

        public string ContainerName { get; set; }

        public int? ContractClientId { get; set; }

        public string ContractClientName { get; set; }

        public int? ContractId { get; set; }

        public string ContractNumber { get; set; }
    }
}
