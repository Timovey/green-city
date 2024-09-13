using AutoMapper;
using ScheduleService.Contracts.DataContracts.DateRange;
using ScheduleService.Contracts.ViewModels;
using TransportContracts.DateRange;
using TransportContracts.ViewModels;
using TransportService.Core.Helpers;

namespace TransportService.Core.MappingProfiles
{
    public class ContractsMappingProfile : Profile
    {
        public ContractsMappingProfile()
        {
            //----------------------------- Transport
            CreateMap<DateRangeBaseModel, BaseDateRangeModel>();
            CreateMap<DateRangeViewModel, DateRangeBaseModel>();

            CreateMap<TransportWorkShiftViewModel, TeamHelperModel>();
            CreateMap<TeamHelperModel, TransportWorkShiftViewModel>();
        }
    }
}
