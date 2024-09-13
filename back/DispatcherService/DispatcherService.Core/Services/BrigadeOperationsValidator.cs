using DispatcherService.Contracts.DataContracts.Brigade;
using DispatcherService.Core.Services.Interface;
using DispatcherService.Database.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DispatcherService.Core.Services
{
    public class BrigadeOperationsValidator : IBrigadeOperationsValidator
    {
        public async Task<(bool IsSuccess, string Error)> ValidateCreateBrigadeAsync(
            CreateBrigadeContract contract,
            IBrigadeStorage brigadeStorage
        )
        {
            if (contract == default)
                return (false, "Пустой запрос");

            if (contract.RouteIds == default ||
                contract.RouteIds.Count == 0
            )
            {
                return (false, "Необходимо выбрать хотябы один рейс");
            }

            if (contract.AuxWorkerIds == default ||
                contract.AuxWorkerIds.Count == 0
            )
            {
                return (false, "Необходимо выбрать хотябы одного подсобного рабочего");
            }

            var brigade2Routes = await brigadeStorage.GetBrigade2RoutesByRouteIdsAsync(contract.RouteIds);
            if (brigade2Routes.Count != 0)
            {
                return (false, $"На рейсы с идентификаторами {string.Join(';', brigade2Routes.Select(x => x.RouteId).Distinct())}, уже созданы бригады");
            }

            return (true, default);
        }
    }
}
