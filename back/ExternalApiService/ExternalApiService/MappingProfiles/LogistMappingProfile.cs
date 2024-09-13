using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExternalApi.Interface.Models.Logist.BaseRoute;
using ExternalApi.Interface.Models.Logist.Route;
using ExternalApiService.Extensions;
using LogistService.Contracts.DataContracts.BaseRoute;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Filters;
using LogistService.Contracts.SortModels;
using LogistService.Contracts.ViewModels;

namespace ExternalApiService.MappingProfiles
{
    public class LogistMappingProfile : Profile
    {
        public LogistMappingProfile()
        {
            //----------------------------- BaseRoute
            CreateMap<ExtCreateBaseRouteRequest, CreateBaseRouteContract>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtBaseRouteSortModel, BaseRouteSortModel>();
            CreateMap<ExtBaseRouteFilterRequest, BaseRouteFilterModel>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(m => Guid.NewGuid()));
            CreateMap<BaseRouteViewModel, ExtBaseRouteViewModel>();
            CreateMap<ExtGetBaseRouteRequest, GetBaseRouteContract>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(m => Guid.NewGuid()))
                .ForMember(dest => dest.Ids, opt => opt.MapFrom(m => new List<int> { m.Id }));
            CreateMap<BaseRouteViewModel, ExtBaseRouteWithStandsViewModel>()
                .ForMember(dest => dest.Stands, opt => opt.MapFrom(m => m.Stands.Select(s => s.Item2).ToList()));
            CreateMap<ExtUpdateBaseRouteRequest, UpdateBaseRouteContract>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteBaseRouteRequest, DeleteBaseRouteContract>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(m => Guid.NewGuid()))
                .ForMember(dest => dest.Ids, opt => opt.MapFrom(m => new List<int> { m.Id }));
            CreateMap<ExtDropdownBaseRouteFilterRequest, BaseRouteFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.Sorting, x => x.MapFrom(m => new BaseRouteSortModel()));

            //----------------------------- Route
            CreateMap<ExtCreateRouteRequest, CreateRouteContract>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()));
            CreateMap<RouteViewModel, ExtRouteViewModel>()
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetStringDateTime()));
            CreateMap<ExtRouteSortModel, RouteSortModel>();
            CreateMap<ExtRouteFilterRequest, RouteFilterModel>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(m => Guid.NewGuid()))
                .ForMember(x => x.Date, x => x.MapFrom(m => m.Date.GetDateTimeOrDefault()));
            CreateMap<FilteredRouteViewModel, ExtFilteredRouteViewModel>();
            CreateMap<ExtDeleteRouteRequest, DeleteRouteContract>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetRouteRequest, GetRouteContract>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(m => Guid.NewGuid()))
                .ForMember(dest => dest.Ids, opt => opt.MapFrom(m => new List<int> { m.Id }));
            CreateMap<ExtUpdateRouteRequest, UpdateRouteContract>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(m => Guid.NewGuid()));
        }
    }
}