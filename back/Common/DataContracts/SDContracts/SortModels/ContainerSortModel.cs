using GreenCity.CommonUtils.CommonModels;

namespace SDContracts.SortModels
{
    public class ContainerSortModel : CommonSortingModel
    {
        public SortModel Name { get; set; }

        public SortModel ContainerTypeId { get; set; }

        public SortModel TrashType { get; set; }

        public SortModel IsInContract { get; set; }

        public SortModel IsValidated { get; set; }

        public SortModel Volume { get; set; }
    }
}
