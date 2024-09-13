using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System;
using System.Collections.Generic;

namespace DispatcherService.Contracts.DataContracts.ShiftTask
{
    public class CreateShiftTaskContract : CommonHttpContract
    {
        public DateTime Date { get; set; }

        public int WorkShiftId { get; set; }

        public int OrgId { get; set; }

        [GCRequired("ИД рейсов")]
        public IList<int> RouteIds { get; set; }
    }
}
