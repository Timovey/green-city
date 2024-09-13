using GreenCity.Http;

namespace SDContracts.DataContracts.ContractClient
{
    public class GetContractClientContract : CommonHttpContract
    {
        /// <summary>
        /// ИД необходимого клиента
        /// </summary>
        public int ContractClientId { get; set; }
    }
}
