using GreenCity.CommonUtils.CommonModels;

namespace ExternalApi.Interface.Models.Transport.Transport
{
    public class ExtTransportSortModel : CommonSortingModel
    {
        public SortModel Name { get; set; }

        public SortModel OrganizationId { get; set; }

        public SortModel RegNumber { get; set; }

        public SortModel TransportType { get; set; }

        public SortModel BodyColor { get; set; }

        public SortModel TankFence { get; set; }

        public SortModel CargoVolume { get; set; }
    }
}
