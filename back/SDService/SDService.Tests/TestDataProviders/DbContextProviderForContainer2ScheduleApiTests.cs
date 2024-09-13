using Microsoft.EntityFrameworkCore;
using SDService.Database;
using SDService.Database.Models.DbModels;
using SDService.Database.Models.DbModels.ContractSchedule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDService.Tests.TestDataProviders
{
    public class DbContextProviderForContainer2ScheduleApiTests
    {
        public static async Task<SdDbContext> GetDbContextForMissionInfos()
        {
            var options = new DbContextOptionsBuilder<SdDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;

            var context = new SdDbContext(options);

            await context.Container2Schedules.AddRangeAsync(GetTestsContainer2Schedules());
            await context.Contracts.AddRangeAsync(GetTestContracts());

            await context.SaveChangesAsync();

            return context;
        }

        public static List<Container2Schedule> GetTestsContainer2Schedules()
        {
            return new List<Container2Schedule>
            {
                new Container2Schedule
                {
                    ContractId = 1
                },
                new Container2Schedule
                {
                    ContractId = 2
                },
                new Container2Schedule
                {
                    ContractId = 3
                },
                new Container2Schedule
                {
                    ContractId = 3
                },
                new Container2Schedule
                {
                    ContractId = 4
                },
                new Container2Schedule
                {
                    ContractId = 5
                }
            };
        }

        public static List<Contract> GetTestContracts()
        {
            return new List<Contract>
            {
                new Contract
                {
                    ContractNumber = "contract_1",
                    StartDate = DateTime.Parse("2022-10-10"), //
                    EndDate = DateTime.Parse("2023-01-10"),
                    IsAutorenewSchedules = true
                },
                new Contract
                {
                    ContractNumber = "contract_without_endDate_and_not_autorenew",
                    StartDate = DateTime.Parse("2022-10-10"),
                    EndDate = null,
                    IsAutorenewSchedules = false
                },
                new Contract
                {
                    ContractNumber = "contract_3_with_autorenew", //
                    StartDate = DateTime.Parse("2020-10-10"),
                    EndDate = DateTime.Parse("2021-10-10"),
                    IsAutorenewSchedules = true
                },
                new Contract
                {
                    ContractNumber = "contract_4_with_not_autorenew",
                    StartDate = DateTime.Parse("2022-11-10"),
                    EndDate = DateTime.Parse("2022-11-12"),
                    IsAutorenewSchedules = false
                },
                new Contract
                {
                    ContractNumber = "contract_5_with_autorenew",
                    StartDate = DateTime.Parse("2022-11-10"), //
                    EndDate = null,
                    IsAutorenewSchedules = true
                }
            };
        }
    }
}
