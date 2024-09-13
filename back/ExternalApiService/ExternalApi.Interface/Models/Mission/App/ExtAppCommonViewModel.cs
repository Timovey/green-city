namespace ExternalApi.Interface.Models.Mission.App
{
    public class ExtAppCommonViewModel
    {
        public int Id { get; set; }

        public string ContractClientName { get; set; }

        public string ContractNumber { get; set; }

        public ExtAppComputedStatus AppStatus { get; set; }

        public string Comment { get; set; }
    }
}
