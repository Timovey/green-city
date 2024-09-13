using AutoMapper;
using TransportService.Database.Models;
using TransportContracts.DataContracts.Transport;
using TransportContracts.ViewModels;
using TransportContracts.DataContracts.Driver;
using TransportContracts.DataContracts.Driver2Transport;
using TransportContracts.DataContracts.DriverVacation;
using TransportContracts.DataContracts.RepairCard;
using GreenCity.CommonUtils.CommonExtensions;
using System.Collections.Generic;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DateRange;
using TransportContracts.DataContracts.TransportShedule;
using TransportContracts.DataContracts.TransportWorkShift;
using TransportContracts.DataContracts.Rent;
using TransportContracts.DataContracts.TransportDutyShift;

namespace TransportService.Database.MappingProfiles
{
    /// <summary>
    /// Класс конфигурации маппера
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //----------------------------- DateRange
            CreateMap<DateRangeBaseModel, DateRange>();
            CreateMap<DateRange, DateRangeBaseModel>();
            CreateMap<DateRange, DateRange>();
            CreateMap<DateRange, DateRangeHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            //----------------------------- Transport
            CreateMap<CreateTransportContract, Transport>()
                .ForMember(dest => dest.TankTypesJson,
                    opt => opt.MapFrom(x => x.TankTypes.SafeSerialize(null)));
            CreateMap<UpdateTransportContract, Transport>()
                .ForMember(dest => dest.TankTypesJson,
                    opt => opt.MapFrom(x => x.TankTypes.SafeSerialize(null)));
            CreateMap<Transport, TransportViewModel>()
                .ForMember(dest => dest.TankTypes,
                    opt => opt.MapFrom(x => x.TankTypesJson.SafeDeserialize<IList<int>>(null)))
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<Transport, TransportSheduleDateRangeViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<Transport, Transport>();
            CreateMap<Transport, TransportHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            //----------------------------- TranportShedule
            CreateMap<CreateTransportSheduleContract, TransportShedule>()
                .ForPath(dest => dest.CreateDate,
                    opt => opt.MapFrom(x => x.DateRange.CreateDate))
                .ForPath(dest => dest.EndDate,
                    opt => opt.MapFrom(x => x.DateRange.EndDate))
                .ForPath(dest => dest.StartDate,
                    opt => opt.MapFrom(x => x.DateRange.StartDate));
            CreateMap<UpdateTransportSheduleContract, TransportShedule>()
                .ForPath(dest => dest.CreateDate,
                    opt => opt.MapFrom(x => x.DateRange.CreateDate))
                .ForPath(dest => dest.EndDate,
                    opt => opt.MapFrom(x => x.DateRange.EndDate))
                .ForPath(dest => dest.StartDate,
                    opt => opt.MapFrom(x => x.DateRange.StartDate));
            CreateMap<TransportShedule, TransportSheduleViewModel>()
                .ForMember(dest => dest.TransportSheduleId,
                   opt => opt.MapFrom(x => x.Id))
               .ForPath(dest => dest.DateRange.CreateDate,
                   opt => opt.MapFrom(x => x.CreateDate))
               .ForPath(dest => dest.DateRange.EndDate,
                   opt => opt.MapFrom(x => x.EndDate))
               .ForPath(dest => dest.DateRange.StartDate,
                   opt => opt.MapFrom(x => x.StartDate));
            CreateMap<TransportShedule, TransportShedule>();
            CreateMap<TransportShedule, TransportSheduleHistory>()
               .ForMember(dest => dest.Id,
                   opt => opt.Ignore());

            //----------------------------- Driver
            CreateMap<CreateDriverContract, Driver>()
                .ForMember(dest => dest.DrivingSkillsJson,
                    opt => opt.MapFrom(x => x.DrivingSkills.SafeSerialize(null)));
            CreateMap<UpdateDriverContract, Driver>().
                ForMember(dest => dest.DrivingSkillsJson,
                    opt => opt.MapFrom(x => x.DrivingSkills.SafeSerialize(null)));
            CreateMap<Driver, DriverViewModel>()
                .ForMember(dest => dest.DrivingSkills,
                    opt => opt.MapFrom(x => x.DrivingSkillsJson.SafeDeserialize<IList<DrivingSkillsType>>(null)))
                .ForMember(dest => dest.DriverId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<Driver, DriverSheduleViewModel>()
                .ForMember(dest => dest.DrivingSkills,
                    opt => opt.MapFrom(x => x.DrivingSkillsJson.SafeDeserialize<IList<DrivingSkillsType>>(null)))
                .ForMember(dest => dest.DriverId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<Driver, Driver>();
            CreateMap<Driver, DriverHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            //----------------------------- RepairCard
            CreateMap<CreateRepairCardContract, RepairCard>()
                .ForPath(dest => dest.CreateDate,
                    opt => opt.MapFrom(x => x.DateRange.CreateDate))
                .ForPath(dest => dest.EndDate,
                    opt => opt.MapFrom(x => x.DateRange.EndDate))
                .ForPath(dest => dest.StartDate,
                    opt => opt.MapFrom(x => x.DateRange.StartDate));
            CreateMap<UpdateRepairCardContract, RepairCard>()
                .ForPath(dest => dest.CreateDate,
                    opt => opt.MapFrom(x => x.DateRange.CreateDate))
                .ForPath(dest => dest.EndDate,
                    opt => opt.MapFrom(x => x.DateRange.EndDate))
                .ForPath(dest => dest.StartDate,
                    opt => opt.MapFrom(x => x.DateRange.StartDate));
            CreateMap<RepairCard, RepairCardViewModel>()
                .ForMember(dest => dest.RepairCardId,
                    opt => opt.MapFrom(x => x.Id))
                .ForPath(dest => dest.DateRange.CreateDate,
                    opt => opt.MapFrom(x => x.CreateDate))
                .ForPath(dest => dest.DateRange.EndDate,
                    opt => opt.MapFrom(x => x.EndDate))
                .ForPath(dest => dest.DateRange.StartDate,
                    opt => opt.MapFrom(x => x.StartDate));
            CreateMap<RepairCard, RepairCard>();
            CreateMap<RepairCard, RepairCardHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            //----------------------------- Driver2Transport
            CreateMap<CreateDriver2TransportContract, Driver2Transport>()
                .ForPath(dest => dest.CreateDate,
                    opt => opt.MapFrom(x => x.DateRange.CreateDate))
                .ForPath(dest => dest.EndDate,
                    opt => opt.MapFrom(x => x.DateRange.EndDate))
                .ForPath(dest => dest.StartDate,
                    opt => opt.MapFrom(x => x.DateRange.StartDate));
            CreateMap<UpdateDriver2TransportContract, Driver2Transport>()
                .ForPath(dest => dest.CreateDate,
                    opt => opt.MapFrom(x => x.DateRange.CreateDate))
                .ForPath(dest => dest.EndDate,
                    opt => opt.MapFrom(x => x.DateRange.EndDate))
                .ForPath(dest => dest.StartDate,
                    opt => opt.MapFrom(x => x.DateRange.StartDate));
            CreateMap<Driver2Transport, Driver2TransportViewModel>()
                .ForMember(dest => dest.Driver2TransportId,
                    opt => opt.MapFrom(x => x.Id))
                .ForPath(dest => dest.DateRange.CreateDate,
                    opt => opt.MapFrom(x => x.CreateDate))
                .ForPath(dest => dest.DateRange.EndDate,
                    opt => opt.MapFrom(x => x.EndDate))
                .ForPath(dest => dest.DateRange.StartDate,
                    opt => opt.MapFrom(x => x.StartDate));
            CreateMap<Driver2Transport, Driver2Transport>();
            CreateMap<Driver2Transport, Driver2TransportHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            //----------------------------- DriverVacation
            CreateMap<CreateDriverVacationContract, DriverVacation>()
                .ForPath(dest => dest.CreateDate,
                    opt => opt.MapFrom(x => x.DateRange.CreateDate))
                .ForPath(dest => dest.EndDate,
                    opt => opt.MapFrom(x => x.DateRange.EndDate))
                .ForPath(dest => dest.StartDate,
                    opt => opt.MapFrom(x => x.DateRange.StartDate));
            CreateMap<UpdateDriverVacationContract, DriverVacation>()
                .ForPath(dest => dest.CreateDate,
                    opt => opt.MapFrom(x => x.DateRange.CreateDate))
                .ForPath(dest => dest.EndDate,
                    opt => opt.MapFrom(x => x.DateRange.EndDate))
                .ForPath(dest => dest.StartDate,
                    opt => opt.MapFrom(x => x.DateRange.StartDate));
            CreateMap<DriverVacation, DriverVacationViewModel>()
                .ForMember(dest => dest.DriverVacationId,
                    opt => opt.MapFrom(x => x.Id))
                .ForPath(dest => dest.DateRange.CreateDate,
                    opt => opt.MapFrom(x => x.CreateDate))
                .ForPath(dest => dest.DateRange.EndDate,
                    opt => opt.MapFrom(x => x.EndDate))
                .ForPath(dest => dest.DateRange.StartDate,
                    opt => opt.MapFrom(x => x.StartDate));
            CreateMap<DriverVacation, DriverVacation>();
            CreateMap<DriverVacation, DriverVacationHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            //----------------------------- Rent
            CreateMap<CreateRentContract, Rent>()
                .ForPath(dest => dest.CreateDate,
                    opt => opt.MapFrom(x => x.DateRange.CreateDate))
                .ForPath(dest => dest.EndDate,
                    opt => opt.MapFrom(x => x.DateRange.EndDate))
                .ForPath(dest => dest.StartDate,
                    opt => opt.MapFrom(x => x.DateRange.StartDate));
            CreateMap<Rent, RentViewModel>()
                .ForMember(dest => dest.RentId,
                   opt => opt.MapFrom(x => x.Id))
               .ForPath(dest => dest.DateRange.CreateDate,
                   opt => opt.MapFrom(x => x.CreateDate))
               .ForPath(dest => dest.DateRange.EndDate,
                   opt => opt.MapFrom(x => x.EndDate))
               .ForPath(dest => dest.DateRange.StartDate,
                   opt => opt.MapFrom(x => x.StartDate));
            CreateMap<Rent, Rent>();
            CreateMap<Rent, RentHistory>()
               .ForMember(dest => dest.Id,
                   opt => opt.Ignore());

            //----------------------------- TransportWorkShift
            CreateMap<TransportViewModel, CreateTransportWorkShiftContract>()
                .ForMember(dest => dest.TransportId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<CreateTransportWorkShiftContract, TransportWorkShift>()
                 .ForMember(dest => dest.OrganizationIdsJson,
                    opt => opt.MapFrom(x => x.OrganizationIds.SafeSerialize(null)));
            CreateMap<TransportWorkShift, TransportWorkShift>();
            CreateMap<TransportWorkShift, TransportWorkShiftHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());
            CreateMap<TransportWorkShift, TransportWorkShiftViewModel>()
                .ForMember(dest => dest.OrganizationIds,
                    opt => opt.MapFrom(x => x.OrganizationIdsJson
                                .SafeDeserialize<IList<int>>(null)));
            CreateMap<TransportWorkShiftViewModel, TransportWorkShiftViewModel>();
            CreateMap<TransportWorkShiftViewModel, CreateTransportWorkShiftContract>();
            CreateMap<TransportWorkShift, TransportWorkShiftDropdownViewModel>()
                .ForMember(dest => dest.OrganizationIds,
                    opt => opt.MapFrom(x => x.OrganizationIdsJson
                                .SafeDeserialize<IList<int>>(null)));

            //----------------------------- TransportDutyShift
            CreateMap<CreateTransportDutyShiftContract, TransportDutyShift>();
            CreateMap<TransportDutyShift, TransportDutyShiftViewModel>()
                .ForMember(dest => dest.TransportDutyShiftId,
                    opt => opt.MapFrom(x => x.Id));
            CreateMap<TransportDutyShift, TransportDutyShift>();
            CreateMap<TransportDutyShift, TransportDutyShiftHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());
        }
    }
}
