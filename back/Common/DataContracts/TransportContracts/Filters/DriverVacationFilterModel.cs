using GreenCity.Http;
using System;
using System.Collections.Generic;
using TransportContracts.SortModels;

namespace TransportContracts.Filters
{
    public class DriverVacationFilterModel : CommonHttpFilter
    {

        /// <summary> ИД Водителя </summary>
        public IList<int> DriverIds { get; set; }

        /// <summary> ИД сменного Водителя </summary>
        public IList<int> ReplacementDriverIds { get; set; }

        /// <summary> Отпуск/Больничный</summary>
        public IList<byte> VacationTypes { get; set; }

        /// <summary> Дата создания сущности От </summary>
        public DateTime? CreateDateFrom { get; set; }

        /// <summary> Дата создания сущности До</summary>
        public DateTime? CreateDateTo { get; set; }

        /// <summary> Дата начала работы От</summary>
        public DateTime? StartDateFrom { get; set; }

        /// <summary> Дата начала работы до</summary>
        public DateTime? StartDateTo { get; set; }

        /// <summary> Дата окончания работы От</summary>
        public DateTime? EndDateFrom { get; set; }

        /// <summary> Дата окончания работы До</summary>
        public DateTime? EndDateTo { get; set; }

        public DriverVacationSortModel Sorting { get; set; }
    }
}
