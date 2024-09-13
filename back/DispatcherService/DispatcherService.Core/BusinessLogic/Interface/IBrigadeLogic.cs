using DispatcherService.Contracts.DataContracts.Brigade;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.ViewModels;
using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Core.BusinessLogic.Interface
{
    public interface IBrigadeLogic
    {
        Task<CommonHttpResponse<int>> CreateBrigadeAsync(CreateBrigadeContract contract);

        Task<CommonHttpResponse<IList<CommonBrigadeViewModel>>> GetFilteredBrigadesAsync(
            CommonBrigadesFilterModel filter
        );

        Task<CommonHttpResponse> DeleteBrigadesAsync(DeleteBrigadesContract contract);

        Task<CommonHttpResponse<IList<AvailableRoutesForCreateBrigadeViewModel>>> GetAvailableRoutesForCreateBrigadeAsync(
            GetAvailableRoutesForCreateBrigadeContract contract
        );
    }
}
