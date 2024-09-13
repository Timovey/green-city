using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.DriverVacation
{
    /// <summary> Получениеотпуска/больничного водителя</summary>
    public class ExtGetDriverVacationRequest : CommonHttpContract
    {
        /// <summary> ИД отпусков/больничных водителей </summary>
        public IList<int> DriverVacationIds { get; set; }
    }
}
