using ExternalApi.Interface.Models.Sd.Container;
using ExternalApi.Interface.Models.Sd.Contract;
using ExternalApi.Interface.Models.Sd.Contract.ScheduleModels;
using ExternalApi.Interface.Models.Sd.ContractClient;
using ExternalApi.Interface.Models.Sd.Stand;
using GreenCity.CommonUtils.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExternalApiService.Services
{
    internal class TestDataProvider : ITestDataProvider
    {
        public IList<ExtCreateStandRequest> GetStands()
        {
            return new List<ExtCreateStandRequest>
            {
                //contract 1
                new ExtCreateStandRequest
                {
                    IsHighSecurityObject = true,
                    Address = "Stand_address_1",
                    Coordinates = new ExtStandCoordinatesModel
                    {
                        Coordinates = new List<Coordinates>
                        {
                            new Coordinates { Latitude = 45.1654M, Longitude = 78.3658M },
                            new Coordinates { Latitude = 12.1242M, Longitude = 41.1241M },
                            new Coordinates { Latitude = 45.1654M, Longitude = 78.3658M }
                        },
                        CoordinatesComment = "Coordinates_comment_1"
                    },
                    DriveWay = "Drive_way_1",
                    Contacts = "79278039373",
                    StandType = ExtStandType.Default,
                    HaveBase = false,
                    HaveRoof = true,
                    HaveFence = true,
                    IsValidated = true,
                    UserId = 15,
                    UserName = "Test Data Generator"
                },
                new ExtCreateStandRequest
                {
                    IsHighSecurityObject = true,
                    Address = "Stand_address_2",
                    Coordinates = new ExtStandCoordinatesModel
                    {
                        Coordinates = new List<Coordinates>
                        {
                            new Coordinates { Latitude = 100.1654M, Longitude = 99.3658M }
                        },
                        CoordinatesComment = "Coordinates_comment_2"
                    },
                    DriveWay = "Drive_way_2",
                    Contacts = "79278039374",
                    StandType = ExtStandType.Default,
                    HaveBase = true,
                    HaveRoof = true,
                    HaveFence = true,
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateStandRequest
                {
                    IsHighSecurityObject = false,
                    Address = "Stand_address_3",
                    Coordinates = new ExtStandCoordinatesModel
                    {
                        Coordinates = new List<Coordinates>
                        {
                            new Coordinates { Latitude = 54.1654M, Longitude = 78.3658M },
                            new Coordinates { Latitude = 79.1242M, Longitude = 41.1241M },
                            new Coordinates { Latitude = 120.1654M, Longitude = 78.3658M }
                        },
                        CoordinatesComment = "Coordinates_comment_3"
                    },
                    DriveWay = "Drive_way_3",
                    Contacts = "79278039374",
                    StandType = ExtStandType.HouseStock,
                    HaveBase = false,
                    HaveRoof = true,
                    HaveFence = false,
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                //contract 2
                new ExtCreateStandRequest
                {
                    IsHighSecurityObject = false,
                    Address = "Stand_address_4",
                    Coordinates = new ExtStandCoordinatesModel
                    {
                        Coordinates = new List<Coordinates>
                        {
                            new Coordinates { Latitude = 65.1242M, Longitude = 47.1241M },
                            new Coordinates { Latitude = 43.1654M, Longitude = 17.3658M }
                        },
                        CoordinatesComment = "Coordinates_comment_4"
                    },
                    DriveWay = "Drive_way_4",
                    Contacts = "79278039375",
                    StandType = ExtStandType.Default,
                    HaveBase = true,
                    HaveRoof = false,
                    HaveFence = true,
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateStandRequest
                {
                    IsHighSecurityObject = false,
                    Address = "Stand_address_5",
                    Coordinates = new ExtStandCoordinatesModel
                    {
                        Coordinates = new List<Coordinates>
                        {
                            new Coordinates { Latitude = 11.1654M, Longitude = 12.3658M }
                        },
                        CoordinatesComment = "Coordinates_comment_5"
                    },
                    DriveWay = "Drive_way_5",
                    Contacts = "79278039375",
                    StandType = ExtStandType.Default,
                    HaveBase = false,
                    HaveRoof = true,
                    HaveFence = false,
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                //common stand
                new ExtCreateStandRequest
                {
                    IsHighSecurityObject = false,
                    Address = "Stand_address_6",
                    Coordinates = new ExtStandCoordinatesModel
                    {
                        Coordinates = new List<Coordinates>
                        {
                            new Coordinates { Latitude = 37.1654M, Longitude = 39.3658M }
                        },
                        CoordinatesComment = "Coordinates_comment_6"
                    },
                    DriveWay = "Drive_way_6",
                    Contacts = "79278039376",
                    StandType = ExtStandType.Default,
                    HaveBase = false,
                    HaveRoof = true,
                    HaveFence = false,
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                }
            };
        }

        public IList<ExtCreateContainerRequest> GetContainers(
            IList<(int id, string address)> stands,
            IList<int> containerTypeIds
        )
        {
            return new List<ExtCreateContainerRequest>
            {
                //contract 1
                new ExtCreateContainerRequest
                {
                    Name = "container_1_stand_1_contract_1",
                    ContainerTypeId = new Random().Next(containerTypeIds.Min(), containerTypeIds.Max()),
                    TrashType = ExtTrashType.Class4,
                    ContainerStandId = stands.Where(x => x.address.Contains("Stand_address_1")).Select(x => x.id).FirstOrDefault(),
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateContainerRequest
                {
                    Name = "container_2_stand_1_contract_1",
                    ContainerTypeId = new Random().Next(containerTypeIds.Min(), containerTypeIds.Max()),
                    TrashType = ExtTrashType.Class5,
                    ContainerStandId = stands.Where(x => x.address.Contains("Stand_address_1")).Select(x => x.id).FirstOrDefault(),
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateContainerRequest
                {
                    Name = "container_3_stand_2_contract_1",
                    ContainerTypeId = new Random().Next(containerTypeIds.Min(), containerTypeIds.Max()),
                    TrashType = ExtTrashType.KGO,
                    ContainerStandId = stands.Where(x => x.address.Contains("Stand_address_2")).Select(x => x.id).FirstOrDefault(),
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                //contract 2
                new ExtCreateContainerRequest
                {
                    Name = "container_4_stand_1_contract_2",
                    ContainerTypeId = new Random().Next(containerTypeIds.Min(), containerTypeIds.Max()),
                    TrashType = ExtTrashType.Construct,
                    ContainerStandId = stands.Where(x => x.address.Contains("Stand_address_1")).Select(x => x.id).FirstOrDefault(),
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateContainerRequest
                {
                    Name = "container_5_stand_3_contract_2",
                    ContainerTypeId = new Random().Next(containerTypeIds.Min(), containerTypeIds.Max()),
                    TrashType = ExtTrashType.TKO,
                    ContainerStandId = stands.Where(x => x.address.Contains("Stand_address_3")).Select(x => x.id).FirstOrDefault(),
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateContainerRequest
                {
                    Name = "container_6_stand_3_contract_2",
                    ContainerTypeId = new Random().Next(containerTypeIds.Min(), containerTypeIds.Max()),
                    TrashType = ExtTrashType.KGO,
                    ContainerStandId = stands.Where(x => x.address.Contains("Stand_address_3")).Select(x => x.id).FirstOrDefault(),
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateContainerRequest
                {
                    Name = "container_7_stand_4_contract_2",
                    ContainerTypeId = new Random().Next(containerTypeIds.Min(), containerTypeIds.Max()),
                    TrashType = ExtTrashType.Construct,
                    ContainerStandId = stands.Where(x => x.address.Contains("Stand_address_4")).Select(x => x.id).FirstOrDefault(),
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                //contract 3
                new ExtCreateContainerRequest
                {
                    Name = "container_8_stand_4_contract_3",
                    ContainerTypeId = new Random().Next(containerTypeIds.Min(), containerTypeIds.Max()),
                    TrashType = ExtTrashType.KGO,
                    ContainerStandId = stands.Where(x => x.address.Contains("Stand_address_4")).Select(x => x.id).FirstOrDefault(),
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateContainerRequest
                {
                    Name = "container_9_stand_5_contract_3",
                    ContainerTypeId = new Random().Next(containerTypeIds.Min(), containerTypeIds.Max()),
                    TrashType = ExtTrashType.Class5,
                    ContainerStandId = stands.Where(x => x.address.Contains("Stand_address_5")).Select(x => x.id).FirstOrDefault(),
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateContainerRequest
                {
                    Name = "container_10_stand_5_contract_3",
                    ContainerTypeId = new Random().Next(containerTypeIds.Min(), containerTypeIds.Max()),
                    TrashType = ExtTrashType.Construct,
                    ContainerStandId = stands.Where(x => x.address.Contains("Stand_address_5")).Select(x => x.id).FirstOrDefault(),
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateContainerRequest
                {
                    Name = "container_11_stand_6_contract_3",
                    ContainerTypeId = new Random().Next(containerTypeIds.Min(), containerTypeIds.Max()),
                    TrashType = ExtTrashType.Class5,
                    ContainerStandId = stands.Where(x => x.address.Contains("Stand_address_6")).Select(x => x.id).FirstOrDefault(),
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateContainerRequest
                {
                    Name = "container_12_stand_6_contract_3",
                    ContainerTypeId = new Random().Next(containerTypeIds.Min(), containerTypeIds.Max()),
                    TrashType = ExtTrashType.Class4,
                    ContainerStandId = stands.Where(x => x.address.Contains("Stand_address_6")).Select(x => x.id).FirstOrDefault(),
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                }
            };
        }

        public IList<ExtCreateContractClientRequest> GetContractClients()
        {
            return new List<ExtCreateContractClientRequest>
            {
                new ExtCreateContractClientRequest
                {
                    ClientName = "Client_1",
                    ClientContact = "Client_contact_1",
                    Email = "client1@gmail.com",
                    Inn = "inn_1",
                    Kpp = "kpp_1",
                    Ogrn = "ogrn_1",
                    Rschet = "rschet_1",
                    Bank = "bank_1",
                    Bik = "bik_1",
                    Kschet = "Kschet_1",
                    LegalAddress = "legal_address_1",
                    SupervizorName = "supervizor_1",
                    SupervizorPosition = "supervizor_position_1",
                    ResponsibleTkoPersonName = "tko_person_name_1",
                    ResponsibleTkoPersonPhone = "phone_1",
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateContractClientRequest
                {
                    ClientName = "Client_2",
                    ClientContact = "Client_contact_2",
                    Email = "client2@gmail.com",
                    Inn = "inn_2",
                    Kpp = "kpp_2",
                    Ogrn = "ogrn_2",
                    Rschet = "rschet_2",
                    Bank = "bank_2",
                    Bik = "bik_2",
                    Kschet = "Kschet_2",
                    LegalAddress = "legal_address_2",
                    SupervizorName = "supervizor_2",
                    SupervizorPosition = "supervizor_position_2",
                    ResponsibleTkoPersonName = "tko_person_name_2",
                    ResponsibleTkoPersonPhone = "phone_2",
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                },
                new ExtCreateContractClientRequest
                {
                    ClientName = "Client_3",
                    ClientContact = "Client_contact_3",
                    Email = "client3@gmail.com",
                    Inn = "inn_3",
                    Kpp = "kpp_3",
                    Ogrn = "ogrn_3",
                    Rschet = "rschet_3",
                    Bank = "bank_3",
                    Bik = "bik_3",
                    Kschet = "Kschet_3",
                    LegalAddress = "legal_address_3",
                    SupervizorName = "supervizor_3",
                    SupervizorPosition = "supervizor_position_3",
                    ResponsibleTkoPersonName = "tko_person_name_3",
                    ResponsibleTkoPersonPhone = "phone_3",
                    UserId = 15,
                    UserName = "Test Data Generator",
                    IsValidated = true
                }
            };
        }

        public IList<ExtCreateContractRequest> GetContracts(
            IList<(int id, string name)> contractClients,
            IList<(int id, string name)> containers
        )
        {
            var result = new List<ExtCreateContractRequest>();

            var contractWithoutPeriods = new ExtCreateContractRequest
            {
                ContractNumber = "contract_number_without_periods",
                ContractClientId = contractClients.Where(x => x.name.Contains("Client_1")).Select(x => x.id).FirstOrDefault(),
                CreateDate = DateTime.Parse("2000-07-12"),
                StartDate = DateTime.Parse("2001-07-12"),
                EndDate = DateTime.Parse("2045-09-12"),
                SendDocsType = "send_doc_type_without_periods",
                Comment = "contract_comment_without_periods",
                IsAutorenewSchedules = true,
                UserId = 15,
                UserName = "Test Data Generator",
                AttachedContainers = new List<ExtCreateContainer2ScheduleRequest>
                {
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_2_stand_1_contract_1")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = null,
                        ScheduleComment = "schedule_comment_2",
                        WorkTimeFrom = "08:30",//DateTime.Parse("2023-12-12").Date.AddHours(8).AddMinutes(30),
                        WorkTimeTo = "18:00", //DateTime.Parse("2023-12-12").Date.AddHours(18).AddMinutes(00),
                        IsInRegistry = true,
                        InRegistryFromDate = "1999-01-01"
                    },
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_3_stand_2_contract_1")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = null,
                        ScheduleComment = "schedule_comment_2",
                        WorkTimeFrom = "05:30",
                        WorkTimeTo = "20:00", 
                        IsInRegistry = true,
                        InRegistryFromDate = "1999-01-01"
                    }
                }
            };

            var contract1 = new ExtCreateContractRequest
            {
                ContractNumber = "contract_number_1",
                ContractClientId = contractClients.Where(x => x.name.Contains("Client_1")).Select(x => x.id).FirstOrDefault(),
                CreateDate = DateTime.Parse("2023-07-12"),
                StartDate = DateTime.Parse("2023-07-12"),
                EndDate = DateTime.Parse("2023-09-12"),
                SendDocsType = "send_doc_type_1",
                Comment = "contract_comment_1",
                IsAutorenewSchedules = true,
                UserId = 15,
                UserName = "Test Data Generator",
                AttachedContainers = new List<ExtCreateContainer2ScheduleRequest>
                {
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_1_stand_1_contract_1")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = new List<ExtContractScheduleCollectionModel>
                        {
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    StepInitialDateSchedule = new ExtStepInitialDateSchedule
                                    {
                                        StepValue = 5.ToString(),
                                        InitialDate = DateTime.Parse("2023-07-15")
                                    },
                                    Crons = new List<string>
                                    {
                                        "0 0 0 1,15 * ? *",
                                        "0 0 0 ? * MON *"
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 ? * 5L *" //последний четверг месяца
                                    },
                                    ScheduleDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-07-16"),
                                        DateTime.Parse("2023-09-12")
                                    },
                                    ExcludeDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-12-19")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.Ignore,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Work
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-01"),
                                ScheduleDateFrom = DateTime.Parse("2023-06-15"),
                                ScheduleDateTo = DateTime.Parse("2023-08-19")
                            },
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    StepInitialDateSchedule = new ExtStepInitialDateSchedule
                                    {
                                        StepValue = 5.ToString(),
                                        InitialDate = DateTime.Parse("2023-07-15")
                                    },
                                    Crons = new List<string>
                                    {
                                        "0 0 0 ? * 1#3 *", //третье воскресенье месяца
                                        "0 0 0 L-1 * ? *" //последний день месяца
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 17,28 * ? *" //последний четверг месяца
                                    },
                                    ScheduleDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-07-16"),
                                        DateTime.Parse("2023-09-12")
                                    },
                                    ExcludeDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-12-19")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.Ignore,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Work
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-02"),
                                ScheduleDateFrom = DateTime.Parse("2023-07-20"),
                                ScheduleDateTo = DateTime.Parse("2023-10-20")
                            }
                        },
                        ScheduleComment = "schedule_comment_1",
                        WorkTimeFrom = "8:30",//DateTime.Parse("2023-12-12").Date.AddHours(8).AddMinutes(30),
                        WorkTimeTo = "18:00",//DateTime.Parse("2023-12-12").Date.AddHours(18).AddMinutes(00),
                        IsInRegistry = false,
                        InRegistryFromDate = string.Empty
                    },
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_2_stand_1_contract_1")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = new List<ExtContractScheduleCollectionModel>
                        {
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    StepInitialDateSchedule = new ExtStepInitialDateSchedule
                                    {
                                        InitialDate = DateTime.Parse("2023-12-13"),
                                        StepValue = null
                                    },
                                    Crons = new List<string>
                                    {
                                        "0 0 0 1 * ? *"
                                    },
                                    #region a lot of schedule and exclude dates
                                    ScheduleDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-06-15"),
                                        DateTime.Parse("2023-06-20"),
                                        DateTime.Parse("2023-06-28"),
                                        DateTime.Parse("2023-07-02"),
                                        DateTime.Parse("2023-07-06"),
                                        DateTime.Parse("2023-07-10"),
                                        DateTime.Parse("2023-07-15"),
                                        DateTime.Parse("2023-07-20"),
                                        DateTime.Parse("2023-07-28"),
                                        DateTime.Parse("2023-07-30"),
                                        DateTime.Parse("2023-08-01"),
                                        DateTime.Parse("2023-08-04"),
                                        DateTime.Parse("2023-08-05"),
                                        DateTime.Parse("2023-08-06"),
                                        DateTime.Parse("2023-08-10"),
                                        DateTime.Parse("2023-08-12"),
                                        DateTime.Parse("2023-08-14"),
                                        DateTime.Parse("2023-08-15"),
                                        DateTime.Parse("2023-08-22"),
                                        DateTime.Parse("2023-08-29"),
                                        DateTime.Parse("2023-09-01"),
                                        DateTime.Parse("2023-09-29"),
                                        DateTime.Parse("2023-10-29"),
                                        DateTime.Parse("2023-11-29"),
                                        DateTime.Parse("2023-12-29"),
                                        DateTime.Parse("2024-08-29"),
                                        DateTime.Parse("2025-08-29"),
                                        DateTime.Parse("2026-08-29"),
                                        DateTime.Parse("2027-08-29"),
                                        DateTime.Parse("2028-08-29"),
                                        DateTime.Parse("2029-08-29"),
                                        DateTime.Parse("2030-08-29"),
                                        DateTime.Parse("2031-08-29"),
                                        DateTime.Parse("2032-08-29"),
                                        DateTime.Parse("2033-08-29"),
                                        DateTime.Parse("2034-08-29"),
                                        DateTime.Parse("2035-08-29"),
                                        DateTime.Parse("2036-08-29"),
                                        DateTime.Parse("2037-08-29"),
                                        DateTime.Parse("2038-08-29"),
                                        DateTime.Parse("2039-08-29"),
                                        DateTime.Parse("2040-08-29"),
                                        DateTime.Parse("2041-08-29"),
                                        DateTime.Parse("2042-08-29"),
                                        DateTime.Parse("2043-08-29"),
                                        DateTime.Parse("2044-08-29"),
                                        DateTime.Parse("2045-08-29"),
                                    },
                                    ExcludeDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-07-15"),
                                        DateTime.Parse("2023-08-20"),
                                        DateTime.Parse("2023-07-28"),
                                        DateTime.Parse("2023-08-02"),
                                        DateTime.Parse("2023-09-06"),
                                        DateTime.Parse("2023-08-10"),
                                        DateTime.Parse("2023-08-15"),
                                        DateTime.Parse("2023-08-20"),
                                        DateTime.Parse("2023-08-28"),
                                        DateTime.Parse("2023-08-30"),
                                        DateTime.Parse("2023-09-01"),
                                        DateTime.Parse("2023-09-04"),
                                        DateTime.Parse("2023-09-05"),
                                        DateTime.Parse("2023-09-06"),
                                        DateTime.Parse("2023-09-10"),
                                        DateTime.Parse("2023-09-12"),
                                        DateTime.Parse("2023-09-14"),
                                        DateTime.Parse("2023-09-15"),
                                        DateTime.Parse("2023-09-22"),
                                        DateTime.Parse("2023-09-29"),
                                        DateTime.Parse("2023-10-01"),
                                        DateTime.Parse("2023-10-29"),
                                        DateTime.Parse("2023-11-29"),
                                        DateTime.Parse("2023-12-29"),
                                        DateTime.Parse("2023-12-29"),
                                        DateTime.Parse("2024-09-29"),
                                        DateTime.Parse("2025-09-29"),
                                        DateTime.Parse("2026-09-29"),
                                        DateTime.Parse("2027-09-29"),
                                        DateTime.Parse("2028-09-29"),
                                        DateTime.Parse("2029-09-29"),
                                        DateTime.Parse("2030-09-29"),
                                        DateTime.Parse("2031-09-29"),
                                        DateTime.Parse("2032-09-29"),
                                        DateTime.Parse("2033-09-29"),
                                        DateTime.Parse("2034-09-29"),
                                        DateTime.Parse("2035-09-29"),
                                        DateTime.Parse("2036-09-29"),
                                        DateTime.Parse("2037-09-29"),
                                        DateTime.Parse("2038-09-29"),
                                        DateTime.Parse("2039-09-29"),
                                        DateTime.Parse("2040-09-29"),
                                        DateTime.Parse("2041-09-29"),
                                        DateTime.Parse("2042-09-29"),
                                        DateTime.Parse("2043-09-29"),
                                        DateTime.Parse("2044-09-29"),
                                        DateTime.Parse("2045-09-29")
                                    },
                                    #endregion
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.NextWorkDay,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Work
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-03"),
                                ScheduleDateFrom = DateTime.Parse("2023-06-15"),
                                ScheduleDateTo = DateTime.Parse("2023-08-19")
                            },
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    StepInitialDateSchedule = new ExtStepInitialDateSchedule
                                    {
                                        InitialDate = DateTime.Parse("2023-05-15"),
                                        StepValue = 7.ToString()
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.Ignore,
                                        WeekendReactionType = ExtNotWorkDayReactionType.NearestWorkingDay
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-04"),
                                ScheduleDateFrom = DateTime.Parse("2023-06-30"),
                                ScheduleDateTo = DateTime.Parse("2023-07-18")
                            }
                        },
                        ScheduleComment = "schedule_comment_2",
                        WorkTimeFrom = "08:30",//DateTime.Parse("2023-12-12").Date.AddHours(8).AddMinutes(30),
                        WorkTimeTo = "18:00", //DateTime.Parse("2023-12-12").Date.AddHours(18).AddMinutes(00),
                        IsInRegistry = true,
                        InRegistryFromDate = "1999-01-01"
                    },
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_3_stand_2_contract_1")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = new List<ExtContractScheduleCollectionModel>
                        {
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    StepInitialDateSchedule = new ExtStepInitialDateSchedule
                                    {
                                        InitialDate = DateTime.Parse("2023-12-13"),
                                        StepValue = ""
                                    },
                                    Crons = new List<string>
                                    {
                                        "0 0 0 22,25,27,28 * ? *", //третье воскресенье месяца
                                        "0 0 0 5 FEB ? *" //последний день месяца
                                    },
                                    ExcludeDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-08-25")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.PrevWorkDat,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Ignore
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-05"),
                                ScheduleDateFrom = DateTime.Parse("2023-08-01"),
                                ScheduleDateTo = DateTime.Parse("2023-11-25")
                            },
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    StepInitialDateSchedule = new ExtStepInitialDateSchedule
                                    {
                                        StepValue = 16.ToString(),
                                        InitialDate = DateTime.Parse("2023-01-15")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.Ignore,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Ignore
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-06"),
                                ScheduleDateFrom = DateTime.Parse("2023-08-01"),
                                ScheduleDateTo = DateTime.Parse("2023-11-25")
                            },
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    StepInitialDateSchedule = new ExtStepInitialDateSchedule
                                    {
                                        InitialDate = DateTime.Parse("2023-12-13"),
                                        StepValue = null
                                    },
                                    Crons = new List<string>
                                    {
                                        "0 0 0 1,5,26,31 FEB,APR,JUL ? *",
                                        "0 0 0 ? * 6#3 *", //3rd friday
                                        "0 0 0 ? * 2#2 *" //second monday
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = false,
                                        RedDateReactionType = ExtNotWorkDayReactionType.Work,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Work
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-07"),
                                ScheduleDateFrom = DateTime.Parse("2023-01-01"),
                                ScheduleDateTo = DateTime.Parse("2023-04-25")
                            }
                        },
                        ScheduleComment = "schedule_comment_3",
                        WorkTimeFrom = "07:30", //DateTime.Parse("2023-12-12").Date.AddHours(7).AddMinutes(30),
                        WorkTimeTo = "19:00", //DateTime.Parse("2023-12-12").Date.AddHours(19).AddMinutes(00),
                        IsInRegistry = false,
                        InRegistryFromDate = null
                    }
                }
            };

            var contract2 = new ExtCreateContractRequest
            {
                ContractNumber = "contract_number_2",
                ContractClientId = contractClients.Where(x => x.name.Contains("Client_2")).Select(x => x.id).FirstOrDefault(),
                CreateDate = DateTime.Parse("2023-08-12"),
                StartDate = DateTime.Parse("2023-08-12"),
                EndDate = DateTime.Parse("2023-09-12"),
                SendDocsType = "send_doc_type_2",
                Comment = "contract_comment_2",
                IsAutorenewSchedules = false,
                UserId = 15,
                UserName = "Test Data Generator",
                AttachedContainers = new List<ExtCreateContainer2ScheduleRequest>
                {
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_4_stand_1_contract_2")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = new List<ExtContractScheduleCollectionModel>
                        {
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    StepInitialDateSchedule = new ExtStepInitialDateSchedule
                                    {
                                        StepValue = 15.ToString(),
                                        InitialDate = DateTime.Parse("1977-07-15")
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 ? * 6L *" //последняя пятница месяца
                                    },
                                    ScheduleDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-07-16"),
                                        DateTime.Parse("2023-09-12")
                                    },
                                    ExcludeDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-12-19")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.Ignore,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Work
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-08"),
                                ScheduleDateFrom = DateTime.Parse("2023-07-10"),
                                ScheduleDateTo = DateTime.Parse("2023-08-13")
                            },
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    StepInitialDateSchedule = new ExtStepInitialDateSchedule
                                    {
                                        InitialDate = DateTime.Parse("2023-12-13"),
                                        StepValue = null
                                    },
                                    Crons = new List<string>
                                    {
                                        "0 0 0 L-1 * ? *" //последний день месяца
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 17,28 * ? *" //последний четверг месяца
                                    },
                                    ScheduleDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-07-16"),
                                        DateTime.Parse("2023-09-12")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.NextWorkDay,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Ignore
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-09"),
                                ScheduleDateFrom = DateTime.Parse("2023-09-03"),
                                ScheduleDateTo = DateTime.Parse("2023-11-20")
                            }
                        },
                        ScheduleComment = "schedule_comment_4",
                        WorkTimeFrom = "06:00", //DateTime.Parse("2023-12-12").Date.AddHours(6).AddMinutes(00),
                        WorkTimeTo = "22:00", //DateTime.Parse("2023-12-12").Date.AddHours(22).AddMinutes(00),
                        IsInRegistry = false,
                        InRegistryFromDate = null
                    },
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_5_stand_3_contract_2")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = new List<ExtContractScheduleCollectionModel>
                        {
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    StepInitialDateSchedule = new ExtStepInitialDateSchedule
                                    {
                                        InitialDate = DateTime.Parse("2023-12-13"),
                                        StepValue = null
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 ? * * *"
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = false,
                                        RedDateReactionType = ExtNotWorkDayReactionType.Ignore,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Ignore
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-10"),
                                ScheduleDateFrom = DateTime.Parse("2023-09-03"),
                                ScheduleDateTo = DateTime.Parse("2023-11-20")
                            },
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    Crons = new List<string>
                                    {
                                        "0 0 0 L-1 * ? *" //последний день месяца
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 17,28 * ? *" //последний четверг месяца
                                    },
                                    ScheduleDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-07-16"),
                                        DateTime.Parse("2023-09-12")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.NextWorkDay,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Ignore
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-11"),
                                ScheduleDateFrom = DateTime.Parse("2023-09-06"),
                                ScheduleDateTo = DateTime.Parse("2023-09-28")
                            }
                        },
                        ScheduleComment = "schedule_comment_5",
                        WorkTimeFrom = "10:12", //DateTime.Parse("2023-12-12").Date.AddHours(10).AddMinutes(12),
                        WorkTimeTo = "21:00", //DateTime.Parse("2023-12-12").Date.AddHours(21).AddMinutes(00),
                        IsInRegistry = true,
                        InRegistryFromDate = "2009-12-12"
                    },
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_6_stand_3_contract_2")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = new List<ExtContractScheduleCollectionModel>
                        {
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 ? * * *"
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = false,
                                        RedDateReactionType = ExtNotWorkDayReactionType.Ignore,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Ignore
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-10"),
                                ScheduleDateFrom = DateTime.Parse("2023-09-03"),
                                ScheduleDateTo = DateTime.Parse("2023-11-20")
                            },
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    Crons = new List<string>
                                    {
                                        "0 0 0 L-1 * ? *" //последний день месяца
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 17,28 * ? *" //последний четверг месяца
                                    },
                                    ScheduleDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-07-16"),
                                        DateTime.Parse("2023-09-12")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.NextWorkDay,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Ignore
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-11"),
                                ScheduleDateFrom = DateTime.Parse("2023-09-06"),
                                ScheduleDateTo = DateTime.Parse("2023-09-28")
                            }
                        },
                        ScheduleComment = "schedule_comment_6",
                        WorkTimeFrom = "08:45", //DateTime.Parse("2023-12-12").Date.AddHours(8).AddMinutes(45),
                        WorkTimeTo = "15:00", //DateTime.Parse("2023-12-12").Date.AddHours(15).AddMinutes(00),
                        IsInRegistry = true,
                        InRegistryFromDate = "2004-11-12"
                    },
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_7_stand_4_contract_2")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = new List<ExtContractScheduleCollectionModel>
                        {
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    ScheduleDates = new List<DateTime>
                                    {
                                        #region A lot of dates
                                        DateTime.Parse("2023-05-01"),
                                        DateTime.Parse("2023-05-02"),
                                        DateTime.Parse("2023-05-03"),
                                        DateTime.Parse("2023-05-04"),
                                        DateTime.Parse("2023-05-05"),
                                        DateTime.Parse("2023-05-06"),
                                        DateTime.Parse("2023-05-07"),
                                        DateTime.Parse("2023-05-08"),
                                        DateTime.Parse("2023-05-09"),
                                        DateTime.Parse("2023-05-10"),
                                        DateTime.Parse("2023-05-11"),
                                        DateTime.Parse("2023-05-12"),
                                        DateTime.Parse("2023-05-13"),
                                        DateTime.Parse("2023-05-14"),
                                        DateTime.Parse("2023-05-15"),
                                        DateTime.Parse("2023-05-16"),
                                        DateTime.Parse("2023-05-17"),
                                        DateTime.Parse("2023-05-18"),
                                        DateTime.Parse("2023-05-19"),
                                        DateTime.Parse("2023-05-20"),
                                        DateTime.Parse("2023-05-21"),
                                        DateTime.Parse("2023-05-22"),
                                        DateTime.Parse("2023-05-23"),
                                        DateTime.Parse("2023-05-24"),
                                        DateTime.Parse("2023-05-25"),
                                        DateTime.Parse("2023-05-26"),
                                        DateTime.Parse("2023-05-27"),
                                        DateTime.Parse("2023-06-01"),
                                        DateTime.Parse("2023-06-02"),
                                        DateTime.Parse("2023-06-03"),
                                        DateTime.Parse("2023-06-04"),
                                        DateTime.Parse("2023-06-05"),
                                        DateTime.Parse("2023-06-06"),
                                        DateTime.Parse("2023-06-07"),
                                        DateTime.Parse("2023-06-08"),
                                        DateTime.Parse("2023-06-09"),
                                        DateTime.Parse("2023-06-10"),
                                        DateTime.Parse("2023-06-11"),
                                        DateTime.Parse("2023-06-12"),
                                        DateTime.Parse("2023-06-13"),
                                        DateTime.Parse("2023-06-14"),
                                        DateTime.Parse("2023-06-15"),
                                        DateTime.Parse("2023-06-16"),
                                        DateTime.Parse("2023-06-17"),
                                        DateTime.Parse("2023-06-18"),
                                        DateTime.Parse("2023-06-19"),
                                        DateTime.Parse("2023-06-20"),
                                        DateTime.Parse("2023-06-21"),
                                        DateTime.Parse("2023-06-22"),
                                        DateTime.Parse("2023-06-23"),
                                        DateTime.Parse("2023-06-24"),
                                        DateTime.Parse("2023-06-25"),
                                        DateTime.Parse("2023-06-26"),
                                        DateTime.Parse("2023-06-27"),
                                        DateTime.Parse("2023-07-01"),
                                        DateTime.Parse("2023-07-02"),
                                        DateTime.Parse("2023-07-03"),
                                        DateTime.Parse("2023-07-04"),
                                        DateTime.Parse("2023-07-05"),
                                        DateTime.Parse("2023-07-06"),
                                        DateTime.Parse("2023-07-07"),
                                        DateTime.Parse("2023-07-08"),
                                        DateTime.Parse("2023-07-09"),
                                        DateTime.Parse("2023-07-10"),
                                        DateTime.Parse("2023-07-11"),
                                        DateTime.Parse("2023-07-12"),
                                        DateTime.Parse("2023-07-13"),
                                        DateTime.Parse("2023-07-14"),
                                        DateTime.Parse("2023-07-15"),
                                        DateTime.Parse("2023-07-16"),
                                        DateTime.Parse("2023-07-17"),
                                        DateTime.Parse("2023-07-18"),
                                        DateTime.Parse("2023-07-19"),
                                        DateTime.Parse("2023-07-20"),
                                        DateTime.Parse("2023-07-21"),
                                        DateTime.Parse("2023-07-22"),
                                        DateTime.Parse("2023-07-23"),
                                        DateTime.Parse("2023-07-24"),
                                        DateTime.Parse("2023-07-25"),
                                        DateTime.Parse("2023-07-26"),
                                        DateTime.Parse("2023-07-27"),
                                        DateTime.Parse("2023-08-01"),
                                        DateTime.Parse("2023-08-02"),
                                        DateTime.Parse("2023-08-03"),
                                        DateTime.Parse("2023-08-04"),
                                        DateTime.Parse("2023-08-05"),
                                        DateTime.Parse("2023-08-06"),
                                        DateTime.Parse("2023-08-07"),
                                        DateTime.Parse("2023-08-08"),
                                        DateTime.Parse("2023-08-09"),
                                        DateTime.Parse("2023-08-10"),
                                        DateTime.Parse("2023-08-11"),
                                        DateTime.Parse("2023-08-12"),
                                        DateTime.Parse("2023-08-13"),
                                        DateTime.Parse("2023-08-14"),
                                        DateTime.Parse("2023-08-15"),
                                        DateTime.Parse("2023-08-16"),
                                        DateTime.Parse("2023-08-17"),
                                        DateTime.Parse("2023-08-18"),
                                        DateTime.Parse("2023-08-19"),
                                        DateTime.Parse("2023-08-20"),
                                        DateTime.Parse("2023-08-21"),
                                        DateTime.Parse("2023-08-22"),
                                        DateTime.Parse("2023-08-23"),
                                        DateTime.Parse("2023-08-24"),
                                        DateTime.Parse("2023-08-25"),
                                        DateTime.Parse("2023-08-26"),
                                        DateTime.Parse("2023-08-27"),
                                        DateTime.Parse("2023-09-01"),
                                        DateTime.Parse("2023-09-02"),
                                        DateTime.Parse("2023-09-03"),
                                        DateTime.Parse("2023-09-04"),
                                        DateTime.Parse("2023-09-05"),
                                        DateTime.Parse("2023-09-06"),
                                        DateTime.Parse("2023-09-07"),
                                        DateTime.Parse("2023-09-08"),
                                        DateTime.Parse("2023-09-09"),
                                        DateTime.Parse("2023-09-10"),
                                        DateTime.Parse("2023-09-11"),
                                        DateTime.Parse("2023-09-12"),
                                        DateTime.Parse("2023-09-13"),
                                        DateTime.Parse("2023-09-14"),
                                        DateTime.Parse("2023-09-15"),
                                        DateTime.Parse("2023-09-16"),
                                        DateTime.Parse("2023-09-17"),
                                        DateTime.Parse("2023-09-18"),
                                        DateTime.Parse("2023-09-19"),
                                        DateTime.Parse("2023-09-20"),
                                        DateTime.Parse("2023-09-21"),
                                        DateTime.Parse("2023-09-22"),
                                        DateTime.Parse("2023-09-23"),
                                        DateTime.Parse("2023-09-24"),
                                        DateTime.Parse("2023-09-25"),
                                        DateTime.Parse("2023-09-26"),
                                        DateTime.Parse("2023-09-27"),
                                        DateTime.Parse("2023-05-01"),
                                        DateTime.Parse("2023-05-02"),
                                        DateTime.Parse("2023-05-03"),
                                        DateTime.Parse("2023-05-04"),
                                        DateTime.Parse("2023-05-05"),
                                        DateTime.Parse("2023-05-06"),
                                        DateTime.Parse("2023-05-07"),
                                        DateTime.Parse("2023-05-08"),
                                        DateTime.Parse("2023-05-09"),
                                        DateTime.Parse("2023-05-10"),
                                        DateTime.Parse("2023-05-11"),
                                        DateTime.Parse("2023-05-12"),
                                        DateTime.Parse("2023-05-13"),
                                        DateTime.Parse("2023-05-14"),
                                        DateTime.Parse("2023-05-15"),
                                        DateTime.Parse("2023-05-16"),
                                        DateTime.Parse("2023-05-17"),
                                        DateTime.Parse("2023-05-18"),
                                        DateTime.Parse("2023-05-19"),
                                        DateTime.Parse("2023-05-20"),
                                        DateTime.Parse("2023-05-21"),
                                        DateTime.Parse("2023-05-22"),
                                        DateTime.Parse("2023-05-23"),
                                        DateTime.Parse("2023-05-24"),
                                        DateTime.Parse("2023-05-25"),
                                        DateTime.Parse("2023-05-26"),
                                        DateTime.Parse("2023-05-27"),
                                        DateTime.Parse("2023-06-01"),
                                        DateTime.Parse("2023-06-02"),
                                        DateTime.Parse("2023-06-03"),
                                        DateTime.Parse("2023-06-04"),
                                        DateTime.Parse("2023-06-05"),
                                        DateTime.Parse("2023-06-06"),
                                        DateTime.Parse("2023-06-07"),
                                        DateTime.Parse("2023-06-08"),
                                        DateTime.Parse("2023-06-09"),
                                        DateTime.Parse("2023-06-10"),
                                        DateTime.Parse("2023-06-11"),
                                        DateTime.Parse("2023-06-12"),
                                        DateTime.Parse("2023-06-13"),
                                        DateTime.Parse("2023-06-14"),
                                        DateTime.Parse("2023-06-15"),
                                        DateTime.Parse("2023-06-16"),
                                        DateTime.Parse("2023-06-17"),
                                        DateTime.Parse("2023-06-18"),
                                        DateTime.Parse("2023-06-19"),
                                        DateTime.Parse("2023-06-20"),
                                        DateTime.Parse("2023-06-21"),
                                        DateTime.Parse("2023-06-22"),
                                        DateTime.Parse("2023-06-23"),
                                        DateTime.Parse("2023-06-24"),
                                        DateTime.Parse("2023-06-25"),
                                        DateTime.Parse("2023-06-26"),
                                        DateTime.Parse("2023-06-27"),
                                        DateTime.Parse("2023-07-01"),
                                        DateTime.Parse("2023-07-02"),
                                        DateTime.Parse("2023-07-03"),
                                        DateTime.Parse("2023-07-04"),
                                        DateTime.Parse("2023-07-05"),
                                        DateTime.Parse("2023-07-06"),
                                        DateTime.Parse("2023-07-07"),
                                        DateTime.Parse("2023-07-08"),
                                        DateTime.Parse("2023-07-09"),
                                        DateTime.Parse("2023-07-10"),
                                        DateTime.Parse("2023-07-11"),
                                        DateTime.Parse("2023-07-12"),
                                        DateTime.Parse("2023-07-13"),
                                        DateTime.Parse("2023-07-14"),
                                        DateTime.Parse("2023-07-15"),
                                        DateTime.Parse("2023-07-16"),
                                        DateTime.Parse("2023-07-17"),
                                        DateTime.Parse("2023-07-18"),
                                        DateTime.Parse("2023-07-19"),
                                        DateTime.Parse("2023-07-20"),
                                        DateTime.Parse("2023-07-21"),
                                        DateTime.Parse("2023-07-22"),
                                        DateTime.Parse("2023-07-23"),
                                        DateTime.Parse("2023-07-24"),
                                        DateTime.Parse("2023-07-25"),
                                        DateTime.Parse("2023-07-26"),
                                        DateTime.Parse("2023-07-27"),
                                        DateTime.Parse("2023-08-01"),
                                        DateTime.Parse("2023-08-02"),
                                        DateTime.Parse("2023-08-03"),
                                        DateTime.Parse("2023-08-04"),
                                        DateTime.Parse("2023-08-05"),
                                        DateTime.Parse("2023-08-06"),
                                        DateTime.Parse("2023-08-07"),
                                        DateTime.Parse("2023-08-08"),
                                        DateTime.Parse("2023-08-09"),
                                        DateTime.Parse("2023-08-10"),
                                        DateTime.Parse("2023-08-11"),
                                        DateTime.Parse("2023-08-12"),
                                        DateTime.Parse("2023-08-13"),
                                        DateTime.Parse("2023-08-14"),
                                        DateTime.Parse("2023-08-15"),
                                        DateTime.Parse("2023-08-16"),
                                        DateTime.Parse("2023-08-17"),
                                        DateTime.Parse("2023-08-18"),
                                        DateTime.Parse("2023-08-19"),
                                        DateTime.Parse("2023-08-20"),
                                        DateTime.Parse("2023-08-21"),
                                        DateTime.Parse("2023-08-22"),
                                        DateTime.Parse("2023-08-23"),
                                        DateTime.Parse("2023-08-24"),
                                        DateTime.Parse("2023-08-25"),
                                        DateTime.Parse("2023-08-26"),
                                        DateTime.Parse("2023-08-27"),
                                        DateTime.Parse("2023-09-01"),
                                        DateTime.Parse("2023-09-02"),
                                        DateTime.Parse("2023-09-03"),
                                        DateTime.Parse("2023-09-04"),
                                        DateTime.Parse("2023-09-05"),
                                        DateTime.Parse("2023-09-06"),
                                        DateTime.Parse("2023-09-07"),
                                        DateTime.Parse("2023-09-08"),
                                        DateTime.Parse("2023-09-09"),
                                        DateTime.Parse("2023-09-10"),
                                        DateTime.Parse("2023-09-11"),
                                        DateTime.Parse("2023-09-12"),
                                        DateTime.Parse("2023-09-13"),
                                        DateTime.Parse("2023-09-14"),
                                        DateTime.Parse("2023-09-15"),
                                        DateTime.Parse("2023-09-16"),
                                        DateTime.Parse("2023-09-17"),
                                        DateTime.Parse("2023-09-18"),
                                        DateTime.Parse("2023-09-19"),
                                        DateTime.Parse("2023-09-20"),
                                        DateTime.Parse("2023-09-21"),
                                        DateTime.Parse("2023-09-22"),
                                        DateTime.Parse("2023-09-23"),
                                        DateTime.Parse("2023-09-24"),
                                        DateTime.Parse("2023-09-25"),
                                        DateTime.Parse("2023-09-26"),
                                        DateTime.Parse("2023-09-27"),
                                        DateTime.Parse("2024-05-01"),
                                        DateTime.Parse("2024-05-02"),
                                        DateTime.Parse("2024-05-03"),
                                        DateTime.Parse("2024-05-04"),
                                        DateTime.Parse("2024-05-05"),
                                        DateTime.Parse("2024-05-06"),
                                        DateTime.Parse("2024-05-07"),
                                        DateTime.Parse("2024-05-08"),
                                        DateTime.Parse("2024-05-09"),
                                        DateTime.Parse("2024-05-10"),
                                        DateTime.Parse("2024-05-11"),
                                        DateTime.Parse("2024-05-12"),
                                        DateTime.Parse("2024-05-13"),
                                        DateTime.Parse("2024-05-14"),
                                        DateTime.Parse("2024-05-15"),
                                        DateTime.Parse("2024-05-16"),
                                        DateTime.Parse("2024-05-17"),
                                        DateTime.Parse("2024-05-18"),
                                        DateTime.Parse("2024-05-19"),
                                        DateTime.Parse("2024-05-20"),
                                        DateTime.Parse("2024-05-21"),
                                        DateTime.Parse("2024-05-22"),
                                        DateTime.Parse("2024-05-23"),
                                        DateTime.Parse("2024-05-24"),
                                        DateTime.Parse("2024-05-25"),
                                        DateTime.Parse("2024-05-26"),
                                        DateTime.Parse("2024-05-27"),
                                        DateTime.Parse("2024-06-01"),
                                        DateTime.Parse("2024-06-02"),
                                        DateTime.Parse("2024-06-03"),
                                        DateTime.Parse("2024-06-04"),
                                        DateTime.Parse("2024-06-05"),
                                        DateTime.Parse("2024-06-06"),
                                        DateTime.Parse("2024-06-07"),
                                        DateTime.Parse("2024-06-08"),
                                        DateTime.Parse("2024-06-09"),
                                        DateTime.Parse("2024-06-10"),
                                        DateTime.Parse("2024-06-11"),
                                        DateTime.Parse("2024-06-12"),
                                        DateTime.Parse("2024-06-13"),
                                        DateTime.Parse("2024-06-14"),
                                        DateTime.Parse("2024-06-15"),
                                        DateTime.Parse("2024-06-16"),
                                        DateTime.Parse("2024-06-17"),
                                        DateTime.Parse("2024-06-18"),
                                        DateTime.Parse("2024-06-19"),
                                        DateTime.Parse("2024-06-20"),
                                        DateTime.Parse("2024-06-21"),
                                        DateTime.Parse("2024-06-22"),
                                        DateTime.Parse("2024-06-23"),
                                        DateTime.Parse("2024-06-24"),
                                        DateTime.Parse("2024-06-25"),
                                        DateTime.Parse("2024-06-26"),
                                        DateTime.Parse("2024-06-27"),
                                        DateTime.Parse("2024-07-01"),
                                        DateTime.Parse("2024-07-02"),
                                        DateTime.Parse("2024-07-03"),
                                        DateTime.Parse("2024-07-04"),
                                        DateTime.Parse("2024-07-05"),
                                        DateTime.Parse("2024-07-06"),
                                        DateTime.Parse("2024-07-07"),
                                        DateTime.Parse("2024-07-08"),
                                        DateTime.Parse("2024-07-09"),
                                        DateTime.Parse("2024-07-10"),
                                        DateTime.Parse("2024-07-11"),
                                        DateTime.Parse("2024-07-12"),
                                        DateTime.Parse("2024-07-13"),
                                        DateTime.Parse("2024-07-14"),
                                        DateTime.Parse("2024-07-15"),
                                        DateTime.Parse("2024-07-16"),
                                        DateTime.Parse("2024-07-17"),
                                        DateTime.Parse("2024-07-18"),
                                        DateTime.Parse("2024-07-19"),
                                        DateTime.Parse("2024-07-20"),
                                        DateTime.Parse("2024-07-21"),
                                        DateTime.Parse("2024-07-22"),
                                        DateTime.Parse("2024-07-23"),
                                        DateTime.Parse("2024-07-24"),
                                        DateTime.Parse("2024-07-25"),
                                        DateTime.Parse("2024-07-26"),
                                        DateTime.Parse("2024-07-27"),
                                        DateTime.Parse("2024-08-01"),
                                        DateTime.Parse("2024-08-02"),
                                        DateTime.Parse("2024-08-03"),
                                        DateTime.Parse("2024-08-04"),
                                        DateTime.Parse("2024-08-05"),
                                        DateTime.Parse("2024-08-06"),
                                        DateTime.Parse("2024-08-07"),
                                        DateTime.Parse("2024-08-08"),
                                        DateTime.Parse("2024-08-09"),
                                        DateTime.Parse("2024-08-10"),
                                        DateTime.Parse("2024-08-11"),
                                        DateTime.Parse("2024-08-12"),
                                        DateTime.Parse("2024-08-13"),
                                        DateTime.Parse("2024-08-14"),
                                        DateTime.Parse("2024-08-15"),
                                        DateTime.Parse("2024-08-16"),
                                        DateTime.Parse("2024-08-17"),
                                        DateTime.Parse("2024-08-18"),
                                        DateTime.Parse("2024-08-19"),
                                        DateTime.Parse("2024-08-20"),
                                        DateTime.Parse("2024-08-21"),
                                        DateTime.Parse("2024-08-22"),
                                        DateTime.Parse("2024-08-23"),
                                        DateTime.Parse("2024-08-24"),
                                        DateTime.Parse("2024-08-25"),
                                        DateTime.Parse("2024-08-26"),
                                        DateTime.Parse("2024-08-27"),
                                        DateTime.Parse("2024-09-01"),
                                        DateTime.Parse("2024-09-02"),
                                        DateTime.Parse("2024-09-03"),
                                        DateTime.Parse("2024-09-04"),
                                        DateTime.Parse("2024-09-05"),
                                        DateTime.Parse("2024-09-06"),
                                        DateTime.Parse("2024-09-07"),
                                        DateTime.Parse("2024-09-08"),
                                        DateTime.Parse("2024-09-09"),
                                        DateTime.Parse("2024-09-10"),
                                        DateTime.Parse("2024-09-11"),
                                        DateTime.Parse("2024-09-12"),
                                        DateTime.Parse("2024-09-13"),
                                        DateTime.Parse("2024-09-14"),
                                        DateTime.Parse("2024-09-15"),
                                        DateTime.Parse("2024-09-16"),
                                        DateTime.Parse("2024-09-17"),
                                        DateTime.Parse("2024-09-18"),
                                        DateTime.Parse("2024-09-19"),
                                        DateTime.Parse("2024-09-20"),
                                        DateTime.Parse("2024-09-21"),
                                        DateTime.Parse("2024-09-22"),
                                        DateTime.Parse("2024-09-23"),
                                        DateTime.Parse("2024-09-24"),
                                        DateTime.Parse("2024-09-25"),
                                        DateTime.Parse("2024-09-26"),
                                        DateTime.Parse("2024-09-27")
                                        #endregion
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.Work,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Work
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-12"),
                                ScheduleDateFrom = DateTime.Parse("2025-09-03")
                            }
                        },
                        ScheduleComment = "schedule_comment_7",
                        WorkTimeFrom = "16:00", //DateTime.Parse("2023-12-12").Date.AddHours(16).AddMinutes(00),
                        WorkTimeTo = "20:00", //DateTime.Parse("2023-12-12").Date.AddHours(20).AddMinutes(00),
                        IsInRegistry = false,
                        InRegistryFromDate = null
                    }
                }
            };

            var contract3 = new ExtCreateContractRequest
            {
                ContractNumber = "contract_number_3",
                ContractClientId = contractClients.Where(x => x.name.Contains("Client_3")).Select(x => x.id).FirstOrDefault(),
                CreateDate = DateTime.Parse("2023-08-12"),
                StartDate = DateTime.Parse("2023-08-12"),
                EndDate = DateTime.Parse("2023-08-30"),
                SendDocsType = "send_doc_type_3",
                Comment = "contract_comment_3",
                IsAutorenewSchedules = false,
                UserId = 15,
                UserName = "Test Data Generator",
                AttachedContainers = new List<ExtCreateContainer2ScheduleRequest>
                {
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_8_stand_4_contract_3")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = new List<ExtContractScheduleCollectionModel>
                        {
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    Crons = new List<string>
                                    {
                                        "0 0 0 1,20 * ? *"
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 ? * 5L *" //последний четверг месяца
                                    },
                                    ScheduleDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-07-16"),
                                        DateTime.Parse("2023-09-12")
                                    },
                                    ExcludeDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-12-19")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.NearestWorkingDay,
                                        WeekendReactionType = ExtNotWorkDayReactionType.NearestWorkingDay
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-13"),
                                ScheduleDateFrom = DateTime.Parse("2023-04-15"),
                                ScheduleDateTo = DateTime.Parse("2023-12-19")
                            },
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    Crons = new List<string>
                                    {
                                        "0 0 0 L-1 * ? *" //последний день месяца
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 17,28 * ? *" //последний четверг месяца
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.NearestWorkingDay,
                                        WeekendReactionType = ExtNotWorkDayReactionType.NearestWorkingDay
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-14"),
                                ScheduleDateFrom = DateTime.Parse("2023-05-20"),
                                ScheduleDateTo = DateTime.Parse("2023-09-14")
                            }
                        },
                        ScheduleComment = "schedule_comment_8",
                        WorkTimeFrom = "10:00", //DateTime.Parse("2023-12-12").Date.AddHours(10).AddMinutes(00),
                        WorkTimeTo = "16:00", //DateTime.Parse("2023-12-12").Date.AddHours(16).AddMinutes(00),
                        IsInRegistry = false,
                        InRegistryFromDate = null
                    },
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_9_stand_5_contract_3")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = new List<ExtContractScheduleCollectionModel>
                        {
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    StepInitialDateSchedule = new ExtStepInitialDateSchedule
                                    {
                                        InitialDate = DateTime.Parse("2023-12-13"),
                                        StepValue = null
                                    },
                                    Crons = new List<string>
                                    {
                                        "0 0 0 1,20 * ? *"
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 ? * 5L *" //последний четверг месяца
                                    },
                                    ScheduleDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-07-16"),
                                        DateTime.Parse("2023-09-12")
                                    },
                                    ExcludeDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-12-19")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.NearestWorkingDay,
                                        WeekendReactionType = ExtNotWorkDayReactionType.NearestWorkingDay
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-13"),
                                ScheduleDateFrom = DateTime.Parse("2023-04-15"),
                                ScheduleDateTo = DateTime.Parse("2023-12-19")
                            },
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    Crons = new List<string>
                                    {
                                        "0 0 0 L-1 * ? *" //последний день месяца
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 17,28 * ? *" //последний четверг месяца
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.NearestWorkingDay,
                                        WeekendReactionType = ExtNotWorkDayReactionType.NearestWorkingDay
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-14"),
                                ScheduleDateFrom = DateTime.Parse("2023-05-20"),
                                ScheduleDateTo = DateTime.Parse("2023-09-14")
                            }
                        },
                        ScheduleComment = "schedule_comment_9",
                        WorkTimeFrom = "09:50", //DateTime.Parse("2023-12-12").Date.AddHours(9).AddMinutes(50),
                        WorkTimeTo = "23:00", //DateTime.Parse("2023-12-12").Date.AddHours(23).AddMinutes(00),
                        IsInRegistry = true,
                        InRegistryFromDate = "2023-02-03"
                    },
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_10_stand_5_contract_3")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = new List<ExtContractScheduleCollectionModel>
                        {
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    Crons = new List<string>
                                    {
                                        "0 0 0 1,20 * ? *"
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 ? * 5L *" //последний четверг месяца
                                    },
                                    ScheduleDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-07-16"),
                                        DateTime.Parse("2023-09-12")
                                    },
                                    ExcludeDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-12-19")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.NearestWorkingDay,
                                        WeekendReactionType = ExtNotWorkDayReactionType.NearestWorkingDay
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-13"),
                                ScheduleDateFrom = DateTime.Parse("2023-04-15"),
                                ScheduleDateTo = DateTime.Parse("2023-12-19")
                            },
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    Crons = new List<string>
                                    {
                                        "0 0 0 L-1 * ? *" //последний день месяца
                                    },
                                    ExcludeCrons = new List<string>
                                    {
                                        "0 0 0 17,28 * ? *" //последний четверг месяца
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.NearestWorkingDay,
                                        WeekendReactionType = ExtNotWorkDayReactionType.NearestWorkingDay
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-14"),
                                ScheduleDateFrom = DateTime.Parse("2023-05-20"),
                                ScheduleDateTo = DateTime.Parse("2023-09-14")
                            }
                        },
                        ScheduleComment = "schedule_comment_10",
                        WorkTimeFrom = "09:50", //DateTime.Parse("2023-12-12").Date.AddHours(9).AddMinutes(50),
                        WorkTimeTo = "23:00", //DateTime.Parse("2023-12-12").Date.AddHours(23).AddMinutes(00),
                        IsInRegistry = true,
                        InRegistryFromDate = "2023-02-03"
                    },
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_11_stand_6_contract_3")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = new List<ExtContractScheduleCollectionModel>
                        {
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    StepInitialDateSchedule = new ExtStepInitialDateSchedule
                                    {
                                        StepValue = 2.ToString(),
                                        InitialDate = DateTime.Parse("2023-07-15")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.Work,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Work
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-15"),
                                ScheduleDateFrom = DateTime.Parse("2023-06-20"),
                                ScheduleDateTo = DateTime.Parse("2023-08-21")
                            }
                        },
                        ScheduleComment = "schedule_comment_11",
                        WorkTimeFrom = "09:50", //DateTime.Parse("2023-12-12").Date.AddHours(9).AddMinutes(50),
                        WorkTimeTo = "23:00", //DateTime.Parse("2023-12-12").Date.AddHours(23).AddMinutes(00),
                        IsInRegistry = true,
                        InRegistryFromDate = "2020-02-03"
                    },
                    new ExtCreateContainer2ScheduleRequest
                    {
                        ContainerId = containers.Where(x => x.name.Contains("container_12_stand_6_contract_3")).Select(x => x.id).FirstOrDefault(),
                        ContractScheduleCollection = new List<ExtContractScheduleCollectionModel>
                        {
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    ScheduleDates = new List<DateTime>
                                    {
                                        DateTime.Parse("2023-07-16"),
                                        DateTime.Parse("2023-09-12")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.NearestWorkingDay,
                                        WeekendReactionType = ExtNotWorkDayReactionType.NearestWorkingDay
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-13"),
                                ScheduleDateFrom = DateTime.Parse("2023-04-15"),
                                ScheduleDateTo = DateTime.Parse("2023-12-19")
                            },
                            new ExtContractScheduleCollectionModel
                            {
                                ContractSchedule = new ExtContractScheduleModel
                                {
                                    Crons = new List<string>
                                    {
                                        "0 0 0 ? * * *"
                                    },
                                    ExcludeDates=new List<DateTime>
                                    {
                                        DateTime.Parse("2023-04-19"),
                                        DateTime.Parse("2023-04-21")
                                    },
                                    ContractScheduleSettings = new ExtContractScheduleSettings
                                    {
                                        EnableWeekendWorkDates = true,
                                        RedDateReactionType = ExtNotWorkDayReactionType.Ignore,
                                        WeekendReactionType = ExtNotWorkDayReactionType.Ignore
                                    }
                                },
                                //CreateDate = DateTime.Parse("2023-01-14"),
                                ScheduleDateFrom = DateTime.Parse("2023-04-18"),
                                ScheduleDateTo = DateTime.Parse("2023-04-25")
                            }
                        },
                        ScheduleComment = "schedule_comment_12",
                        WorkTimeFrom = "06:50", //DateTime.Parse("2023-12-12").Date.AddHours(6).AddMinutes(50),
                        WorkTimeTo = "17:00", //DateTime.Parse("2023-12-12").Date.AddHours(17).AddMinutes(00),
                        IsInRegistry = false,
                        InRegistryFromDate = null
                    }
                }
            };

            result.Add(contractWithoutPeriods);
            result.Add(contract1);
            result.Add(contract2);
            result.Add(contract3);

            return result;
        }
    }
}
