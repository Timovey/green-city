using MissionService.Database;
using MissionService.Database.Models.DbModels;
using System;
using System.Threading.Tasks;

namespace MissionService.Tests.TestDataProviders
{
    internal class CreateMissionsTestsDataProvider : MissionInfoTestsDataProviderBase
    {
        public static async Task<MissionDbContext> GetDbContextForSmokeTest()
        {
            var dbContext = GetEmptyDbContext();

            await dbContext.Missions.AddAsync(
                new Mission
                {
                    ContainerId = 1,
                    Date = DateTime.Parse("2022-10-10")
                }
            );

            await dbContext.SaveChangesAsync();

            return dbContext;
        }
    }
}
