using GreenCity.Http;

namespace ExternalApi.Interface.Models.Dispatcher.Route
{
    public class ExtGetAvailableRoutesForCreateBrigadeRequest : CommonHttpContract
    {
        public string Date { get; set; }

        public int WorkShiftId { get; set; }
    }
}
