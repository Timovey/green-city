using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System.Collections.Generic;

namespace DispatcherService.Contracts.DataContracts.Brigade
{
    public class DeleteBrigadesContract : CommonHttpContract
    {
        [GCRequired("Бригады")]
        public IList<int> BrigadeIds { get; set; }
    }
}
