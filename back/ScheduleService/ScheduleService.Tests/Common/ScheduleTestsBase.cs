using GreenCity.Http;
using System.Collections.Generic;
using Xunit;

namespace ScheduleService.Tests.Common
{
    public class ScheduleTestsBase
    {
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
    }
}
