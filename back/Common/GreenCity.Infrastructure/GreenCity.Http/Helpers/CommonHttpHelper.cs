using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace GreenCity.Http.Helpers
{
    public class CommonHttpHelper
    {
        public static Dictionary<string, string> ModelToQuery<T>(T model)
        {
            if (model == null) return new Dictionary<string, string>();

            return typeof(T).GetProperties().ToDictionary(x => x.Name, y => y.GetValue(model)?.ToString() ?? string.Empty);
        }

        public static CommonHttpResponse BuildErrorResponse(
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            List<string> extErrors = null,
            string initialError = null)
        {
            if (initialError == null) initialError = $"Ошибка при выполнении запроса. Код: {(int)statusCode}";
            var errors = new List<string> { initialError };
            if (extErrors != null)
            {
                errors.AddRange(extErrors);
            }

            return new CommonHttpResponse
            {
                Success = false,
                StatusCode = statusCode,
                Errors = errors.ToArray()
            };
        }

        public static CommonHttpResponse<T> BuildErrorResponse<T>(
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            List<string> extErrors = null,
            string initialError = null)
        {
            if (initialError == null) initialError = $"Ошибка при выполнении запроса. Код: {statusCode}";
            var errors = new List<string> { initialError };
            if (extErrors != null)
            {
                errors.AddRange(extErrors);
            }

            return new CommonHttpResponse<T>
            {
                Success = false,
                Data = default(T),
                StatusCode = statusCode,
                Errors = errors.ToArray()
            };
        }

        public static CommonHttpResponse BuildSuccessResponse(
            HttpStatusCode statusCode = HttpStatusCode.NoContent
        )
        {
            return new CommonHttpResponse
            {
                Success = true,
                StatusCode = statusCode
            };
        }

        public static CommonHttpResponse<T> BuildSuccessResponse<T>(
            T data,
            HttpStatusCode statusCode = HttpStatusCode.NoContent
        )
        {
            return new CommonHttpResponse<T>
            {
                Success = true,
                StatusCode = statusCode,
                Data = data
            };
        }
    }
}
