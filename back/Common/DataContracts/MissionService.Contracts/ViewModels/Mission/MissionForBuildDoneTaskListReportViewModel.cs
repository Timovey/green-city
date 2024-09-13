namespace MissionService.Contracts.ViewModels.Mission
{
    public class MissionForBuildDoneTaskListReportViewModel
    {
        public int Id { get; set; }

        public int StandId { get; set; }

        public string Address { get; set; }

        public int ContainerId { get; set; }

        public string ContainerName { get; set; }

        public decimal FactVolume { get; set; }
    }
}
