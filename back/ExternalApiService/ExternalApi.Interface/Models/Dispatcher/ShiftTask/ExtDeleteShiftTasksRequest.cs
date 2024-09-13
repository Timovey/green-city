using GreenCity.Http;

namespace ExternalApi.Interface.Models.Dispatcher.ShiftTask
{
    public class ExtDeleteShiftTasksRequest : CommonHttpContract
    {
        public int Id { get; set; }
    }
}
