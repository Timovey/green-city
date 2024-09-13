using AuthContracts.DataContracts;
using AutoMapper;
using DispatcherService.Contracts.DataContracts.ShiftTask;
using TransportContracts.DataContracts.TransportWorkShift;

namespace DispatcherService.Core.MappingProfile
{
    public class ShiftTaskMappingProfile : Profile
    {
        public ShiftTaskMappingProfile()
        {
            CreateMap<GetOrgsDropDownForCreateShiftTaskContract, GetWorkShiftsByIdsContract>()
                .ForMember(x => x.IsFilterByUser, opt => opt.MapFrom(m => false));

            CreateMap<GetOrgsDropDownForCreateShiftTaskContract, GetOrgsByIdsContract>()
                .ForMember(x => x.IsFilterByUserId, opt => opt.MapFrom(m => false));
        }
    }
}
