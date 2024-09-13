using GreenCity.CommonUtils.CommonModels;

namespace ExternalApi.Interface.Models.Sd.Stand
{
    public class ExtStandSortingModel : CommonSortingModel
    {
        public SortModel IsHighSecurityObject { get; set; }

        public SortModel Address { get; set; }

        public SortModel DriveWay { get; set; }

        public SortModel Contacts { get; set; }

        public SortModel StandType { get; set; }

        public SortModel IsValidated { get; set; }

        public SortModel HaveBase { get; set; }

        public SortModel HaveRoof { get; set; }

        public SortModel HaveFence { get; set; }
    }
}
