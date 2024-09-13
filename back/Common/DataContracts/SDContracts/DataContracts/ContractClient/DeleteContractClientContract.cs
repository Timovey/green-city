using GreenCity.Http;

namespace SDContracts.DataContracts.ContractClient
{
    public class DeleteContractClientContract : CommonHttpContract
    {
        /// <summary>
        /// ИД улаляемого контрагента
        /// </summary>
        public int ContractClientId { get; set; }
    }
}
