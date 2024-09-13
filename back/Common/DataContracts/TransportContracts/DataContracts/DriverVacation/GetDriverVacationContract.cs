using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.DriverVacation
{
    /// <summary> Получениеотпуска/больничного водителя</summary>
    public class GetDriverVacationContract : CommonHttpContract
    {
        /// <summary> ИД отпусков/больничных водителей </summary>
        public IList<int> DriverVacationIds { get; set; }
    }
}
