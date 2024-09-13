using GreenCity.Http;
using System;
using System.Collections.Generic;

namespace MissionService.Contracts.DataContracts.App
{
    /// <summary>
    /// Запуск полной перегенерации заявок на основе заполненных данных по графикам контейнеров
    /// </summary>
    public class RunAppGenerationInSingleThreadContract : CommonHttpContract
    {
        public DateTime Date { get; set; }

        public IList<int> ContractClients { get; set; }

        public IList<int> ContractIds { get; set; }
    }
}
