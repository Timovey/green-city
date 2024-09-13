using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System.Collections.Generic;

namespace DispatcherService.Contracts.DataContracts.ShiftTask
{
    public class DeleteShiftTasksContract : CommonHttpContract
    {
        [GCRequired("Сменные задания")]
        public IList<int> ShiftTaskIds { get; set; }
    }
}
