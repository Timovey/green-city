using AuthContracts.DataContracts;
using AuthContracts.ViewModels;
using AutoMapper;
using ExternalApi.Interface.Models.Auth;
using System;

namespace ExternalApiService.MappingProfiles
{
    internal class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<ExtAuthRequest, AuthUserContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<AuthUserResult, ExtAuthResult>();

            CreateMap<ExtAuthV2Request, AuthUserV2Contract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<AuthUserV2Result, ExtAuthV2Result>();

            CreateMap<ExtGetOrgsRequest, GetOrgsContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<OrganizationApiViewModel, ExtOrganizationViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.OrganizationId));
        }
    }
}
