using AutoMapper;
using DispatcherService.Contracts.DataContracts.AuxiliaryWorker;
using DispatcherService.Contracts.ViewModels;
using DispatcherService.Database.Models;

namespace DispatcherService.Database.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //----------------------------- AuxiliaryWorker
            CreateMap<CreateAuxiliaryWorkerContract, AuxiliaryWorker>();
            CreateMap<UpdateAuxiliaryWorkerContract, AuxiliaryWorker>();
            CreateMap<AuxiliaryWorker, AuxiliaryWorkerViewModel>();
            CreateMap<AuxiliaryWorker, AuxiliaryWorker>();
            CreateMap<AuxiliaryWorker, AuxiliaryWorkerHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            #region [ Brigade ]

            CreateMap<Brigade, BrigadeHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Brigade2AuxWorker, Brigade2AuxWorkerHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());            
            CreateMap<Brigade2Route, Brigade2RouteHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            #endregion

            #region [ TravelSheet ]

            CreateMap<TravelSheet, TravelSheetHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            #endregion

            #region [ ShiftTasks ]

            CreateMap<ShiftTask, ShiftTaskHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());            
            CreateMap<ShiftTask2Route, ShiftTask2RouteHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            #endregion
        }
    }
}
