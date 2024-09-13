using GreenCity.Http;
using SDContracts.DataContracts.Container;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.ViewModels;
using SDService.Database.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Database.Interfaces
{
    public interface IContainerStorage
    {
        Task<int> CreateContainerAsync(CreateContainerContract createContract);

        Task<bool> DeleteContainersAsync(DeleteContainerContract deleteContract);

        Task<IList<ContainerViewModel>> GetContainersAsync(GetContainerContract getContract);

        Task<IList<ContainerViewModel>> GetContainersWithoutFilterAsync(CommonHttpFilter filter);

        Task<IList<ContainerViewModel>> GetFilteredContainersAsync(ContainerFilterModel filter);

        Task<bool> UpdateContainersAsync(UpdateContainerContract updateContract);

        Task<IList<ContainerTypeViewModel>> GetActualContainerTypesAsync();

        Task<IList<ContainerTypeViewModel>> GetContainerTypesByIdsAsync(IList<int> containerTypeIds);

        Task<IList<(int StandId, int ContainerId)>> GetFilteredContainerIdsForRouteGroupedByStandsAsync(
            GetFilteredContainerIdsForRouteContract contract
        );

        Task<IList<(Container Container, ContainerType ContainerType)>> GetDropDownContainersAsync(GetDropDownContainersFilterModel filter);
    }
}
