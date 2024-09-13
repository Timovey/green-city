using AutoMapper;
using MissionService.Contracts.DataContracts.App;
using MissionService.Contracts.DataContracts.Mission;
using MissionService.Contracts.Enums;
using MissionService.Database.Models.Contracts;
using ScheduleService.Contracts.DataContracts.ContractSchedule;
using SDContracts.DataContracts.Contract.ScheduleModels;
using SDContracts.DataContracts.MissionInfo;
using SDContracts.ViewModels.ContractSchedule;

namespace MissionService.Core.MappingProfile
{
    /// <summary>
    /// Класс конфигурации маппера
    /// </summary>
    public class BusinessLogicMappingProfile : Profile
    {
        public BusinessLogicMappingProfile()
        {
            #region [ ContractSchedule ]

            CreateMap<ContractScheduleViewModel, SSContractScheduleModel>();
            CreateMap<StepInitialDateScheduleViewModel, SSStepInitialDateSchedule>();
            CreateMap<ContractScheduleSettingsViewModel, SSContractScheduleSettings>();

            CreateMap<SSContractScheduleModel, ContractScheduleViewModel>();
            CreateMap<SSStepInitialDateSchedule, StepInitialDateScheduleViewModel>();
            CreateMap<SSContractScheduleSettings, ContractScheduleSettingsViewModel>();
            CreateMap<NotWorkDayReactionType, SSNotWorkDayReactionType>();

            #endregion

            #region [ Missions ]

            CreateMap<CreateRealMissionsContract, CreateOneRealMissionContract>();
            CreateMap<CreateMissionFromMapContract, CreateOneRealMissionContract>();

            CreateMap<CreateAppFromMapContract, CreateFakeDataForMissionInfoContract>();

            #endregion

            #region [ Apps ]

            CreateMap<CreateAppByContractClientContract, CreateAppDbContract>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(x => AppType.ByContractClient))
                .ForMember(dest => dest.CreatedUserId,
                    opt => opt.MapFrom(x => x.UserId))
                .ForMember(dest => dest.CreatedUserName,
                    opt => opt.MapFrom(x => x.UserName));

            #endregion
        }
    }
}
