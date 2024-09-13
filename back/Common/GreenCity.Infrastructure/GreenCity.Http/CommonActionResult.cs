using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace GreenCity.Http
{
    public class CommonActionResult : IActionResult
    {
        private readonly CommonHttpResponse _result;

        public CommonActionResult(CommonHttpResponse result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
