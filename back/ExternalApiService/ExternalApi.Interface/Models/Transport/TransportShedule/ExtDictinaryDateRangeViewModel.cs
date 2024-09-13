using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.TransportShedule
{
    public class ExtDictinaryDateRangeViewModel
    {
        public Dictionary<DateTime, ExtTransportWorkDateType> DatesTransport { get; set; }

        public Dictionary<DateTime, Dictionary<int, ExtDriverWorkDateType>> DatesDrivers { get; set; }

        public Dictionary<DateTime, ExtDateWorkedDriverType> ResponseErrors { get; set; }
    }
}
