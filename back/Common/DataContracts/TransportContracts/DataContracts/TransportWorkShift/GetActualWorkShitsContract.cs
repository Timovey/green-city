using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.TransportWorkShift
{
    /// <summary>
    /// Контракт получения смен на число
    /// </summary>
    public class GetActualWorkShitsContract : CommonHttpContract
    {
        /// <summary>
        /// Дату
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Включать в фильтр организации пользователя
        /// </summary>
        public bool IsFilterByUser { get; set; }

        /// <summary>
        /// Фильтр на организацию
        /// </summary>
        public IList<int> OrganizationIds { get; set; }
    }
}
