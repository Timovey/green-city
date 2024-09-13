using GreenCity.Http;

namespace ExternalApi.Interface.Models.Dispatcher.Route
{
    public class ExtGetAvailableRoutesForCreateShiftTaskRequest : CommonHttpContract
    {
        public string Date { get; set; }

        public int WorkShiftId { get; set; }
    }
}
