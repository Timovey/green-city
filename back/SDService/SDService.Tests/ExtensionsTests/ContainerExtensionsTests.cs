using SDService.Core.Extensions;
using SDService.Database.Models.DbModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SDService.Tests.ExtensionsTests
{
    public class ContainerExtensionsTests
    {
        [Fact]
        public void ContainerExtensionsTests__GetContainerInfoForRoute__SmokeTest()
        {
            var inputData = new List<(Container Container, ContainerType ContainerType)>
            {
                (new Container(), new ContainerType { Id = 1, Volume = 1.1M }),
                (new Container(), new ContainerType { Id = 2, Volume = 3.1M }),
                (new Container(), new ContainerType { Id = 3, Volume = 1.98787M }),
                (new Container(), new ContainerType { Id = 2, Volume = 3.1M }),
                (new Container(), new ContainerType { Id = 1, Volume = 1.1M }),
                (new Container(), new ContainerType { Id = 1, Volume = 1.1M })
            };

            var actualSplitted = inputData.GetContainerInfoForRoute().Split(',').ToList();
            var expectedList = new List<string> { "1.1*3", "3.1*2", "1.98787*1" };

            Assert.True(actualSplitted.Count == 3);

            foreach(var actualElem in actualSplitted)
            {
                Assert.Contains(actualElem, expectedList);
            }
        }
    }
}
