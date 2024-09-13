using GreenCity.Http;
using SDContracts.Enums;
using System.Collections.Generic;

namespace SDContracts.Filter.DropDown
{
    public class GetDropDownContractClientFilterModel : CommonHttpFilter
    {
        public string Name { get; set; }

        /// <summary>
        /// Енам для отсеивания фейковых данных, которые были созданы для заявок с карты
        /// </summary>
        public IList<MissionInfoEntityType> EntityTypes { get; set; }
    }
}
