using GreenCity.Http;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Mission.App
{
    public class ExtCommonAppFilterRequest : CommonHttpFilter
    {
        /// <summary> Дата заявки </summary>
        public string AppDate { get; set; }

        /// <summary> Идентификаторы КА </summary>
        public IList<int> ContractClientIds { get; set; }

        /// <summary> Идентификаторы договоров </summary>
        public IList<int> ContractIds { get; set; }

        /// <summary> Типы источников создания </summary>
        public IList<ExtCreatorType> CreatorTypes { get; set; }

        /// <summary> Тип заявки </summary>
        public ExtAppType AppType { get; set; }
    }
}
