using GreenCity.Http;
using System;

namespace TransportContracts.DataContracts.TransportDutyShift
{
    /// <summary>
    /// Контракт на создание дежурной смены
    /// </summary>
    public class CreateTransportDutyShiftContract : CommonHttpContract
    {
        /// <summary> ИД транспортного средства </summary>
        public int TransportId { get; set; }

        /// <summary> Дата выхода </summary>
        public DateTime Date { get; set; }
    }    
}
