using AutoMapper;
using ScheduleService.Contracts.DataContracts.DateRange;
using ScheduleService.Contracts.DataContracts.RedDates;
using ScheduleService.Contracts.DataContracts.WeekendWorkDates;
using ScheduleService.Contracts.ViewModels;
using ScheduleService.Contracts.ViewModels.RedDates;
using ScheduleService.Contracts.ViewModels.WeekendWorkDates;
using ScheduleService.Database.Models;

namespace ScheduleService.Database.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //----------------------------- Red date
            CreateMap<CreateRedDateContract, RedDate>()
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(x => x.Date.Date));
            CreateMap<RedDate, RedDateViewModel>();

            //----------------------------- Weekend work date
            CreateMap<CreateWeekendWorkDateContract, WeekendWorkDate>()
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(x => x.Date.Date)); ;
            CreateMap<WeekendWorkDate, WeekendWorkDateViewModel>();

            //----------------------------- DateRange
            CreateMap<BaseDateRangeModel, DateRangeViewModel>();
        }
    }
}
