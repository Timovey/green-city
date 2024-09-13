using AutoMapper;
using DispatcherService.Contracts.DataContracts.DoneTaskList.Report;
using DispatcherService.Contracts.DataContracts.ShiftTask.Report;
using DispatcherService.Contracts.ViewModels;
using ReportService.Contracts.DataContracts;
using ReportService.Contracts.DataContracts.DoneTasksList;

namespace ExternalApiService.MappingProfiles
{
    internal class ReportMappingProfile : Profile
    {
        public ReportMappingProfile()
        {

            #region [ TravelSheet ]

            CreateMap<TravelSheetsReportContract, CreateTravelSheetsReportContract>();

            #endregion

            #region [ ShiftTask ]

            CreateMap<ShiftTaskReportModel, CreateShiftTaskReportContract>();
            CreateMap<ShiftTaskRouteReportModel, ShiftTaskRoute>();
            CreateMap<ShiftTaskStandReportModel, ShiftTaskStand>();
            CreateMap<ShiftTaskContainerReportModel, ShiftTaskContainer>();

            #endregion
            
            #region [ DoneTaskList ]

            CreateMap<DoneTaskListStandReportModel, DoneTaskListStandsInfo>();
            CreateMap<DoneTaskListRouteReportModel, DoneTaskListRouteInfo>();
            CreateMap<DoneTaskListReportModel, DoneTaskListContract>();
            
            #endregion
        }
    }
}
