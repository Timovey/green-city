using GreenCity.Http;
using System;

namespace ExternalApi.Interface.Models.Transport.TransportDutyShift
{
    /// <summary>
    /// Контракт на создание дежурной смены
    /// </summary>
    public class ExtCreateTransportDutyShiftRequest : CommonHttpContract
    {
        /// <summary> ИД транспортного средства </summary>
        public int TransportId { get; set; }

        /// <summary> Дата выхода </summary>
        public DateTime Date { get; set; }
    }
}
