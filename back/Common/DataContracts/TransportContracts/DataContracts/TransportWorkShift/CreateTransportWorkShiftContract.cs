using GreenCity.Http;
using System;
using System.Collections.Generic;
using TransportContracts.DataContracts.Enums;

namespace TransportContracts.DataContracts.TransportWorkShift
{
    public class CreateTransportWorkShiftContract : CommonHttpContract
    {
        /// <summary>
        /// ID транспорта
        /// </summary>
        public int TransportId { get; set; }

        /// <summary>
        /// Дата выхода
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Список организаций, которым доступна рабочая смена
        /// </summary>
        public IList<int> OrganizationIds { get; set; }

        /// <summary>
        /// ИД Водителя
        /// </summary>
        public int DriverId { get; set; }

        /// <summary>
        /// Рабочая смена(график) водителя
        /// </summary>
        public DriverWorkDateType DriverWorkDateType { get; set; }
    }
}
