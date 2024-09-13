using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.CommonUtils.CommonModels;
using Microsoft.EntityFrameworkCore;
using SDContracts.Enums;
using SDContracts.SpecificModels.ContainerStand;
using SDService.Database;
using SDService.Database.Models.DbModels;
using SDService.Database.Models.DbModels.ContractSchedule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Tests.TestDataProviders.MissionInfo
{
    internal class DbContextProviderForSmokeTest
    {
        public static async Task<SdDbContext> GetDbContextForMissionInfos()
        {
            var options = new DbContextOptionsBuilder<SdDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;

            var context = new SdDbContext(options);

            await context.ContainerTypes.AddRangeAsync(GetTestContainerTypes());
            await context.Containers.AddRangeAsync(GetTestContainers());
            await context.Stands.AddRangeAsync(GetTestStands());
            await context.ContractClients.AddRangeAsync(GetTestContractClients());
            await context.Contracts.AddRangeAsync(GetTestContracts());
            await context.Container2Schedules.AddRangeAsync(GetTestContainer2Schedule());

            await context.SaveChangesAsync();

            return context;
        }

        private static List<ContainerType> GetTestContainerTypes()
        {
            var result = new List<ContainerType>();

            result.Add(new ContainerType
            {
                Name = "Volume 0.9",
                Volume = 0.9M
            });

            result.Add(new ContainerType
            {
                Name = "Volume 0.1",
                Volume = 0.1M
            });

            result.Add(new ContainerType
            {
                Name = "Volume 2.2",
                Volume = 2.2M
            });

            return result;
        }

        private static List<Stand> GetTestStands()
        {
            var result = new List<Stand>();

            result.Add(new Stand
            {
                IsHighSecurityObject = true,
                Address = "address_1",
                CoordinatesJson = (new StandCoordinatesModel
                {
                    Coordinates = new List<Coordinates> { new Coordinates { Longitude = 109.11M, Latitude = 15.90M } },
                    CoordinatesComment = "coordinates_comment_1"
                }).SafeSerialize(),
                DriveWay = "drive_way_1",
                Contacts = "contacts_1",
                CreatedUserId = TestDataProviderConstants.TestUserCreatorId,
                CreatedUserName = TestDataProviderConstants.TestUserCreatorName
            });

            result.Add(new Stand
            {
                IsHighSecurityObject = true,
                Address = "address_2",
                CoordinatesJson = (new StandCoordinatesModel
                {
                    Coordinates = new List<Coordinates> { new Coordinates { Longitude = 111.04M, Latitude = 157.43M } },
                    CoordinatesComment = "coordinates_comment_2"
                }).SafeSerialize(),
                DriveWay = "drive_way_2",
                Contacts = "contacts_2",
                CreatedUserId = TestDataProviderConstants.TestUserCreatorId,
                CreatedUserName = TestDataProviderConstants.TestUserCreatorName
            });

            result.Add(new Stand
            {
                IsHighSecurityObject = true,
                Address = "address_3",
                CoordinatesJson = (new StandCoordinatesModel
                {
                    Coordinates = new List<Coordinates> { new Coordinates { Longitude = 44.11M, Latitude = 77.14M } },
                    CoordinatesComment = "coordinates_comment_2"
                }).SafeSerialize(),
                DriveWay = "drive_way_3",
                Contacts = "contacts_3",
                CreatedUserId = TestDataProviderConstants.TestUserCreatorId,
                CreatedUserName = TestDataProviderConstants.TestUserCreatorName
            });

            return result;
        }

        private static List<Container> GetTestContainers()
        {
            var result = new List<Container>();

            result.Add(new Container
            {
                Name = "container_1",
                ContainerTypeId = 1,
                ContainerStandId = 3, //!_! КП которой нету
                TrashType = (byte)TrashType.TKO,
                CreatedUserId = TestDataProviderConstants.TestUserCreatorId,
                CreatedUserName = TestDataProviderConstants.TestUserCreatorName
            });

            result.Add(new Container
            {
                Name = "container_2",
                ContainerTypeId = 2,
                ContainerStandId = 2,
                TrashType = (byte)TrashType.KGO,
                CreatedUserId = TestDataProviderConstants.TestUserCreatorId,
                CreatedUserName = TestDataProviderConstants.TestUserCreatorName
            });

            result.Add(new Container
            {
                Name = "container_3",
                ContainerTypeId = 2,
                ContainerStandId = 2,
                TrashType = (byte)TrashType.TKO,
                CreatedUserId = TestDataProviderConstants.TestUserCreatorId,
                CreatedUserName = TestDataProviderConstants.TestUserCreatorName
            });

            result.Add(new Container
            {
                Name = "container_4",
                ContainerTypeId = 3,
                ContainerStandId = 3,
                TrashType = (byte)TrashType.TKO,
                CreatedUserId = TestDataProviderConstants.TestUserCreatorId,
                CreatedUserName = TestDataProviderConstants.TestUserCreatorName
            });

            result.Add(new Container
            {
                Name = "container_5",
                ContainerTypeId = 3,
                ContainerStandId = 1,
                TrashType = (byte)TrashType.TKO,
                CreatedUserId = TestDataProviderConstants.TestUserCreatorId,
                CreatedUserName = TestDataProviderConstants.TestUserCreatorName
            });

            return result;
        }

        private static List<ContractClient> GetTestContractClients()
        {
            var result = new List<ContractClient>();

            result.Add(new ContractClient
            {
                ClientName = "Client name 1"
            });

            result.Add(new ContractClient
            {
                ClientName = "Client name 2"
            });

            result.Add(new ContractClient
            {
                ClientName = "Client name 3"
            });

            return result;
        }

        private static List<Contract> GetTestContracts()
        {
            var result = new List<Contract>();

            result.Add(new Contract
            {
                ContractNumber = "contract_1",
                ContractClientId = 1
            });

            result.Add(new Contract
            {
                ContractNumber = "contract_2",
                ContractClientId = 2
            });

            result.Add(new Contract
            {
                ContractNumber = "contract_3",
                ContractClientId = 3
            });

            return result;
        }

        private static List<Container2Schedule> GetTestContainer2Schedule()
        {
            var result = new List<Container2Schedule>();

            result.Add(new Container2Schedule
            {
                ContainerId = 1,
                ContractId = 1
            });

            result.Add(new Container2Schedule
            {
                ContainerId = 1,
                ContractId = 2
            });

            result.Add(new Container2Schedule
            {
                ContainerId = 2,
                ContractId = 2
            });

            result.Add(new Container2Schedule
            {
                ContainerId = 3,
                ContractId = 3
            });

            result.Add(new Container2Schedule
            {
                ContainerId = 4,
                ContractId = 3
            });

            return result;
        }
    }
}
