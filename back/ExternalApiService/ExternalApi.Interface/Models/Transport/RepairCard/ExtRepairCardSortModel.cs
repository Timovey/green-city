using GreenCity.CommonUtils.CommonModels;

namespace ExternalApi.Interface.Models.Transport.RepairCard
{
    public class ExtRepairCardSortModel : CommonSortingModel
    {
        public SortModel Event { get; set; }

        public SortModel OrganizationId { get; set; }

        public SortModel TransportId { get; set; }
    }
}
