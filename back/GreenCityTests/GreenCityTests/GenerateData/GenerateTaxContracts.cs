using System;
using System.Collections.Generic;
using TransportContracts.DataContracts.Driver;
using TransportContracts.DataContracts.Driver2Transport;
using TransportContracts.DataContracts.DriverVacation;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DataContracts.Rent;
using TransportContracts.DataContracts.RepairCard;
using TransportContracts.DataContracts.Transport;
using TransportContracts.DataContracts.TransportDutyShift;
using TransportContracts.DataContracts.TransportShedule;
using TransportContracts.DateRange;

namespace GreenCityTests.GenerateData
{
    public static class GenerateTaxContracts
    {
        public static List<CreateDriverContract> GetTestsDrivers()
        {
            return new List<CreateDriverContract>
            {
                new CreateDriverContract
                {
                    TableNumber = "1",
                    DriverName= "Вася",
                    DriverSurname = "Пупкин",
                    DrivingSkills = new List<DrivingSkillsType>() {
                        DrivingSkillsType.GarbageTruck, 
                        DrivingSkillsType.BunkerTruck, 
                        DrivingSkillsType.DumpTruck
                    },
                    DriverPositionType = DriverPositionType.Main,
                    AcceptWorkDate = DateTime.Now.AddDays(-4),
                    BirthDate = DateTime.Parse("2000-01-01"),
                    IssueDate = DateTime.Parse("2022-01-01"),
                    OrganizationId= 1
                },
                new CreateDriverContract
                {
                    TableNumber = "2",
                    DriverName= "Михал",
                    DriverSurname = "Петрович",
                    DrivingSkills = new List<DrivingSkillsType>() {
                        DrivingSkillsType.GarbageTruck,
                        DrivingSkillsType.BunkerTruck,
                        DrivingSkillsType.DumpTruck
                    },
                    DriverPositionType = DriverPositionType.Main,
                    AcceptWorkDate= DateTime.Now.AddDays(1),
                    BirthDate = DateTime.Parse("1997-05-29"),
                    IssueDate = DateTime.Parse("2022-01-01"),
                    OrganizationId = 2
                },
                new CreateDriverContract
                {
                    TableNumber = "3",
                    DriverName= "Александр",
                    DriverSurname = "Крупинин",
                    DrivingSkills = new List<DrivingSkillsType>() {
                        DrivingSkillsType.GarbageTruck,
                        DrivingSkillsType.BunkerTruck,
                        DrivingSkillsType.DumpTruck
                    },
                    DriverPositionType = DriverPositionType.Main,
                    AcceptWorkDate= DateTime.Now.AddDays(-30),
                    BirthDate = DateTime.Parse("1989-08-14"),
                    IssueDate = DateTime.Parse("2022-01-01"),
                    OrganizationId= 1
                },
                 new CreateDriverContract
                {
                    TableNumber = "4",
                    DriverName= "Валерий",
                    DriverSurname = "Профкофьев",
                    DrivingSkills = new List<DrivingSkillsType>() {
                        DrivingSkillsType.GarbageTruck,
                        DrivingSkillsType.BunkerTruck,
                        DrivingSkillsType.DumpTruck
                    },
                    DriverPositionType = DriverPositionType.Main,
                    AcceptWorkDate = DateTime.Now.AddDays(-30),
                    BirthDate = DateTime.Parse("1995-02-11"),
                    IssueDate = DateTime.Parse("2022-01-01"),
                    OrganizationId = 2
                },
                  new CreateDriverContract
                {
                    TableNumber = "5",
                    DriverName= "Михаил",
                    DriverSurname = "Валуев",
                    DrivingSkills = new List<DrivingSkillsType>() {
                        DrivingSkillsType.GarbageTruck,
                        DrivingSkillsType.BunkerTruck,
                        DrivingSkillsType.DumpTruck
                    },
                    DriverPositionType = DriverPositionType.Main,
                    AcceptWorkDate = DateTime.Now.AddDays(-30),
                    BirthDate = DateTime.Parse("2003-05-18"),
                    IssueDate = DateTime.Parse("2022-01-01"),
                    OrganizationId = 2
                },
            };
        }

        public static List<CreateTransportContract> GetTestsTransports()
        {
            return new List<CreateTransportContract>
            {
                new CreateTransportContract
                {
                    Name = "Грузовик особенный",
                    RegNumber = "Е267ММ",
                    Sts = "test_sts",
                    TransportType = TransportType.GarbageTruck,
                    OrganizationId= 1,
                },
                new CreateTransportContract
                {
                    Name = "Камаз",
                    Sts = "test_sts",
                    RegNumber = "А587АО",
                    TransportType = TransportType.GarbageTruck,
                    OrganizationId= 2,
                },
                new CreateTransportContract
                {
                    Name = "Газель",
                    Sts = "test_sts",
                    RegNumber = "М537АО",
                    TransportType = TransportType.GarbageTruck,
                    OrganizationId= 2,
                },
            };
        }

        public static List<CreateDriver2TransportContract> GetTestsDriver2Transports(List<int> driverIds, List<int> transportIds)
        {
            return new List<CreateDriver2TransportContract>
            {
                new CreateDriver2TransportContract
                {
                    DriverId= driverIds[0],
                    TransportId= transportIds[0],
                    WorkShift = WorkShift.AllDay,
                    DateRange = new DateRangeBaseModel
                    {
                        CreateDate = DateTime.Now.AddHours(-4),
                        StartDate = DateTime.Parse("2023-03-01"),
                        EndDate = DateTime.MaxValue
                    }
                },
                new CreateDriver2TransportContract
                {
                    DriverId= driverIds[1],
                    TransportId= transportIds[0],
                    WorkShift = WorkShift.AllDay,
                    DateRange = new DateRangeBaseModel
                    {
                        CreateDate = DateTime.Now.AddHours(-2),
                        StartDate = DateTime.Parse("2023-03-04"),
                        EndDate = null,
                    }
                },
                new CreateDriver2TransportContract
                {
                    DriverId= driverIds[2],
                    TransportId= transportIds[1],
                    WorkShift = WorkShift.AllDay,
                    DateRange = new DateRangeBaseModel
                    {
                        CreateDate = DateTime.Now,
                        StartDate = DateTime.Parse("2023-03-01"),
                        EndDate = null,
                    }
                },
            };
        }

        public static List<CreateTransportSheduleContract> GetTestsTransportShedules(List<int> transportIds)
        {
            return new List<CreateTransportSheduleContract>
            {
                new CreateTransportSheduleContract
                {
                    TransportId= transportIds[0],
                    WorkSheduleType = WorkScheduleType.ThreeThree,
                    DateRange = new DateRangeBaseModel
                    {
                        CreateDate = DateTime.Now.AddHours(-5),
                        StartDate = DateTime.Parse("2023-03-01"),
                        EndDate = DateTime.MaxValue,
                    }
                },
                new CreateTransportSheduleContract
                {
                    TransportId = transportIds[1],
                    WorkSheduleType = WorkScheduleType.FiveTwo,
                    DateRange = new DateRangeBaseModel
                    {
                        CreateDate = DateTime.Now.AddHours(-5),
                        StartDate = DateTime.Parse("2023-03-01"),
                        EndDate = DateTime.MaxValue,
                    }
                },
            };
        }

        public static List<CreateDriverVacationContract> GetTestsDriverVacations(List<int> driverIds)
        {
            return new List<CreateDriverVacationContract>
            {
                new CreateDriverVacationContract
                {
                    DateRange = new DateRangeBaseModel
                    {
                        CreateDate = DateTime.Now,
                        StartDate = DateTime.Parse("2023-03-02"),
                        EndDate = DateTime.Parse("2023-03-03"),
                    },
                    DriverId= driverIds[0],
                    ReplacementDriverId = null,
                },
                new CreateDriverVacationContract
                {
                    DateRange = new DateRangeBaseModel
                    {
                        CreateDate = DateTime.Now.AddHours(2),
                        StartDate = DateTime.Parse("2023-03-05"),
                        EndDate = DateTime.Parse("2023-03-07"),
                    },
                    DriverId= driverIds[1],
                    ReplacementDriverId = driverIds[3],
                },
            };
        }

        public static List<CreateRentContract> GetTestsRents(List<int> transportIds)
        {
            return new List<CreateRentContract>
            {
                new CreateRentContract
                {
                    DateRange = new DateRangeBaseModel
                    {
                        CreateDate = DateTime.Now,
                        StartDate = DateTime.Parse("2023-03-02"),
                        EndDate = DateTime.Parse("2023-03-04"),
                    },
                    OrganizationId = 2,
                    TransportId = transportIds[0],
                },
                new CreateRentContract
                {
                    DateRange = new DateRangeBaseModel
                    {
                        CreateDate = DateTime.Now,
                        StartDate = DateTime.Parse("2023-03-03"),
                        EndDate = DateTime.Parse("2023-03-06"),
                    },
                    OrganizationId = 3,
                    TransportId = transportIds[1],
                },
            };
        }

        public static List<CreateTransportDutyShiftContract> GetTestsDutyShifts(List<int> transportIds)
        {
            return new List<CreateTransportDutyShiftContract>
            {
                new CreateTransportDutyShiftContract
                {
                    TransportId = transportIds[1],
                    Date = DateTime.Parse("2023-03-01")
                }
            };
        }

        public static List<CreateRepairCardContract> GetTestsTransportRepars(List<int> transportIds)
        {
            return new List<CreateRepairCardContract>
            {
                new CreateRepairCardContract
                {
                    TransportId = transportIds[0],
                    OrganizationId= 1,
                    Event = RepairType.Repair,
                    WorkShift= WorkShift.AllDay,
                    DateRange = new DateRangeBaseModel
                    {
                        CreateDate = DateTime.Now,
                        StartDate = DateTime.Parse("2023-03-05"),
                        EndDate = DateTime.Parse("2023-03-05")
                    }
                }
            };
        }
    }
}
