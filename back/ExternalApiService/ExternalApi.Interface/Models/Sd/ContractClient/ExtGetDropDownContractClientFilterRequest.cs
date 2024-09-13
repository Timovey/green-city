using GreenCity.Http;

namespace ExternalApi.Interface.Models.Sd.ContractClient
{
    public class ExtGetDropDownContractClientFilterRequest : CommonHttpFilter
    {
        /// <summary> Наименование КА </summary>
        public string Name { get; set; }
    }
}
