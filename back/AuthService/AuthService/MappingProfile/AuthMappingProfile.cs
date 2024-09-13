using AuthContracts.ViewModels;
using AuthService.Models.Db;
using AuthService.Models.Orgs;
using AuthService.Models.UserToOrgs;
using AuthService.ViewModels.Orgs;
using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using System.Collections.Generic;

namespace AuthService.MappingProfile
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            #region orgs

            CreateMap<CreateOrgModel, DbOrganization>();
            CreateMap<DbOrganization, DbOrganizationHistory>()
                .ForMember(x => x.Id,
                    opt => opt.Ignore());
            CreateMap<DbOrganization, OrganizationViewModel>()
                .ForMember(dest => dest.OrganizationId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<UpdateOrgModel, DbOrganization>();
            CreateMap<DbOrganization, DbOrganization>();
            CreateMap<OrganizationViewModel, OrganizationApiViewModel>();

            #endregion

            #region UserToOrgs

            CreateMap<DbUserToOrgs, DbUserToOrgsHistory>()
                .ForMember(x => x.Id,
                    opt => opt.Ignore());
            CreateMap<CreateUserToOrgsModel, DbUserToOrgs>()
                .ForMember(dest => dest.OrgIdsJson,
                    opt => opt.MapFrom(x => x.OrgIds.SafeSerialize(null)));
            CreateMap<DbUserToOrgs, UserToOrgsViewModel>()
                .ForMember(dest => dest.UserToOrgsId,
                    opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.OrgIds,
                    opt => opt.MapFrom(x => x.OrgIdsJson.SafeDeserialize<IList<int>>(null)));
            CreateMap<UpdateUserToOrgsModel, DbUserToOrgs>()
                .ForMember(dest => dest.OrgIdsJson,
                    opt => opt.MapFrom(x => x.OrgIds.SafeSerialize(null)));
            CreateMap<OrganizationViewModel, EditOrgViewModel>();
            CreateMap<EditOrgViewModel, UpdateOrgModel>()
                .ForMember(dest => dest.OrganizationIds,
                    opt => opt.MapFrom(x => new List<int> { x.OrganizationId }));

            #endregion
        }
    }
}
