using GreenCity.CommonUtils.CommonModels;

namespace TransportContracts.SortModels
{
    public class RepairCardSortModel : CommonSortingModel
    {
        public SortModel Event { get; set; }

        public SortModel OrganizationId { get; set; }

        public SortModel TransportId { get; set; }

    }
}
