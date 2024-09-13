using GreenCity.Http;
using System;

namespace DispatcherService.Contracts.DataContracts.Brigade
{
    public class GetAvailableRoutesForCreateBrigadeContract : CommonHttpContract
    {
        public DateTime Date { get; set; }

        public int WorkShiftId { get; set; }
    }
}
