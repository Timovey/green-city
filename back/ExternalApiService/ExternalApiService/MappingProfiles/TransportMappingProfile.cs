using AutoMapper;
using ExternalApi.Interface.Models.Transport.Driver;
using ExternalApi.Interface.Models.Transport.Transport;
using ExternalApi.Interface.Models.Transport.Driver2Transport;
using ExternalApi.Interface.Models.Transport.DriverVacation;
using ExternalApi.Interface.Models.Transport.RepairCard;
using ExternalApi.Interface.Models.Transport.TransportShedule;
using System;
using TransportContracts.DataContracts.Driver;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DataContracts.Transport;
using TransportContracts.DataContracts.Driver2Transport;
using TransportContracts.DataContracts.DriverVacation;
using TransportContracts.DataContracts.RepairCard;
using TransportContracts.DataContracts.TransportShedule;
using TransportContracts.Filters;
using TransportContracts.SortModels;
using TransportContracts.ViewModels;
using ExternalApi.Interface.Models.Transport.DateRange;
using TransportContracts.DateRange;
using ExternalApi.Interface.Models.Transport.TransportWorkShift;
using TransportContracts.DataContracts.TransportWorkShift;
using TransportContracts.DataContracts.Rent;
using TransportContracts.DataContracts.TransportDutyShift;
using ExternalApi.Interface.Models.Transport.Rent;
using ExternalApi.Interface.Models.Transport.TransportDutyShift;

namespace ExternalApiService.MappingProfiles
{
    public class TransportMappingProfile : Profile
    {
        public TransportMappingProfile()
        {
            //----------------------------- Transport
            CreateMap<ExtTransportType, TransportType>();
            CreateMap<ExtCreateTransportRequest, CreateTransportContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtUpdateTransportRequest, UpdateTransportContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteTransportRequest, DeleteTransportContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetTransportRequest, GetTransportContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtTransportSortModel, TransportSortModel>();
            CreateMap<ExtTransportFilterRequest, TransportFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<TransportViewModel, ExtTransportViewModel>();

            //----------------------------- Drivers
            CreateMap<ExtDrivingSkillsType, DrivingSkillsType>();
            CreateMap<ExtDriverPositionType, DriverPositionType>();
            CreateMap<DriverPositionType, ExtDriverPositionType>();
            CreateMap<ExtCreateDriverRequest, CreateDriverContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtUpdateDriverRequest, UpdateDriverContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteDriverRequest, DeleteDriverContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetDriverRequest, GetDriverContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDriverSortModel, DriverSortModel>();
            CreateMap<ExtDriverFilterRequest, DriverFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<DriverViewModel, ExtDriverViewModel>();

            //----------------------------- Drivers2Transport
            CreateMap<ExtCreateDriver2TransportRequest, CreateDriver2TransportContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtUpdateDriver2TransportRequest, UpdateDriver2TransportContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteDriver2TransportRequest, DeleteDriver2TransportContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetDriver2TransportRequest, GetDriver2TransportContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDriver2TransportSortModel, Driver2TransportSortModel>();
            CreateMap<ExtDriver2TransportFilterRequest, Driver2TransportFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<Driver2TransportViewModel, ExtDriver2TransportViewModel>();

            //----------------------------- DriversVacation
            CreateMap<ExtDriverVacationType, DriverVacationType>();
            CreateMap<ExtCreateDriverVacationRequest, CreateDriverVacationContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtUpdateDriverVacationRequest, UpdateDriverVacationContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteDriverVacationRequest, DeleteDriverVacationContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetDriverVacationRequest, GetDriverVacationContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDriverVacationSortModel, DriverVacationSortModel>();
            CreateMap<ExtDriverVacationFilterRequest, DriverVacationFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<DriverVacationViewModel, ExtDriverVacationViewModel>();
            CreateMap<DriverVacationViewModel, ExtDriverVacationViewModel>();

            //----------------------------- RepairCard
            CreateMap<ExtRepairType, RepairType>();
            CreateMap<ExtWorkShift, WorkShift>();
            CreateMap<ExtCreateRepairCardRequest, CreateRepairCardContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtUpdateRepairCardRequest, UpdateRepairCardContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteRepairCardRequest, DeleteRepairCardContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetRepairCardRequest, GetRepairCardContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtRepairCardSortModel, RepairCardSortModel>();
            CreateMap<ExtRepairCardFilterRequest, RepairCardFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<RepairCardViewModel, ExtRepairCardViewModel>();

            //----------------------------- Rent
            CreateMap<ExtCreateRentRequest, CreateRentContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteRentRequest, DeleteRentContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetRentRequest, GetRentContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtRentSortModel, RentSortModel>();
            CreateMap<ExtRentFilterRequest, RentFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<RentViewModel, ExtRentViewModel>();

            //----------------------------- TransportDutyShift
            CreateMap<ExtCreateTransportDutyShiftRequest, CreateTransportDutyShiftContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteTransportDutyShiftRequest, DeleteTransportDutyShiftContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetTransportDutyShiftRequest, GetTransportDutyShiftContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtTransportDutyShiftSortModel, TransportDutyShiftSortModel>();
            CreateMap<ExtTransportDutyShiftFilterRequest, TransportDutyShiftFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<TransportDutyShiftViewModel, ExtTransportDutyShiftViewModel>();

            //----------------------------- TransportShedule
            CreateMap<ExtWorkScheduleType, WorkScheduleType>();
            CreateMap<TransportWorkDateType, ExtTransportWorkDateType>();
            CreateMap<DriverWorkDateType, ExtDriverWorkDateType>();
            CreateMap<DateWorkedDriverType, ExtDateWorkedDriverType>();
            CreateMap<ExtCreateTransportSheduleRequest, CreateTransportSheduleContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtUpdateTransportSheduleRequest, UpdateTransportSheduleContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDeleteTransportSheduleRequest, DeleteTransportSheduleContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetTransportSheduleRequest, GetTransportSheduleContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetTransportSheduleDateRangeRequest, GetTransportSheduleDateRangeContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtTransportSheduleSortModel, TransportSheduleSortModel>();
            CreateMap<ExtTransportSheduleFilterRequest, TransportSheduleFilterModel>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<TransportSheduleViewModel, ExtTransportSheduleViewModel>();
            CreateMap<DriverSheduleViewModel, ExtDriverSheduleViewModel>();
            CreateMap<TransportSheduleDateRangeViewModel, ExtTransportSheduleDateRangeViewModel>();
            CreateMap<DictinaryDateRangeViewModel, ExtDictinaryDateRangeViewModel>();

            //----------------------------- TransportWorkShift
            CreateMap<ExtGetActualWorkShitsRequest, GetActualWorkShitsContract>()
               .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtGetWorkShitsDropdownRequest, GetWorkShitsDropdownContract>()
             .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtActualizeWorkShitsRequest, ActualizeWorkShitsContract>()
               .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<TransportWorkShiftViewModel, ExtTransportWorkShiftViewModel>();
            CreateMap<TransportWorkShiftDropdownViewModel, ExtTransportWorkShiftDropdownViewModel>();

            //----------------------------- DateRange
            CreateMap<DateRangeBaseModel,ExtDateRangeBaseModel>();
            CreateMap<ExtDateRangeBaseModel, DateRangeBaseModel>();
        }
    }
}
