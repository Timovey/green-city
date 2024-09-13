using System.Collections.Generic;

namespace SDService.Database.Models.Contracts
{
    public class GetFilteredStandsForRouteFilter
    {
        public IList<int> IncludeStandIds { get; set; }

        /// <summary>
        /// ИД КП, которые не нужно брать в выборку
        /// </summary>
        public IList<int> ExcludeStandIds { get; set; }

        /// <summary>
        /// ИД типов контейнеров
        /// </summary>
        public IList<int> ContainerTypeIds { get; set; }

        /// <summary>
        /// Идентификаторы контейнеров
        /// </summary>
        public IList<int> IncludeContainerIds { get; set; }

        public string StandAddress { get; set; }
    }
}
