using GreenCity.Http;
using MissionService.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace MissionService.Contracts.Filters.App
{
    public class CommonAppFilterModel : CommonHttpFilter
    {
        /// <summary> Дата заявки </summary>
        public DateTime AppDate { get; set; }

        /// <summary> Идентификаторы КА </summary>
        public IList<int> ContractClientIds { get; set; }

        /// <summary> Идентификаторы договоров </summary>
        public IList<int> ContractIds { get; set; }

        /// <summary> Типы источников создания </summary>
        public IList<CreatorType> CreatorTypes { get; set; }

        /// <summary> Тип заявки </summary>
        public AppType AppType { get; set; }
    }
}
