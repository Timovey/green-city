using GreenCity.Http;
using System;

namespace DispatcherService.Contracts.DataContracts.TravelSheet
{
    public class CreateTravelSheetContract : CommonHttpContract
    {
        /// <summary> Дата ПЛ </summary>
        public DateTime Date { get; set; }

        /// <summary> ИД машины </summary>
        public int CarId { get; set; }

        /// <summary> ИД водителя </summary>
        public int DriverId { get; set; }

        /// <summary> ИД организации </summary>
        public int OrgId { get; set; }
    }
}
