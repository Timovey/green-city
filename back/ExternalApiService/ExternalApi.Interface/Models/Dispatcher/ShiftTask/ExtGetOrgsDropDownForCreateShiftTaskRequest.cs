using GreenCity.Http;

namespace ExternalApi.Interface.Models.Dispatcher.ShiftTask
{
    public class ExtGetOrgsDropDownForCreateShiftTaskRequest : CommonHttpContract
    {
        public int WorkShiftId { get; set; }
    }
}
