using GreenCity.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDContracts.DataContracts.Stand
{
    public class DeleteStandContract : CommonHttpContract
    {
        /// <summary>
        /// ИД необходимых контейнерных площадок
        /// </summary>
        [Required]
        public IList<int> StandIds { get; set; }
    }
}
