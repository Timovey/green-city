using GreenCity.Http;

namespace ExternalApi.Interface.Models.Dispatcher.TravelSheet
{
    public class ExtCreateTravelSheetRequest : CommonHttpContract
    {
        /// <summary> Дата ПЛ </summary>
        public string Date { get; set; }

        /// <summary> ИД машины </summary>
        public int CarId { get; set; }

        /// <summary> ИД водителя </summary>
        public int DriverId { get; set; }

        /// <summary> ИД организации </summary>
        public int OrgId { get; set; }

        public string StartDriveTime { get; set; }

        public string FinishDriveTime { get; set; }
    }
}
