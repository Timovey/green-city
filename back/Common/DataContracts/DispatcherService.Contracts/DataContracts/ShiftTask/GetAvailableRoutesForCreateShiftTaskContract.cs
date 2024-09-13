using GreenCity.Http;
using System;

namespace DispatcherService.Contracts.DataContracts.ShiftTask
{
    public class GetAvailableRoutesForCreateShiftTaskContract : CommonHttpContract
    {
        public DateTime Date { get; set; }

        public int WorkShiftId { get; set; }
    }
}
