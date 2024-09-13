using GreenCity.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GreenCityTests
{
    public class BaseApiTests
    {
        public void AssertSuccessNotNull<T>(CommonHttpResponse<T> response)
        {
            Assert.True(response.Success);
            Assert.NotNull(response.Data);
        }
        public void AssertSuccess<T>(CommonHttpResponse<T> response)
        {
            Assert.True(response.Success);
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
