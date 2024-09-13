using GreenCity.Http;
using System.Collections.Generic;

namespace SDContracts.DataContracts.Contract
{
    public class DeleteContractContract : CommonHttpContract
    {
        /// <summary>
        /// ИД удаляемого договора
        /// </summary>
        public IList<int> ContractIds { get; set; }
    }
}
