using GreenCity.Http;
using System;
using System.Collections.Generic;
using TransportContracts.SortModels;

namespace TransportContracts.Filters
{
    public class Driver2TransportFilterModel : CommonHttpFilter
    {
        /// <summary> Марка транспорта </summary>
        public string TransportName { get; set; }

        /// <summary> ИД Организации транспорта</summary>
        public IList<int> TransportOrganizationIds { get; set; }

        /// <summary> Тип транспорта</summary>
        public IList<byte> TransportTypes { get; set; }

        /// <summary> Регистрационный номер </summary>
        public string RegNumber { get; set; }

        /// <summary> ИД транспорта</summary>
        public IList<int> TransportIds { get; set; }

        /// <summary> ИД Водителя</summary>
        public IList<int> DriverIds { get; set; }

        /// <summary> Имя водителя </summary>
        public string DriverName { get; set; }

        /// <summary> Фамилия водителя </summary>
        public string DriverSurname { get; set; }

        /// <summary> Должность </summary>
        public IList<byte> DriverPositionTypes { get; set; }

        /// <summary> ИД Организации водителя </summary>
        public IList<int> DriverOrganizationIds { get; set; }

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

        public Driver2TransportSortModel Sorting { get; set; }
    }
}
