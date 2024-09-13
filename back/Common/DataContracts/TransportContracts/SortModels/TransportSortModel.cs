using GreenCity.CommonUtils.CommonModels;

namespace TransportContracts.SortModels
{
    public class TransportSortModel : CommonSortingModel
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
