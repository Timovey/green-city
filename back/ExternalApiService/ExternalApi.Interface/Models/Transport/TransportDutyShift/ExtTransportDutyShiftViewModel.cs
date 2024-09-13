using ExternalApi.Interface.Models.Transport.Transport;
using System;

namespace ExternalApi.Interface.Models.Transport.TransportDutyShift
{
    public class ExtTransportDutyShiftViewModel
    {
        /// <summary> ИД аренды транспорта</summary>
        public int TransportDutyShiftId { get; set; }

        /// <summary> ИД транспортного средства </summary>
        public int TransportId { get; set; }

        /// <summary>
        /// Название транспортного средства
        /// </summary>
        public string TransportName { get; set; }

        /// <summary> ИД Организации транспорта</summary>
        public int? TransportOrganizationId { get; set; }

        /// <summary> Тип транспорта</summary>
        public ExtTransportType TransportType { get; set; }

        /// <summary> Регистрационный номер </summary>
        public string RegNumber { get; set; }

        /// <summary> Дата выхода </summary>
        public DateTime Date { get; set; }
    }
}
