using GreenCity.Http;

namespace ExternalApi.Interface.Models.Sd.ContractClient
{
    public class ExtGetContractClientRequest : CommonHttpContract
    {
        /// <summary>
        /// ИД улаляемого контрагента
        /// </summary>
        public int ContractClientId { get; set; }
    }
}
