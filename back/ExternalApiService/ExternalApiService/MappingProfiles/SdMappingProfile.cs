using AutoMapper;
using ExternalApi.Interface.Models.Sd.Container;
using ExternalApi.Interface.Models.Sd.Contract;
using ExternalApi.Interface.Models.Sd.Contract.ContractScheduleViewModels;
using ExternalApi.Interface.Models.Sd.Contract.ScheduleModels;
using ExternalApi.Interface.Models.Sd.ContractClient;
using ExternalApi.Interface.Models.Sd.Mission;
using ExternalApi.Interface.Models.Sd.Stand;
using ExternalApiService.EnumConverters;
using ExternalApiService.Extensions;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.CommonUtils.CommonModels;
using SDContracts.DataContracts.Container;
using SDContracts.DataContracts.Contract;
using SDContracts.DataContracts.Contract.ScheduleModels;
using SDContracts.DataContracts.Contract.ScheduleModels.Mission;
using SDContracts.DataContracts.ContractClient;
using SDContracts.DataContracts.Stand;
using SDContracts.Enums;
using SDContracts.Filter;
using SDContracts.Filter.DropDown;
using SDContracts.SortModels;
using SDContracts.SpecificModels.ContainerStand;
using SDContracts.ViewModels;
using SDContracts.ViewModels.Container;
using SDContracts.ViewModels.Container.ContractClient;
using SDContracts.ViewModels.Contract;
using SDContracts.ViewModels.ContractSchedule;
using SDContracts.ViewModels.ContractSchedule.Mission;
using SDContracts.ViewModels.Stand;
using System;
using System.Linq;

namespace ExternalApiService.MappingProfiles
{
    internal class SdMappingProfile : Profile
    {
        public SdMappingProfile()
        {
            #region [ Common ]

            CreateMap<SortModel, SortModel>();

            #endregion

            #region [ Contract Client ]

            CreateMap<ExtCreateContractClientRequest, CreateContractClientContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtUpdateContractClientRequest, UpdateContractClientContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteContractClientRequest, DeleteContractClientContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetContractClientRequest, GetContractClientContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtContractClientSortingModel, ContractClientSortModel>();
            CreateMap<ExtContractClientFilterRequest, ContractClientFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ContractClientViewModel, ExtContractClientViewModel>();
            CreateMap<ExtContractContractClientViewModel, ContractContractClientViewModel>();
            CreateMap<ContractContractClientViewModel, ExtContractContractClientViewModel>();
            CreateMap<ContractClientViewModel, ExtContractClientDropDownViewModel>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.ContractClientId))
                .ForMember(x => x.Name, x => x.MapFrom(m => m.ClientName));

            #endregion

            #region [ Container ]

            CreateMap<ExtTrashType, TrashType>();
            CreateMap<TrashType, ExtTrashType>();
            CreateMap<ExtContainerDropDownViewModel, ContainerDropDownViewModel>();
            CreateMap<ContainerDropDownViewModel, ExtContainerDropDownViewModel>();
            CreateMap<ExtCreateContainerRequest, CreateContainerContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtUpdateContainerRequest, UpdateContainerContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteContainerRequest, DeleteContainerContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtContainerSortingModel, ContainerSortModel>();
            CreateMap<ExtGetContainerRequest, GetContainerContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtContainerFilterRequest, ContainerFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ContainerViewModel, ExtContainerViewModel>()
                .AfterMap((src, dest) => dest.TrashTypeName = dest.TrashType.ToExtTrashTypeName());
            CreateMap<ExtContractContainerViewModel, ContractContainerViewModel>();
            CreateMap<ContractContainerViewModel, ExtContractContainerViewModel>();
            CreateMap<ContainerTypeViewModel, ExtContainerTypeViewModel>();

            CreateMap<ExtGetFilteredContainerIdsForRouteRequest, GetFilteredContainerIdsForRouteContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));

            #endregion

            #region [ Stand ]

            CreateMap<ExtStandType, StandType>();
            CreateMap<StandType, ExtStandType>();
            CreateMap<ExtStandCoordinatesModel, StandCoordinatesModel>();
            CreateMap<StandCoordinatesModel, ExtStandCoordinatesModel>();
            CreateMap<ExtCreateStandRequest, CreateStandContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtUpdateStandRequest, UpdateStandContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteStandRequest, DeleteStandContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtStandSortingModel, StandSortModel>();
            CreateMap<ExtGetStandRequest, GetStandContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtStandFilterRequest, StandFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<StandViewModel, ExtStandViewModel>()
                .AfterMap((src, dest) => dest.StandTypeName = dest.StandType.ToExtStandTypeName());
            CreateMap<StandViewModel, ExtOneStandViewModel>()
                .ForMember(dest => dest.AttachedContainerIds, opt => opt.MapFrom(m => m.AttachedContainers.Select(x => x.ContainerId).Distinct().ToList()))
                .AfterMap((src, dest) => dest.StandTypeName = dest.StandType.ToExtStandTypeName());
            CreateMap<ExtStandViewModel, StandViewModel>();
            CreateMap<ExtStandForBaseRouteFilterRequest, StandFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<StandViewModel, ExtStandForBaseRouteViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.StandId))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(s => s.Coordinates.Coordinates.FirstOrDefault().Longitude))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(s => s.Coordinates.Coordinates.FirstOrDefault().Latitude))
                .ForMember(dest => dest.CoordinatesComment, opt => opt.MapFrom(s => s.Coordinates.CoordinatesComment));
            CreateMap<ExtStandForBaseRouteViewModel, StandViewModel>();
            CreateMap<ExtContractStandViewModel, ContractStandViewModel>();
            CreateMap<ContractStandViewModel, ExtContractStandViewModel>();

            CreateMap<ExtGetFilteredStandsForRouteFilterRequest, GetFilteredStandsForRouteFilterModel>()
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()))
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.IncludeContainersRouteId, x => x.MapFrom(m => m.RouteId));
            CreateMap<StandForRouteViewModel, ExtStandForRouteViewModel>();

            CreateMap<ExtGetStandsForRouteByRouteIdRequest, GetStandsForRouteByRouteIdContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));

            #endregion

            #region [ Contract ]

            CreateMap<ExtCreateContractRequest, CreateContractContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtUpdateContractRequest, UpdateContractContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtBulkUpdateContractRequest, BulkUpdateContractContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteContractRequest, DeleteContractContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ContractViewModel, ExtContractViewModel>();
            CreateMap<Container2ScheduleViewModel, ExtContainer2ScheduleViewModel>()
                .ForMember(dest => dest.InRegistryFromDate,
                    opt => opt.MapFrom(x => x.InRegistryFromDate.HasValue ? x.InRegistryFromDate : default(DateTime?)))
                .ForMember(dest => dest.WorkTimeFrom,
                    opt => opt.MapFrom(x => x.WorkTimeFrom.ToString(@"hh\:mm")))
                .ForMember(dest => dest.WorkTimeTo,
                    opt => opt.MapFrom(x => x.WorkTimeTo.ToString(@"hh\:mm")));
            CreateMap<ExtContractSortModel, ContractSortModel>();
            CreateMap<ExtGetContractRequest, GetContractContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtContractClientFilterRequest, ContractFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtContractSortModel, ContractSortModel>();
            CreateMap<ExtCreateContainer2ScheduleRequest, CreateContainer2ScheduleContract>()
                .ForMember(dest => dest.InRegistryFromDate,
                    opt => opt.MapFrom(x => GetDateTimeFromString(x.InRegistryFromDate)))
                .ForMember(dest => dest.WorkTimeFrom,
                    opt => opt.MapFrom(x => GetTimeSpanFromString(x.WorkTimeFrom, "00:00")))
                .ForMember(dest => dest.WorkTimeTo,
                    opt => opt.MapFrom(x => GetTimeSpanFromString(x.WorkTimeFrom, "23:59")));
            CreateMap<ExtContractFilterRequest, ContractFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetDropDownContractsFilterRequest, ContractFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.Sorting, x => x.MapFrom(m => new ContractSortModel()));
            CreateMap<ContractViewModel, ExtContractDropDownViewModel>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.ContractId));

            #endregion

            #region [ ContractSchedule ]

            CreateMap<ExtNotWorkDayReactionType, NotWorkDayReactionType>();
            CreateMap<NotWorkDayReactionType, ExtNotWorkDayReactionType>();

            CreateMap<ExtContractScheduleCollectionModel, ContractScheduleCollectionModel>();
            CreateMap<ExtContractScheduleModel, ContractScheduleModel>();
            CreateMap<ExtContractScheduleSerializedModel, ContractScheduleSerializedModel>();
            CreateMap<ExtContractScheduleSettings, ContractScheduleSettings>();
            CreateMap<ExtStepInitialDateSchedule, StepInitialDateScheduleModel>()
                .ForMember(dest => dest.StepValue,
                    opt => opt.MapFrom(x => GetNullableInt(x.StepValue)));

            CreateMap<ContractScheduleCollectionViewModel, ExtContractScheduleCollectionViewModel>();
            CreateMap<ContractScheduleViewModel, ExtContractScheduleViewModel>();
            CreateMap<ContractScheduleSettingsViewModel, ExtContractScheduleSettingsViewModel>();
            CreateMap<StepInitialDateScheduleViewModel, ExtStepInitialDateScheduleViewModel>()
                 .ForMember(dest => dest.StepValue,
                      opt => opt.MapFrom(x => GetString(x.StepValue)));

            #endregion

            #region [ Mission ]

            CreateMap<ExtShowMissionsRequest, ShowMissionsContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));

            CreateMap<Container2ScheduleInfoMissionViewModel, ExtContainer2ScheduleInfoMissionViewModel>();
            CreateMap<ContractClientInfoMissionViewModel, ExtContractClientInfoMissionViewModel>();
            CreateMap<ContractContainerInfoMissionViewModel, ExtContractContainerInfoMissionViewModel>();
            CreateMap<ContractScheduleMissionViewModel, ExtContractScheduleMissionViewModel>();
            CreateMap<ContractStandMissionInfoViewModel, ExtContractStandMissionInfoViewModel>();

            #endregion

            #region [ DropDowns ]

            CreateMap<ExtGetDropDownContractClientFilterRequest, GetDropDownContractClientFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ContractClientDropDownViewModel, ExtContractClientDropDownViewModel>();

            CreateMap<ExtGetDropDownContractClientFilterRequest, ContractClientFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.ClientName, x => x.MapFrom(m => m.Name));

            CreateMap<ExtGetDropDownContainersFilterRequest, GetDropDownContainersFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ContainerDropDownV2ViewModel, ExtContainerDropDownV2ViewModel>();

            CreateMap<ExtGetDropDownStandsFilterRequest, GetDropDownStandsFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<StandDropDownViewModel, ExtStandDropDownViewModel>();

            CreateMap<ExtGetDropDownContractsFilterRequest, GetDropDownContractsFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ContractDropDownViewModel, ExtContractDropDownViewModel>();

            #endregion
        }

        private int? GetNullableInt(string str)
        {
            if (str.IsNullOrEmpty())
                return null;

            try
            {
                return int.Parse(str);
            }
            catch
            {
                return null;
            }
        }

        private string GetString(int? number)
        {
            if (!number.HasValue)
                return null;

            return number.Value.ToString();
        }

        private TimeSpan GetTimeSpanFromString(string timeSpan, string defaultString)
        {
            try
            {
                return timeSpan.IsNullOrEmpty() ? TimeSpan.Parse(defaultString) : TimeSpan.Parse(timeSpan);
            }
            catch
            {
                return TimeSpan.Parse("00:00");
            }
        }

        private DateTime? GetDateTimeFromString(string date)
        {
            if (date.IsNullOrEmpty())
                return null;

            try
            {
                return DateTime.Parse(date);
            }
            catch
            {
                return null;
            }
        }
    }
}
