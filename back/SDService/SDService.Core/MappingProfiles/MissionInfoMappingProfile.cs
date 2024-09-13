using AutoMapper;
using SDContracts.ViewModels.MissionInfo;
using SDService.Database.Models.ViewModels;

namespace SDService.Core.MappingProfiles
{
    public class MissionInfoMappingProfile : Profile
    {
        public MissionInfoMappingProfile()
        {
            #region [ MissionInfo ]

            CreateMap<InfoForMissionStandModel, MissionStandInfoViewModel>();

            #endregion
        }
    }
}
