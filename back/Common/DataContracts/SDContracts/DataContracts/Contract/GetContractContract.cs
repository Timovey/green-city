using GreenCity.Http;

namespace SDContracts.DataContracts.Contract
{
    public class GetContractContract : CommonHttpContract
    {
        /// <summary>
        /// ИД необходимого договора
        /// </summary>
        public int ContractId { get; set; }
    }
}
