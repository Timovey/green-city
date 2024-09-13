using DispatcherService.Contracts.DataContracts.AuxiliaryWorker;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.ViewModels;
using GreenCity.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispatcherService.Database.Interfaces
{
    public interface IAuxiliaryWorkerStorage
    {
        Task<AuxiliaryWorkerViewModel> CreateAuxiliaryWorkerAsync(CreateAuxiliaryWorkerContract createContract);

        Task<AuxiliaryWorkerViewModel> UpdateAuxiliaryWorkerAsync(UpdateAuxiliaryWorkerContract updateContract);

        Task<IList<AuxiliaryWorkerViewModel>> GetFilteredAuxiliaryWorkersAsync(AuxiliaryWorkerFilterModel filter);

        Task<IList<AuxiliaryWorkerViewModel>> GetAuxiliaryWorkersWithoutFilterAsync(CommonHttpFilter filter);

        Task<AuxiliaryWorkerViewModel> GetAuxiliaryWorkerAsync(GetAuxiliaryWorkerContract getContract);

        Task<bool> DeleteAuxiliaryWorkerAsync(DeleteAuxiliaryWorkerContract deleteContract);
    }
}
