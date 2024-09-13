using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System;
using System.Collections.Generic;

namespace DispatcherService.Contracts.DataContracts.Brigade
{
    public class CreateBrigadeContract : CommonHttpContract
    {
        public DateTime Date { get; set; }
        
        public int WorkShiftId { get; set; }

        [GCRequired("Подсобные рабочие")]
        public IList<int> AuxWorkerIds { get; set; }

        [GCRequired("Рейсы")]
        public IList<int> RouteIds { get; set; }
    }
}
