using MissionService.Contracts.Enums;
using MissionService.Core.Extensions;
using System.Collections.Generic;
using Xunit;

namespace MissionService.Tests.ExtensionsTests
{
    public class AppMissionStatusesExtensionsTests
    {
        [Fact]
        public void AppMissionStatusesExtensionsTests__GetAppComputedStatus__WhenAllNew__ThenNewStatusReturned()
        {
            var missionStatuses = new List<MissionStatus> { MissionStatus.New, MissionStatus.New, MissionStatus.New };

            var actualComputedStatus = missionStatuses.GetAppComputedStatus();

            var expectedComputedStatus = AppComputedStatus.New;

            Assert.Equal(expectedComputedStatus, actualComputedStatus);
        }

        [Fact]
        public void AppMissionStatusesExtensionsTests__GetAppComputedStatus__WhenHaveOneUnknown__ThenUnknownStatusReturned()
        {
            var missionStatuses = new List<MissionStatus> { MissionStatus.Unknown, MissionStatus.Failed, MissionStatus.Done };

            var actualComputedStatus = missionStatuses.GetAppComputedStatus();

            var expectedComputedStatus = AppComputedStatus.Unknown;

            Assert.Equal(expectedComputedStatus, actualComputedStatus);
        }

        [Fact]
        public void AppMissionStatusesExtensionsTests__GetAppComputedStatus__WhenHaveOneFailed__ThenFailedStatusReturned()
        {
            var missionStatuses = new List<MissionStatus> { MissionStatus.InProgress, MissionStatus.Failed, MissionStatus.Done };

            var actualComputedStatus = missionStatuses.GetAppComputedStatus();

            var expectedComputedStatus = AppComputedStatus.Failed;

            Assert.Equal(expectedComputedStatus, actualComputedStatus);
        }

        [Fact]
        public void AppMissionStatusesExtensionsTests__GetAppComputedStatus__WhenHaveOnlyInProgressAndNew__ThenInProgressStatusReturned()
        {
            var missionStatuses = new List<MissionStatus> { MissionStatus.InProgress, MissionStatus.New, MissionStatus.InProgress };

            var actualComputedStatus = missionStatuses.GetAppComputedStatus();

            var expectedComputedStatus = AppComputedStatus.InProgress;

            Assert.Equal(expectedComputedStatus, actualComputedStatus);
        }

        [Fact]
        public void AppMissionStatusesExtensionsTests__GetAppComputedStatus__WhenHaveOneDoneAndInProgressWithNew__ThenPartiallyDoneStatusReturned()
        {
            var missionStatuses = new List<MissionStatus> { MissionStatus.Done, MissionStatus.New, MissionStatus.InProgress };

            var actualComputedStatus = missionStatuses.GetAppComputedStatus();

            var expectedComputedStatus = AppComputedStatus.PartiallyDone;

            Assert.Equal(expectedComputedStatus, actualComputedStatus);
        }

        [Fact]
        public void AppMissionStatusesExtensionsTests__GetAppComputedStatus__WhenHaveOnlyDone__ThenDoneStatusReturned()
        {
            var missionStatuses = new List<MissionStatus> { MissionStatus.Done, MissionStatus.Done, MissionStatus.Done };

            var actualComputedStatus = missionStatuses.GetAppComputedStatus();

            var expectedComputedStatus = AppComputedStatus.Done;

            Assert.Equal(expectedComputedStatus, actualComputedStatus);
        }

        [Fact]
        public void AppMissionStatusesExtensionsTests__GetAppComputedStatus__WhenHaveOnlyNew__ThenNewStatusReturned()
        {
            var missionStatuses = new List<MissionStatus> { MissionStatus.New, MissionStatus.New, MissionStatus.New };

            var actualComputedStatus = missionStatuses.GetAppComputedStatus();

            var expectedComputedStatus = AppComputedStatus.New;

            Assert.Equal(expectedComputedStatus, actualComputedStatus);
        }

        [Fact]
        public void AppMissionStatusesExtensionsTests__GetAppComputedStatus__WhenHaveOnlyOneNew__ThenNewStatusReturned()
        {
            var missionStatuses = new List<MissionStatus> { MissionStatus.New };

            var actualComputedStatus = missionStatuses.GetAppComputedStatus();

            var expectedComputedStatus = AppComputedStatus.New;

            Assert.Equal(expectedComputedStatus, actualComputedStatus);
        }

        [Fact]
        public void AppMissionStatusesExtensionsTests__GetAppComputedStatus__WhenHaveOnlyOneUnknown__ThenUnknownStatusReturned()
        {
            var missionStatuses = new List<MissionStatus> { MissionStatus.Unknown };

            var actualComputedStatus = missionStatuses.GetAppComputedStatus();

            var expectedComputedStatus = AppComputedStatus.Unknown;

            Assert.Equal(expectedComputedStatus, actualComputedStatus);
        }

        [Fact]
        public void AppMissionStatusesExtensionsTests__GetAppComputedStatus__WhenHaveOnlyOneInProgress__ThenInProgressStatusReturned()
        {
            var missionStatuses = new List<MissionStatus> { MissionStatus.InProgress };

            var actualComputedStatus = missionStatuses.GetAppComputedStatus();

            var expectedComputedStatus = AppComputedStatus.InProgress;

            Assert.Equal(expectedComputedStatus, actualComputedStatus);
        }

        [Fact]
        public void AppMissionStatusesExtensionsTests__GetAppComputedStatus__WhenHaveOnlyOneDone__ThenDoneStatusReturned()
        {
            var missionStatuses = new List<MissionStatus> { MissionStatus.Done };

            var actualComputedStatus = missionStatuses.GetAppComputedStatus();

            var expectedComputedStatus = AppComputedStatus.Done;

            Assert.Equal(expectedComputedStatus, actualComputedStatus);
        }

        [Fact]
        public void AppMissionStatusesExtensionsTests__GetAppComputedStatus__WhenHaveOnlyOneFailed__ThenFailedStatusReturned()
        {
            var missionStatuses = new List<MissionStatus> { MissionStatus.Failed };

            var actualComputedStatus = missionStatuses.GetAppComputedStatus();

            var expectedComputedStatus = AppComputedStatus.Failed;

            Assert.Equal(expectedComputedStatus, actualComputedStatus);
        }
    }
}
