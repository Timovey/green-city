using System;
using System.Collections.Generic;
using GreenCity.Http;

namespace ExternalApi.Interface.Models.Report
{
    public class ExtGetDoneTaskListFileRequest : CommonHttpContract
    {
        /// <summary> Дата начала </summary>
        public DateTime DateFrom { get; set; }

        /// <summary> Дата окончания </summary>
        public DateTime DateTo { get; set; }

        /// <summary> ИД машин </summary>
        public IList<int> CarIds { get; set; }
    }
}