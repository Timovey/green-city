using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using ScheduleService.Contracts.DataContracts.ContractSchedule;
using ScheduleService.Contracts.DataContracts.ContractSchedule.Mission;
using SDContracts.DataContracts.Container;
using SDContracts.DataContracts.Contract;
using SDContracts.DataContracts.Contract.ScheduleModels;
using SDContracts.DataContracts.Contract.ScheduleModels.Mission;
using SDContracts.DataContracts.ContractClient;
using SDContracts.DataContracts.Stand;
using SDContracts.SpecificModels.ContainerStand;
using SDContracts.ViewModels;
using SDContracts.ViewModels.Container;
using SDContracts.ViewModels.ContractSchedule;
using SDContracts.ViewModels.ContractSchedule.Mission;
using SDContracts.ViewModels.Stand;
using SDService.Database.Extensions;
using SDService.Database.JsonModels.Contract.ContractSchedule;
using SDService.Database.Models.DbModels;
using SDService.Database.Models.DbModels.ContractSchedule;
using SDService.Database.Models.ViewModels;
using System.Collections.Generic;

namespace SDService.Database.MappingProfile
{
    /// <summary>
    /// Класс конфигурации маппера
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region [ Contract Client ]
            CreateMap<CreateContractClientContract, ContractClient>()
                .ForMember(dest => dest.CreatedUserId,
                    opt => opt.MapFrom(x => x.UserId))
                .ForMember(dest => dest.CreatedUserName,
                    opt => opt.MapFrom(x => x.UserName));
            CreateMap<UpdateContractClientContract, ContractClient>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(x => x.ContractClientId));
            CreateMap<ContractClient, ContractClientViewModel>()
                .ForMember(dest => dest.ContractClientId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<ContractClient, ContractClient>()
                .ForMember(dest => dest.CreatedUserId,
                    opt => { opt.UseDestinationValue(); opt.Ignore(); })
                .ForMember(dest => dest.CreatedUserName,
                    opt => { opt.UseDestinationValue(); opt.Ignore(); });
            CreateMap<ContractClient, ContractContractClientViewModel>()
                .ForMember(dest => dest.ContractClientId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<ContractClient, ContractClientHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            #endregion

            #region [ Stand ]
            CreateMap<CreateStandContract, Stand>()
                .ForMember(dest => dest.CreatedUserId,
                    opt => opt.MapFrom(x => x.UserId))
                .ForMember(dest => dest.CreatedUserName,
                    opt => opt.MapFrom(x => x.UserName));
            CreateMap<UpdateStandContract, Stand>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(x => x.StandId));
            CreateMap<Stand, StandViewModel>()
                .ForMember(dest => dest.Coordinates,
                    opt => opt.MapFrom(x => x.CoordinatesJson.SafeDeserialize<StandCoordinatesModel>(null)))
                .ForMember(dest => dest.StandId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<Stand, InfoForMissionStandModel>()
                .ForMember(dest => dest.Coordinates,
                    opt => opt.MapFrom(x => x.CoordinatesJson.SafeDeserialize<StandCoordinatesModel>(null)))
                .ForMember(dest => dest.StandId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<Stand, ContractStandViewModel>()
                .ForMember(dest => dest.StandId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<Stand, Stand>()
                .ForMember(dest => dest.CreatedUserId,
                    opt => { opt.UseDestinationValue(); opt.Ignore(); })
                .ForMember(dest => dest.CreatedUserName,
                    opt => { opt.UseDestinationValue(); opt.Ignore(); });
            CreateMap<Stand, StandHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            #endregion

            #region [ Contract ]

            CreateMap<CreateContractContract, Contract>()
                .ForMember(dest => dest.CreatedUserId,
                    opt => opt.MapFrom(x => x.UserId))
                .ForMember(dest => dest.CreatedUserName,
                    opt => opt.MapFrom(x => x.UserName));
            CreateMap<UpdateContractContract, Contract>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(x => x.ContractId));
            CreateMap<Contract, ContractViewModel>()
                .ForMember(dest => dest.ContractId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<Contract, Contract>()
                .ForMember(dest => dest.CreatedUserId,
                    opt => { opt.UseDestinationValue(); opt.Ignore(); })
                .ForMember(dest => dest.CreatedUserName,
                    opt => { opt.UseDestinationValue(); opt.Ignore(); });
            CreateMap<Contract, ContractHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            #endregion

            #region [ ContractSchedule ]

            CreateMap<ContractScheduleSettings, ContractScheduleSettingsViewModel>();
            CreateMap<StepInitialDateScheduleModel, StepInitialDateScheduleViewModel>();

            CreateMap<ContractScheduleViewModel, SSContractScheduleModel>();
            CreateMap<StepInitialDateScheduleViewModel, SSStepInitialDateSchedule>();
            CreateMap<ContractScheduleSettingsViewModel, SSContractScheduleSettings>();

            CreateMap<SSContractScheduleModel, ContractScheduleViewModel>();
            CreateMap<SSStepInitialDateSchedule, StepInitialDateScheduleViewModel>();
            CreateMap<SSContractScheduleSettings, ContractScheduleSettingsViewModel>();
            CreateMap<NotWorkDayReactionType, SSNotWorkDayReactionType>();

            #endregion

            #region [ Mission ]

            CreateMap<Container2ScheduleViewModel, Container2ScheduleInfoMissionViewModel>();
            CreateMap<ContractContainerViewModel, ContractContainerInfoMissionViewModel>();
            CreateMap<ContractStandViewModel, ContractStandMissionInfoViewModel>();
            CreateMap<ContractContractClientViewModel, ContractClientInfoMissionViewModel>();
            CreateMap<ContractViewModel, ContractScheduleMissionViewModel>();
            CreateMap<ContractScheduleCollectionViewModel, SSContractScheduleCollectionModel>()
                .ForMember(dest => dest.StartDate,
                    opt => opt.MapFrom(x => x.ScheduleDateFrom))
                .ForMember(dest => dest.EndDate,
                    opt => opt.MapFrom(x => x.ScheduleDateTo));

            CreateMap<CreateMissionContract, ContractScheduleMission>()
                .ForMember(dest => dest.CreatedUserId,
                    opt => opt.MapFrom(x => x.UserId))
                .ForMember(dest => dest.CreatedUserName,
                    opt => opt.MapFrom(x => x.UserName));

            #endregion

            #region [ Container ]

            CreateMap<CreateContainerContract, Container>()
                .ForMember(dest => dest.CreatedUserId,
                    opt => opt.MapFrom(x => x.UserId))
                .ForMember(dest => dest.CreatedUserName,
                    opt => opt.MapFrom(x => x.UserName));
            CreateMap<UpdateContainerContract, Container>();
            CreateMap<Container, ContainerViewModel>()
                .ForMember(dest => dest.ContainerId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<Container, ContainerDropDownViewModel>()
                .ForMember(dest => dest.ContainerName,
                    opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.ContainerId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<ContainerType, ContainerTypeViewModel>();
            CreateMap<Container, ContractContainerViewModel>()
                .ForMember(dest => dest.ContainerId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<Container, Container>()
                .ForMember(dest => dest.CreatedUserId,
                    opt => { opt.UseDestinationValue(); opt.Ignore(); })
                .ForMember(dest => dest.CreatedUserName,
                    opt => { opt.UseDestinationValue(); opt.Ignore(); });
            CreateMap<Container, ContainerHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            #endregion

            #region [ Container2Schedule ]

            CreateMap<CreateContainer2ScheduleContract, Container2Schedule>()
                .ForMember(dest => dest.ContractScheduleJson,
                    opt => opt.MapFrom(x => x.ContractScheduleCollection.ToJsonContractScheduleCollectionBulk().SafeSerialize(null)));
            CreateMap<Container2Schedule, Container2ScheduleViewModel>()
                .ForMember(dest => dest.ContractScheduleCollection,
                    opt => opt.MapFrom(x => x.ContractScheduleJson.SafeDeserialize<IList<ContractScheduleCollectionJsonModel>>(null).ToContractScheduleCollectionBulk()));
            CreateMap<Container2Schedule, Container2ScheduleForMissionsViewModel>()
                .ForMember(dest => dest.ContractScheduleCollection,
                    opt => opt.MapFrom(x => x.ContractScheduleJson.SafeDeserialize<IList<ContractScheduleCollectionJsonModel>>(null).ToContractScheduleCollectionBulk()));
            CreateMap<Container2Schedule, Container2ScheduleHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            #endregion
        }
    }
}