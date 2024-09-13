using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System.Collections.Generic;

namespace DispatcherService.Contracts.DataContracts.ShiftTask
{
    public class GetOrgsDropDownForCreateShiftTaskContract : CommonHttpContract
    {
        [GCRequired("Экипажи")]
        public IList<int> WorkShiftIds { get; set; }
    }
}
