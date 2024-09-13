using GreenCity.Http;

namespace AuthContracts.DataContracts
{
    public class GetOrgsContract : CommonHttpContract
    {
        public bool IsFilterByUserId { get; set; } = true;
    }
}
