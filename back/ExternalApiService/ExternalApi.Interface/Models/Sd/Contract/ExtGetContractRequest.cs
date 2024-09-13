using GreenCity.Http;

namespace ExternalApi.Interface.Models.Sd.Contract
{
    public class ExtGetContractRequest : CommonHttpContract
    {
        /// <summary>
        /// ИД необходимого договора
        /// </summary>
        public int ContractId { get; set; }
    }
}
