using Microsoft.EntityFrameworkCore;
using MissionService.Database;
using System;

namespace MissionService.Tests.TestDataProviders
{
    internal class MissionInfoTestsDataProviderBase
    {
        public static MissionDbContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder<MissionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new MissionDbContext(options);
        }
    }
}
