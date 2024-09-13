using System.Collections.Generic;
using AutoMapper;
using LogistService.Contracts.DataContracts.BaseRoute;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Filters;
using LogistService.Contracts.ViewModels;
using LogistService.Database.Models;
using LogistService.Database.Models.Contracts;
using MissionService.Contracts.DataContracts.Mission;
using TransportContracts.DataContracts.TransportWorkShift;

namespace LogistService.Database.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //----------------------------- BaseRoute
            CreateMap<CreateBaseRouteContract, CreateDbBaseRouteContract>()
                .ForMember(dest => dest.CreatedUserId, opt => opt.MapFrom(x => x.UserId))
                .ForMember(dest => dest.CreatedUserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.StandIdsWithPriorities, opt => opt.Ignore());

            CreateMap<CreateDbBaseRouteContract, BaseRoute>();

            CreateMap<BaseRoute, BaseRouteHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<BaseRoute, BaseRouteViewModel>()
                .ForMember(dest => dest.Stands, opt => opt.Ignore());

            CreateMap<UpdateBaseRouteContract, UpdateDbBaseRouteContract>()
                .ForMember(dest => dest.CreatedUserId, opt => opt.MapFrom(x => x.UserId))
                .ForMember(dest => dest.CreatedUserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.StandIdsWithPriorities, opt => opt.Ignore());

            CreateMap<UpdateDbBaseRouteContract, BaseRoute>();
            CreateMap<BaseRoute, BaseRoute>();

            //----------------------------- BaseRoute2Stand
            CreateMap<BaseRoute2Stand, BaseRoute2StandHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            //----------------------------- Route
            CreateMap<CreateRouteContract, Route>()
                .ForMember(dest => dest.RouteName, opt => opt.MapFrom(m => "Рейс"));

            CreateMap<Route, RouteHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Route, RouteViewModel>()
                .ForMember(dest => dest.RouteName, opt => opt.MapFrom(x => $"{x.RouteName} {x.RouteNumberOnDay}"));

            CreateMap<Route, FilteredRouteViewModel>()
                .ForMember(dest => dest.RouteName, opt => opt.MapFrom(x => $"{x.RouteName} {x.RouteNumberOnDay}"));

            CreateMap<RouteViewModel, BindMissionsToRouteByStandsContract>()
                .ForMember(dest => dest.RouteId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.RouteDate, opt => opt.MapFrom(x => x.Date));

            CreateMap<RouteFilterModel, GetActualWorkShitsContract>()
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(x => x.Date.Date))
                .ForMember(dest => dest.IsFilterByUser, opt => opt.MapFrom(x => false));

            CreateMap<UpdateRouteContract, Route>();

            CreateMap<RouteViewModel, UpdateMissionBindingsByRouteIdContract>()
                .ForMember(dest => dest.RouteId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.RouteDate, opt => opt.MapFrom(x => x.Date.Date));

            CreateMap<CloseRouteContract, Route>()
                .ForMember(dest => dest.RouteCompleted, opt => opt.MapFrom(x => true));
            
            CreateMap<Route, GetRoutesByWorkShiftsIdsViewModel>()
                .ForMember(dest => dest.RouteName, opt => opt.MapFrom(x => $"{x.RouteName} {x.RouteNumberOnDay}"));
        }
    }
}