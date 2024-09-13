namespace MissionService.Contracts.ViewModels.Mission
{
    public class MissionForBuildShiftTaskReportViewModel
    {
        public int Id { get; set; }

        /// <summary> Комментарий к вывозу </summary>
        public string MissionComment { get; set; }

        public string ContractClients { get; set; }

        public int StandId { get; set; }

        public string Address { get; set; }

        public string TrashType { get; set; }

        public string StandType { get; set; }

        public string Contacts { get; set; }

        public int ContainerId { get; set; }

        public string ContainerName { get; set; }
    }
}