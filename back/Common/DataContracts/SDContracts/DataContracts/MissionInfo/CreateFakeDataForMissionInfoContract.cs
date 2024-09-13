using GreenCity.Http;

namespace SDContracts.DataContracts.MissionInfo
{
    public class CreateFakeDataForMissionInfoContract : CommonHttpContract
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string CoordinatesComment { get; set; }

        public decimal CustomVolume { get; set; }

        public int? ContractClientId { get; set; }

        public int? ContractId { get; set; }
    }
}
