using AutoMapper;
using ExternalApi.Interface.Models.Mission.App;
using ExternalApi.Interface.Models.Mission.Mission;
using ExternalApi.Interface.Models.Sd.Container;
using ExternalApiService.Extensions;
using MissionService.Contracts.DataContracts.App;
using MissionService.Contracts.Enums;
using MissionService.Contracts.Filters.App;
using MissionService.Contracts.Filters.Mission;
using MissionService.Contracts.ViewModels.App;
using MissionService.Contracts.ViewModels.Mission;
using System;

namespace ExternalApiService.MappingProfiles
{
    public class MissionMappingProfile : Profile
    {
        public MissionMappingProfile()
        {
            #region [ Missions ]

            CreateMap<ExtMissionStatus, MissionStatus>();
            CreateMap<ExtTrashType, MissionTrashType>();

            CreateMap<CommonMissionViewModel, ExtCommonMissionViewModel>();

            CreateMap<ExtCommonMissionFilterRequest, CommonMissionFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));

            #endregion

            #region [ Apps ]

            CreateMap<ExtContainerInAppInfo, ExtContainerInAppInfo>();

            CreateMap<AppFromMapViewModel, ExtAppFromMapViewModel>()
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetStringDateTime()));

            CreateMap<ContainerInAppInfo, ExtContainerInAppInfo>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.ContainerId))
                .ForMember(x => x.Name, x => x.MapFrom(m => m.ContainerName));
            CreateMap<AppByContractClientViewModel, ExtAppByContractClientViewModel>()
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetStringDateTime()));

            CreateMap<ExtCreatorType, CreatorType>();
            CreateMap<ExtAppType, AppType>();
            CreateMap<ExtAppComputedStatus, AppComputedStatus>();

            CreateMap<ExtCommonAppFilterRequest, CommonAppFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.AppDate, x => x.MapFrom(m => m.AppDate.GetDateTimeOrDefault()));
            CreateMap<AppCommonViewModel, ExtAppCommonViewModel>();

            CreateMap<ExtCreateAppByContractClientRequest, CreateAppByContractClientContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()));
            CreateMap<ExtCreateAppByContractClientRequest, ExtCreateAppByContractClientResponse>();

            CreateMap<ExtCreateAppFromMapRequest, CreateAppFromMapContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                //TODO Выпилить это
                .ForMember(x => x.ContractClientId, x => x.MapFrom(m => m.ContractClientId == 0 ? default : m.ContractClientId))
                .ForMember(x => x.ContractId, x => x.MapFrom(m => m.ContractId == 0 ? default : m.ContractClientId))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()));
            CreateMap<ExtCreateAppFromMapRequest, ExtCreateAppFromMapResponse>();

            CreateMap<ExtUpdateAppByContractClientRequest, UpdateAppByContractClient>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.AppId, x => x.MapFrom(m => m.Id))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()));
            CreateMap<ExtUpdateAppByContractClientRequest, ExtUpdateAppByContractClientResponse>();

            CreateMap<ExtDeleteAppsRequest, DeleteAppsContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));

            CreateMap<ExtRunAppGenerationInSingleThreadRequest, RunAppGenerationInSingleThreadContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));

            #endregion
        }
    }
}
