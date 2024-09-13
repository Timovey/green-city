using GreenCity.Http;
using System;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    public class ExtGetTransportSheduleDateRangeRequest : CommonHttpContract
    {
        public int TransportId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
