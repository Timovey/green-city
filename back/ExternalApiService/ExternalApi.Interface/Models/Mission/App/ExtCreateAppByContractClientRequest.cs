using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Mission.App
{
    public class ExtCreateAppByContractClientRequest : CommonHttpContract
    {
        /// <summary> Дата заявки </summary>
        public string Date { get; set; }

        /// <summary> ИД контрагента </summary>
        public int ContractClientId { get; set; }

        /// <summary> ИД договора </summary>
        public int ContractId { get; set; }

        /// <summary> Комментарий </summary>
        public string Comment { get; set; }

        public IList<ExtContainerInAppInfo> Containers { get; set; }
    }
}
