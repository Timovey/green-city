using GreenCity.Http;
using System;

namespace TransportContracts.DataContracts.TransportShedule
{
    public class GetTransportSheduleDateRangeContract : CommonHttpContract
    {
        public int TransportId { get; set; }

        public int? OrganizationId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
