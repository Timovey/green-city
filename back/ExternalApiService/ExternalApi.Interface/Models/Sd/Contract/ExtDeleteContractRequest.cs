using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Sd.Contract
{
    public class ExtDeleteContractRequest : CommonHttpContract
    {
        /// <summary>
        /// ИД удаляемого договора
        /// </summary>
        public IList<int> ContractIds { get; set; }
    }
}
