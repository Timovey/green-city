using DispatcherService.Contracts.DataContracts.AuxiliaryWorker;
using DispatcherService.Contracts.DataContracts.Brigade;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.ViewModels;
using GreenCity.Http;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Contracts.Interfaces
{
    public interface IDispatcherApi
    {
        [Post("/Dispatcher/GetFilteredAuxiliaryWorkers")]
        Task<CommonHttpResponse<IList<AuxiliaryWorkerViewModel>>> GetFilteredAuxiliaryWorkersAsync(
          [Body] AuxiliaryWorkerFilterModel filter
       );

        [Post("/Dispatcher/GetAuxiliaryWorker")]
        Task<CommonHttpResponse<AuxiliaryWorkerViewModel>> GetAuxiliaryWorkerAsync(
            [Body] GetAuxiliaryWorkerContract getContract
        );

        [Post("/Dispatcher/CreateAuxiliaryWorker")]
        Task<CommonHttpResponse<AuxiliaryWorkerViewModel>> CreateAuxiliaryWorkerAsync(
            [Body] CreateAuxiliaryWorkerContract createContract
        );

        [Post("/Dispatcher/UpdateAuxiliaryWorker")]
        Task<CommonHttpResponse<AuxiliaryWorkerViewModel>> UpdateAuxiliaryWorkerAsync(
            [Body] UpdateAuxiliaryWorkerContract updateContract
        );

        [Post("/Dispatcher/DeleteAuxiliaryWorker")]
        Task<CommonHttpResponse> DeleteAuxiliaryWorkerAsync(
            [Body] DeleteAuxiliaryWorkerContract deleteContract
        );

        [Post("/Dispatcher/CreateBrigade")]
        Task<CommonHttpResponse<int>> CreateBrigadeAsync(
           [Body] CreateBrigadeContract contract
        );

        [Post("/Dispatcher/GetFilteredBrigades")]
        Task<CommonHttpResponse<IList<CommonBrigadeViewModel>>> GetFilteredBrigadesAsync(
           [Body] CommonBrigadesFilterModel filter
        );

        [Post("/Dispatcher/DeleteBrigades")]
        Task<CommonHttpResponse> DeleteBrigadesAsync(
           [Body] DeleteBrigadesContract contract
        );

        [Post("/Dispatcher/GetAvailableRoutesForCreateBrigade")]
        Task<CommonHttpResponse<IList<AvailableRoutesForCreateBrigadeViewModel>>> GetAvailableRoutesForCreateBrigadeAsync(
           [Body] GetAvailableRoutesForCreateBrigadeContract contract
        );
    }
}
