using GreenCity.Http.Attributes.Validation;
using ScheduleService.Contracts.DataContracts.DateRange;

namespace ScheduleService.Contracts.DataContracts.ContractSchedule.Mission
{
    /// <summary>
    /// Объект коллекции расписаний гибкого диапазонного графика договоров
    /// </summary>
    public class SSContractScheduleCollectionModel : BaseDateRangeModel
    {
        /// <summary>
        /// Расписание договора
        /// </summary>
        [GCRequired]
        public SSContractScheduleModel ContractSchedule { get; set; }

        public override SSContractScheduleCollectionModel Clone()
        {
            return new SSContractScheduleCollectionModel
            {
                Id = Id,
                CreateDate = CreateDate,
                StartDate = StartDate,
                EndDate = EndDate,
                ContractSchedule = ContractSchedule //!_! вот это вообще не факт что сработает
            };
        }
    }
}
