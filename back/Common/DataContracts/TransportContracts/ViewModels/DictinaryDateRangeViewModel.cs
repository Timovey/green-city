using System;
using System.Collections.Generic;
using TransportContracts.DataContracts.Enums;

namespace TransportContracts.ViewModels
{
    public class DictinaryDateRangeViewModel
    {
        public int? OrganizationId { get; set; }
        public Dictionary<DateTime, TransportWorkDateType> DatesTransport { get; set; }

        public Dictionary<DateTime, Dictionary<int, DriverWorkDateType>> DatesDrivers { get; set; }

        public Dictionary<DateTime, DateWorkedDriverType> ResponseErrors { get; set; }
    }
}
