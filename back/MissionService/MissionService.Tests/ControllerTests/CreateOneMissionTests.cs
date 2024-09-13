using MissionService.Tests.Common;
using MissionService.Tests.TestDataProviders;
using System.Threading.Tasks;
using Xunit;

namespace MissionService.Tests.ControllerTests
{
    public class CreateOneMissionTests : MissionServiceTestsBase
    {
        [Fact]
        public async Task CreateOneMissionTests__CreateOneRealMission__Smoke()
        {
            using(var context = await CreateMissionsTestsDataProvider.GetDbContextForSmokeTest())
            {
                using(var controller = GetMissionController(context))
                {

                }
            }
        }
    }
}
