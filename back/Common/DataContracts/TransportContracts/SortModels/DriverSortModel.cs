using GreenCity.CommonUtils.CommonModels;

namespace TransportContracts.SortModels
{
    public class DriverSortModel : CommonSortingModel
    {
        public SortModel TableNumber { get; set; }

        public SortModel IsFired { get; set; }

        public SortModel FiredDate { get; set; }

        public SortModel DriverName { get; set; }

        public SortModel DriverSurname { get; set; }

        public SortModel AcceptWorkDate { get; set; }

        public SortModel DriverPositionType { get; set; }

        public SortModel OrganizationId { get; set; }

        public SortModel SkillClass { get; set; }

        public SortModel DrivingSkillsJson { get; set; }

        public SortModel ContactNumber { get; set; }

        public SortModel DriversLicense { get; set; }

        public SortModel BirthDate { get; set; }

        public SortModel BirthPlace { get; set; }

        public SortModel RegAddress { get; set; }

        public SortModel FactAddress { get; set; }

        public SortModel PassportSerie { get; set; }

        public SortModel PassportNumber { get; set; }

        public SortModel IssueDate { get; set; }

        public SortModel Issuer { get; set; }
    }
}
