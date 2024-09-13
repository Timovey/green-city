using AutoMapper;
using GreenCity.Db.Services;
using GreenCity.Http;
using Microsoft.Extensions.DependencyInjection;
using MissionService.Core.BusinessLogic;
using MissionService.Core.MappingProfile;
using MissionService.Core.Services;
using MissionService.Database;
using MissionService.Database.MappingProfile;
using MissionService.Database.Storage;
using MissionService.Database.Storage.Interface;
using MissionService.Main.Controllers;
using Moq;
using ScheduleService.Contracts.Interfaces;
using SDContracts.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace MissionService.Tests.Common
{
    public class MissionServiceTestsBase
    {
        protected readonly IMapper _missionDbMapper;
        protected readonly IMapper _missionBusinessLogicMapper;

        public MissionServiceTestsBase()
        {
            _missionDbMapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _missionBusinessLogicMapper = new MapperConfiguration(cfg => cfg.AddProfile<BusinessLogicMappingProfile>()).CreateMapper();
        }

        public void AssertSuccessNotNull<T>(CommonHttpResponse<T> response)
        {
            Assert.True(response.Success);
            Assert.NotNull(response.Data);
        }

        public void AssertSuccessNotEmpty<T>(CommonHttpResponse<IList<T>> response)
        {
            AssertSuccessNotNull(response);
            Assert.NotEmpty(response.Data);
        }

        public void AssertSuccessEmpty<T>(CommonHttpResponse<IList<T>> response)
        {
            AssertSuccessNotNull(response);
            Assert.Empty(response.Data);
        }

        public void AssertFailure<T>(CommonHttpResponse<T> response)
        {
            Assert.False(response.Success);
        }

        private Mock<IServiceProvider> GetNewMockServiceProvider()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            return serviceProvider;
        }

        public MissionController GetMissionController(
            IMissionInfoApi missionInfoApi,
            IContainer2ScheduleApi container2ScheduleApi,
            IScheduleServiceApi scheduleServiceApi,
            MissionDbContext missionDbContext
        )
        {
            var missionStorage = new MissionStorage(missionDbContext, new FillHistoryService(_missionDbMapper), _missionDbMapper);

            var mockServiceProvider = GetNewMockServiceProvider();

            mockServiceProvider
                .Setup(x => x.GetRequiredService<IContainer2ScheduleApi>())
                .Returns(container2ScheduleApi);

            mockServiceProvider
                .Setup(x => x.GetRequiredService<IScheduleServiceApi>())
                .Returns(scheduleServiceApi);

            mockServiceProvider
                .Setup(x => x.GetRequiredService<IMapper>())
                .Returns(_missionBusinessLogicMapper);

            mockServiceProvider
                .Setup(x => x.GetRequiredService<IMissionStorage>())
                .Returns(missionStorage);

            var missionGenerator = new AppGeneratorSingletonService(mockServiceProvider.Object);
            MissionLogic missionLogic = default;//new MissionLogic(missionInfoApi, missionStorage, _missionBusinessLogicMapper/*, missionGenerator*/);
            return new MissionController(missionLogic);
        }

        public MissionController GetMissionController(
            MissionDbContext missionDbContext
        )
        {
            var missionStorage = new MissionStorage(missionDbContext, new FillHistoryService(_missionDbMapper), _missionDbMapper);

            AppGeneratorSingletonService? missionGenerator = null;
            IMissionInfoApi? missionInfoApi = null;

            MissionLogic missionLogic = default;//new MissionLogic(missionInfoApi, missionStorage, _missionBusinessLogicMapper/*, missionGenerator*/);
            return new MissionController(missionLogic);
        }
    }
}
