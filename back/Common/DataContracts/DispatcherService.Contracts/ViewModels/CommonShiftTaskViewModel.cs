namespace DispatcherService.Contracts.ViewModels
{
    public class CommonShiftTaskViewModel
    {
        public int Id { get; set; }

        public int TravelSheetId { get; set; }

        public string TravelSheetInfo { get; set; }

        public string OrgInfo { get; set; }

        public string CarWithDriverInfo { get; set; }

        public string RoutesInfo { get; set; }
    }
}
