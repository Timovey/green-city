using GreenCity.Http;

namespace ExternalApi.Interface.Models.Transport.TransportWorkShift
{
    public class ExtTransportWorkShiftDropdownViewModel : CommonHttpContract
    {
        /// <summary>
        /// ИД сущности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Данные для dropdown
        /// </summary>
        public string WorkshiftData { get; set; }
    }
}
