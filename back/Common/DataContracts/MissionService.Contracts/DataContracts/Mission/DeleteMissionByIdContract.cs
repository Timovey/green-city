using GreenCity.Http;

namespace MissionService.Contracts.DataContracts.Mission
{
    public class DeleteMissionByIdContract : CommonHttpContract
    {
        public int Id { get; set; }
    }
}
