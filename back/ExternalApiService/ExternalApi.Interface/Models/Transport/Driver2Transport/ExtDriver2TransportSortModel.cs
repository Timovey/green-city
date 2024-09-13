using GreenCity.CommonUtils.CommonModels;

namespace ExternalApi.Interface.Models.Transport.Driver2Transport
{
    public class ExtDriver2TransportSortModel : CommonSortingModel
    {
        public SortModel TransportModel { get; set; }

        public SortModel TransportOrganizationId { get; set; }

        public SortModel TransportType { get; set; }

        public SortModel DriverId { get; set; }

        public SortModel DriverName { get; set; }

        public SortModel DriverSurname { get; set; }

        public SortModel DriverType { get; set; }

        public SortModel DriverOrganizationId { get; set; }

        public SortModel DrivingSkills { get; set; }
    }
}
