using GreenCity.Http;
using SDContracts.Enums;
using System.Collections.Generic;

namespace SDContracts.Filter.DropDown
{
    public class GetDropDownContractsFilterModel : CommonHttpFilter
    {
        public string ContractNumber { get; set; }

        public IList<int> ContractClientIds { get; set; }

        /// <summary>
        /// Енам для отсеивания фейковых данных, которые были созданы для заявок с карты
        /// </summary>
        public IList<MissionInfoEntityType> EntityTypes { get; set; }
    }
}
