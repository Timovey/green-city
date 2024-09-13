using AutoMapper;
using SDService.Core.BusinessLogic;
using SDService.Core.MappingProfiles;
using SDService.Database;
using SDService.Database.Implements;
using SDService.Database.MappingProfile;
using SDService.Main.Controllers;
using SDService.Tests.ControllerE2ETests.Common;
using System.Threading.Tasks;
using Xunit;

namespace SDService.Tests.ControllerE2ETests.MIssionInfoTests
{
    public class MissionInfoTestsCommon : SdServiceTestsBase
    {
        private readonly IMapper _dbMapper;
        private readonly IMapper _missionInfoMapper;

        public MissionInfoTestsCommon()
        {
            _dbMapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _missionInfoMapper = new MapperConfiguration(cfg => cfg.AddProfile<MissionInfoMappingProfile>()).CreateMapper();
        }

        [Fact]
        public async Task MissionInfoTests__GetFilteredMissionInfo__SmokeTest()
        {
            //using (var context = await DbContextProviderForSmokeTest.GetDbContextForMissionInfos())
            //{
            //    using (var controller = GetMissionInfoControllerWithDbContext(context))
            //    {
            //        var result = await controller.GetFilteredMissionInfoAsync();

            //        AssertSuccessNotNull(result);

            //        //!_! Должны вернуться все 5 контейнеров
            //        Assert.Equal(5, result.Data.Count());

            //        var searchDuplicates = result.Data
            //            .GroupBy(x => x.ContainerId)
            //            .Select(x => x.ToList())
            //            .Where(x => x.Count != 1)
            //            .ToList();

            //        //!_! Никаких дублей быть не должно, на каждый контейнер ровно одна строчка
            //        Assert.Empty(searchDuplicates);

            //        var firstInfo = result.Data
            //            .Where(x => x.ContainerId == 1)
            //            .Single();

            //        //!_! 1ый контейнер фигурирует в двух договорах с разными КА
            //        Assert.Equal(2, firstInfo.ContractClientNames.Count);
            //    }
            //}
        }

        [Fact]
        public async Task MissionInfoTests__GetFilteredMissionInfo__SmokeFilterTest()
        {
            //var filterDate = DateTime.Parse("2022-10-11");
            //var filterContractClients = new List<int> { 2, 3 };
            //var filterContracts = new List<int> { 1, 2 };
            //var filterStands = new List<int> { 2, 3 };
            //var filterContainerTypes = new List<int> { 1, 3 };
            //var filterContainers = new List<int> { 1, 5, 6 };

            //using (var context = await DbContextProviderForFilterTest.GetDbContextForMissionInfos())
            //{
            //    using (var controller = GetMissionInfoControllerWithDbContext(context))
            //    {
            //        var filterResult = await controller.GetFilteredMissionInfoAsync(
            //            contractClientIds: filterContractClients,
            //            standIds: filterStands,
            //            containerIds: filterContainers,
            //            containerTypeIds: filterContainerTypes,
            //            contractIds: filterContracts,
            //            date: filterDate
            //        );

            //        AssertSuccessNotNull(filterResult);

            //        Assert.Equal(2, filterResult.Data.Count());

            //        var searchBadMissionInfoByExistingInfo = filterResult.Data
            //            .Where(x =>
            //            !new List<int> { 5, 6 }.Contains(x.ContainerId) ||
            //            x.StandInfo == default || !new List<int> { 2, 3 }.Contains(x.StandInfo.StandId) ||
            //            x.ContractClientNames.Intersect(new List<string> { "Client name 1" }).Any() ||
            //            !new List<int> { 1, 3 }.Contains(x.ContainerTypeId));

            //        Assert.Empty(searchBadMissionInfoByExistingInfo);
            //    }
            //}
        }

        private MissionInfoController GetMissionInfoControllerWithDbContext(SdDbContext sdDbContext)
        {
            var missionInfoStorage = new MissionInfoStorage(sdDbContext, _dbMapper, default);
            var missionInfoLogic = new MissionInfoLogic(missionInfoStorage, _missionInfoMapper);
            return new MissionInfoController(missionInfoLogic);
        }
    }
}
