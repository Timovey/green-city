using GreenCity.Http;

namespace ExternalApi.Interface.Models.Sd.ContractClient
{
    public class ExtDeleteContractClientRequest : CommonHttpContract
    {
        /// <summary>
        /// ИД улаляемого контрагента
        /// </summary>
        public int ContractClientId { get; set; }
    }
}
