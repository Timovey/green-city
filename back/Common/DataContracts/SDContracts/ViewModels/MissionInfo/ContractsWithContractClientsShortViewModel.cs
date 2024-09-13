using System.Collections.Generic;

namespace SDContracts.ViewModels.MissionInfo
{
    /// <summary>
    /// Короткая информация по КА и договорам
    /// </summary>
    public class ContractsWithContractClientsShortViewModel
    {
        /// <summary>
        /// Информация по КА
        /// </summary>
        public IDictionary<int, ContractClientShortInfo> ContractClientShortInfo { get; set; }

        /// <summary>
        /// Информация по договорам
        /// </summary>
        public IDictionary<int, ContractShortInfo> ContractShortInfo { get; set; }
    }
}
