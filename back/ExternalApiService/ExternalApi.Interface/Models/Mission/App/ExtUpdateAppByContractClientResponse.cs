using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Mission.App
{
    public class ExtUpdateAppByContractClientResponse
    {
        /// <summary> Идентификатор заявки </summary>
        public int Id { get; set; }

        /// <summary> Дата </summary>
        public string Date { get; set; }

        /// <summary> Контейнеры </summary>
        public IList<ExtContainerInAppInfo> Containers { get; set; }
    }
}
