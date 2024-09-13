using AutoMapper;
using ExternalApi.Interface.Models.Dispatcher.AuxiliaryWorker;
using System;
using DispatcherService.Contracts.DataContracts.AuxiliaryWorker;
using DispatcherService.Contracts.Filters;
using DispatcherService.Contracts.SortModels;
using DispatcherService.Contracts.ViewModels;
using DispatcherService.Contracts.DataContracts.Brigade;
using ExternalApi.Interface.Models.Dispatcher.Brigade;
using ExternalApi.Interface.Models.Dispatcher.TravelSheet;
using DispatcherService.Contracts.DataContracts.TravelSheet;
using ReportService.Contracts.DataContracts;
using ExternalApi.Interface.Models.Dispatcher.ShiftTask;
using DispatcherService.Contracts.DataContracts.ShiftTask;
using MissionService.Contracts.Enums;
using ExternalApi.Interface.Models.Sd.Container;
using ExternalApi.Interface.Models.Dispatcher.Mission;
using MissionService.Contracts.DataContracts.Mission;
using ExternalApi.Interface.Models.Dispatcher.Route;
using MissionService.Contracts.ViewModels.Mission;
using ExternalApiService.Extensions;
using ExternalApi.Interface.Models.Mission.App;

namespace ExternalApiService.MappingProfiles
{
    public class DispatcherMappingProfile : Profile
    {
        public DispatcherMappingProfile()
        {
            #region [ Common ]

            CreateMap<MissionTrashType, ExtTrashType>();
            CreateMap<ExtMissionStatus, MissionStatus>();

            #endregion

            #region [ AuxiliaryWorker ]

            CreateMap<ExtCreateAuxiliaryWorkerRequest, CreateAuxiliaryWorkerContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtUpdateAuxiliaryWorkerRequest, UpdateAuxiliaryWorkerContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteAuxiliaryWorkerRequest, DeleteAuxiliaryWorkerContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetAuxiliaryWorkerRequest, GetAuxiliaryWorkerContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtAuxiliaryWorkerSortingModel, AuxiliaryWorkerSortModel>();
            CreateMap<ExtAuxiliaryWorkerFilterRequest, AuxiliaryWorkerFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<AuxiliaryWorkerViewModel, ExtAuxiliaryWorkerViewModel>();

            #endregion

            #region [ Brigade ]

            CreateMap<ExtCreateBrigadeRequest, CreateBrigadeContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()));

            CreateMap<ExtCreateBrigadeRequest, ExtCreateBrigadeResponse>();

            CreateMap<ExtCommonBrigadesFilterRequest, CommonBrigadesFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()));
            CreateMap<CommonBrigadeViewModel, ExtCommonBrigadeViewModel>();

            CreateMap<ExtDeleteBrigadesRequest, DeleteBrigadesContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));

            CreateMap<ExtGetAvailableRoutesForCreateBrigadeRequest, GetAvailableRoutesForCreateBrigadeContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()));
            CreateMap<AvailableRoutesForCreateBrigadeViewModel, ExtAvailableRouteForCreateBrigadeViewModel>()
                .ForMember(x => x.Name, x => x.MapFrom(m => m.RouteName));

            #endregion

            #region [ TravelSheet ]

            CreateMap<ExtCreateTravelSheetRequest, CreateTravelSheetContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()));
            CreateMap<ExtCreateTravelSheetRequest, ExtCreateTravelSheetResponse>();

            CreateMap<TravelSheetForFrontViewModel, ExtTravelSheetForFrontViewModel>()
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetStringDateTime()));

            CreateMap<ExtCommonTravelSheetFilterRequest, CommonTravelSheetFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<CommonTravelSheetViewModel, ExtCommonTravelSheetViewModel>();

            CreateMap<ExtDeleteTravelSheetsRequest, DeleteTravelSheetsContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));

            CreateMap<ExtFixTravelSheetDriveTimesRequest, FixTravelSheetDriveTimesContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.TravelSheetId, x => x.MapFrom(m => m.Id));
            CreateMap<ExtFixTravelSheetDriveTimesRequest, ExtFixTravelSheetDriveTimesResponse>();

            CreateMap<TravelSheetsReportContract, CreateTravelSheetsReportContract>();

            #endregion

            #region [ ShiftTask ]

            CreateMap<ExtCreateShiftTaskRequest, CreateShiftTaskContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()));
            CreateMap<ExtCreateShiftTaskRequest, ExtCreateShiftTaskResponse>();

            CreateMap<ExtCommonShiftTasksFilterRequest, CommonShiftTasksFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()));
            CreateMap<CommonShiftTaskViewModel, ExtCommonShiftTaskViewModel>();

            CreateMap<ExtDeleteShiftTasksRequest, DeleteShiftTasksContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));

            CreateMap<ExtGetAvailableRoutesForCreateShiftTaskRequest, GetAvailableRoutesForCreateShiftTaskContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()));
            CreateMap<AvailableRouteForCreateShiftTaskViewModel, ExtAvailableRouteForCreateShiftTaskViewModel>()
                .ForMember(x => x.Name, x => x.MapFrom(m => m.RouteName));

            CreateMap<ExtGetOrgsDropDownForCreateShiftTaskRequest, GetOrgsDropDownForCreateShiftTaskContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<OrgsDropDownForCreateShiftTaskViewModel, ExtOrgsDropDownForCreateShiftTaskViewModel>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.OrgId))
                .ForMember(x => x.Name, x => x.MapFrom(m => m.OrgName));

            #endregion

            #region [ Mission ]

            CreateMap<ExtUpdateMissionsForCloseRouteRequest, UpdateMissionsForCloseRouteContract>()
                .ForMember(x => x.UpdateContracts, x => x.MapFrom(m => m.Missions))
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.RouteId, x => x.MapFrom(m => m.Id));
            CreateMap<ExtFrontMissionUpdateModel, UpdateOneMissionForCloseRouteContract>()
                .ForMember(x => x.MissionId, x => x.MapFrom(m => m.Id))
                .ForMember(x => x.MissionStatus, x => x.MapFrom(m => m.Status));

            CreateMap<ExtUpdateMissionsForCloseRouteRequest, ExtUpdateMissionsForCloseRouteResponse>();
            CreateMap<ExtFrontMissionUpdateModel, ExtFrontMissionUpdateModel>();

            CreateMap<MissionsForDispatcherViewModel, ExtMissionsForDispatcherViewModel>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.RouteId))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.RouteDate.GetStringDateTime()));
                
            CreateMap<CommonMissionViewModel, ExtCommonDispatcherMissionViewModel>();

            #endregion
        }
    }
}
