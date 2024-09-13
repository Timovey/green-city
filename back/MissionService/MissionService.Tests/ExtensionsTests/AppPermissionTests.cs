using MissionService.Contracts.Enums;
using MissionService.Core.Extensions;
using System;
using Xunit;

namespace MissionService.Tests.ExtensionsTests
{
    public class AppPermissionTests
    {
        [Fact]
        public void AppPermissionTests__CanCreateMissionByTime__WhenUnknown__ThenFalseReturned()
        {
            var validationDate = DateTime.Parse("2022-10-10").AddHours(14).AddMinutes(30);

            var missionDate = DateTime.Parse("2023-10-09").AddHours(23).AddMinutes(59);

            var appPermission = AppActionsPermission.Unknown;

            var actual = appPermission.CanCreateOrChangeMissionByTime(missionDate, validationDate);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AppPermissionTests__CanCreateMissionByTime__WhenAbsolutCreateToPast__ThenFalseReturned()
        {
            var validationDate = DateTime.Parse("2022-10-10").AddHours(14).AddMinutes(30);

            var missionDate = DateTime.Parse("2022-10-09").AddHours(23).AddMinutes(59);

            var appPermission = AppActionsPermission.Absolute;

            var actual = appPermission.CanCreateOrChangeMissionByTime(missionDate, validationDate);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AppPermissionTests__CanCreateMissionByTime__WhenAbsolutCreateToToday__ThenTrueReturned()
        {
            var validationDate = DateTime.Parse("2022-10-10").AddHours(14).AddMinutes(30);

            var missionDate = DateTime.Parse("2022-10-10").AddHours(23).AddMinutes(59);

            var appPermission = AppActionsPermission.Absolute;

            var actual = appPermission.CanCreateOrChangeMissionByTime(missionDate, validationDate);
            var expected = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AppPermissionTests__CanCreateMissionByTime__WhenNotAbsolutCreateTomorrowLessThan10Hours__ThenFalseReturned()
        {
            var validationDate = DateTime.Parse("2022-10-10").AddHours(14).AddMinutes(30);

            var missionDate = DateTime.Parse("2022-10-11").AddHours(23).AddMinutes(59);

            var appPermission = AppActionsPermission.Default;

            var actual = appPermission.CanCreateOrChangeMissionByTime(missionDate, validationDate);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AppPermissionTests__CanCreateMissionByTime__WhenNotAbsolutCreateTomorrowMoreThan10Hours__ThenFalseReturned()
        {
            var validationDate = DateTime.Parse("2022-10-10").AddHours(13).AddMinutes(30);

            var missionDate = DateTime.Parse("2022-10-11").AddHours(23).AddMinutes(59);

            var appPermission = AppActionsPermission.Default;

            var actual = appPermission.CanCreateOrChangeMissionByTime(missionDate, validationDate);
            var expected = true;

            Assert.Equal(expected, actual);
        }
    }
}
