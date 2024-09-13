using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.DriverVacation
{
    /// <summary>Контракт на удаление отпусков/больничных водителей </summary>
    public class DeleteDriverVacationContract : CommonHttpContract
    {
        /// <summary> ИД отпусков/больничных водителей </summary>
        public IList<int> DriverVacationIds { get; set; }
    }
}
