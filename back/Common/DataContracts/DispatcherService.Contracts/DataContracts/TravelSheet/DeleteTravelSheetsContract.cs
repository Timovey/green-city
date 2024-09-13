using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System.Collections.Generic;

namespace DispatcherService.Contracts.DataContracts.TravelSheet
{
    public class DeleteTravelSheetsContract : CommonHttpContract
    {
        [GCRequired("Путевые листы")]
        public IList<int> TravelSheetIds { get; set; }
    }
}
