using AutoMapper;
using SDService.Core.BusinessLogic;
using SDService.Database;
using SDService.Database.Implements;
using SDService.Database.MappingProfile;
using SDService.Main.Controllers;
using SDService.Tests.ControllerE2ETests.Common;
using SDService.Tests.TestDataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SDService.Tests.ControllerE2ETests.Container2ScheduleTests
{
    public class Container2ScheduleTestsCommon : SdServiceTestsBase
    {
        private readonly IMapper _dbMapper;

        public Container2ScheduleTestsCommon()
        {
            _dbMapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
        }

        [Fact]
        public async Task Container2ScheduleTestsCommon__GetFilteredContainer2Schedules__SmokeTest()
        {
            using (var context = await DbContextProviderForContainer2ScheduleApiTests.GetDbContextForMissionInfos())
            {
                using (var controller = GetMissionInfoControllerWithDbContext(context))
                {
                    var container2Schedules = await controller.GetFilteredContainer2SchedulesForMissionsAsync(new SDContracts.Filter.Container2ScheduleFilterModel
                    {
                        Date = DateTime.Parse("2022-11-13")
                    });

                    AssertSuccessNotNull(container2Schedules);

                    var expectedContracts = new List<int>
                    {
                        1,3,5
                    };

                    //!_! Ищем связки по контрактам
                    var expectedResult = container2Schedules.Data
                        .Where(x => expectedContracts.Contains(x.ContractId))
                        .DistinctBy(x => x.ContractId);

                    Assert.Equal(3, expectedResult.Count());
                }
            }
        }

        private Container2ScheduleController GetMissionInfoControllerWithDbContext(SdDbContext sdDbContext)
        {
            var storage = new Container2ScheduleStorage(sdDbContext, _dbMapper);
            var logic = new Container2ScheduleLogic(storage);
            return new Container2ScheduleController(logic);
        }
    }
}
