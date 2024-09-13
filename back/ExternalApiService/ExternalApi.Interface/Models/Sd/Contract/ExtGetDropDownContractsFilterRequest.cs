using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Contract
{
    public class ExtGetDropDownContractsFilterRequest : CommonHttpFilter
    {
        /// <summary> Номер договора </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// ИД контрагентов (клиента)
        /// </summary>
        public IList<int> ContractClientIds { get; set; }
    }
}
