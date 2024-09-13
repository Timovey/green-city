using GreenCity.CommonUtils.CommonModels;

namespace ExternalApi.Interface.Models.Sd.ContractClient
{
    public class ExtContractClientSortingModel : CommonSortingModel
    {
        public SortModel ClientName { get; set; }

        public SortModel ClientContact { get; set; }

        public SortModel Email { get; set; }

        public SortModel Inn { get; set; }

        public SortModel Kpp { get; set; }

        public SortModel Ogrn { get; set; }

        public SortModel Rschet { get; set; }

        public SortModel Bank { get; set; }

        public SortModel Bik { get; set; }

        public SortModel Kschet { get; set; }

        public SortModel LegalAddress { get; set; }

        public SortModel PostalAddress { get; set; }

        public SortModel SupervizorName { get; set; }

        public SortModel SupervizorPosition { get; set; }

        public SortModel ResponsibleTkoPersonName { get; set; }

        public SortModel ResponsibleTkoPersonPhone { get; set; }

        public SortModel IsValidated { get; set; }
    }
}
