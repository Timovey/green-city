using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;

namespace DispatcherService.Contracts.DataContracts.TravelSheet
{
    public class FixTravelSheetDriveTimesContract : CommonHttpContract
    {
        public int TravelSheetId { get; set; }

        [GCRequired("Время выезда")]
        public string StartDriveTime { get; set; }

        [GCRequired("Время заезда")]
        public string FinishDriveTime { get; set; }
    }
}
