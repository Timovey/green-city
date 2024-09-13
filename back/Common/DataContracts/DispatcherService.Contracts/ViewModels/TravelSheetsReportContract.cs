namespace DispatcherService.Contracts.ViewModels
{
    public class TravelSheetsReportContract
    {
        //инфа по дате
        public string DateDay { get; set; }

        public string DateMonth { get; set; }

        public string DateYear { get; set; }

        /// <summary>
        /// Номер путевого листа
        /// </summary>
        public string WayBillId { get; set; }

        /// <summary>
        /// Полная инфа о организации
        /// </summary>
        public string OrganizationInfo { get; set; }

        /// <summary>
        /// Имя транспорта
        /// </summary>
        public string TransportName { get; set; }

        /// <summary>
        /// Номер транспорта
        /// </summary>
        public string TransportNumber { get; set; }

        /// <summary>
        /// Номер лецензии
        /// </summary>
        public string DrivingLicenseNumber { get; set; }

        /// <summary>
        /// Полное имя водителя
        /// </summary>
        public string DriverFullName { get; set; }

        /// <summary>
        /// Фамилия водителяс инициалами
        /// </summary>
        public string DriverShortName { get; set; }
    }
}
