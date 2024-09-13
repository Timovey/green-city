using ExternalApi.Interface.Http;
using System;
using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Transport.Driver2Transport
{
    public class ExtDriver2TransportFilterRequest : CommonHttpFilterV2
    {
        /// <summary> Марка транспорта </summary>
        public string TransportModel { get; set; }

        /// <summary> ИД Организации транспорта</summary>
        public IList<int> TransportOrganizationIds { get; set; }

        /// <summary> Тип транспорта</summary>
        public IList<int> TransportTypes { get; set; }

        /// <summary> ИД транспорта</summary>
        public IList<int> TransportIds { get; set; }

        /// <summary> ИД Водителя</summary>
        public IList<int> DriverIds { get; set; }

        /// <summary> Имя водителя </summary>
        public string DriverName { get; set; }

        /// <summary> Фамилия водителя </summary>
        public string DriverSurname { get; set; }

        /// <summary> ИД Организации водителя </summary>
        public IList<int> DriverOrganizationIds { get; set; }

        /// <summary> Классификации водителя </summary>
        public string DrivingSkills { get; set; }

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

        public ExtDriver2TransportSortModel Sorting { get; set; }
    }
}
