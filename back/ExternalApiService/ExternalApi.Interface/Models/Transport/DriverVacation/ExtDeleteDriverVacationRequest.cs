using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.DriverVacation
{
    /// <summary>Контракт на удаление отпусков/больничных водителей </summary>
    public class ExtDeleteDriverVacationRequest : CommonHttpContract
    {
        /// <summary> ИД отпусков/больничных водителей </summary>
        public IList<int> DriverVacationIds { get; set; }
    }
}
