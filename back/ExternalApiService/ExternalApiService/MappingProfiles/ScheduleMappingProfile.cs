using AutoMapper;
using ExternalApi.Interface.Models.Common.Cron;
using ExternalApi.Interface.Models.Sd.Contract.ScheduleModels;
using GreenCity.CommonUtils.CommonExtensions;
using ScheduleService.Contracts.DataContracts.ContractSchedule;
using System;

namespace ExternalApiService.MappingProfiles
{
    internal class ScheduleMappingProfile : Profile
    {
        public ScheduleMappingProfile()
        {
            CreateMap<ExtGetCronNextFireDatesRequest, GetContractScheduleContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtContractScheduleModel, SSContractScheduleModel>();
            CreateMap<ExtContractScheduleSerializedModel, SSContractScheduleSerializedModel>();
            CreateMap<ExtStepInitialDateSchedule, SSStepInitialDateSchedule>()
                .ForMember(dest => dest.StepValue,
                    opt => opt.MapFrom(x => GetNullableInt(x.StepValue)));
            CreateMap<ExtContractScheduleSettings, SSContractScheduleSettings>();
            CreateMap<ExtNotWorkDayReactionType, SSNotWorkDayReactionType>();
        }

        private int? GetNullableInt(string str)
        {
            if (str.IsNullOrEmpty())
                return null;

            try
            {
                return int.Parse(str);
            }
            catch
            {
                return null;
            }
        }
    }
}
