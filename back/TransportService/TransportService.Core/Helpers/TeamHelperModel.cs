using TransportContracts.DataContracts.Enums;

namespace TransportService.Core.Helpers
{
    public class TeamHelperModel
    {
       
        public int TransportId { get; set; }

        public DateTime DateTime { get; set; }

        public int OrganizationId { get; set; }

        public int DriverId { get; set; }

        public DriverWorkDateType DriverWorkDateType{ get; set; }
    }
}
