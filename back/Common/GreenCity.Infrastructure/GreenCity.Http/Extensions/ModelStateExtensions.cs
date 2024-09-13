using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace GreenCity.Http.Extensions
{
    public static class ModelStateExtensions
    {
        public static string[] GetErrors(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            foreach (var elem in modelState.Values)
            {
                foreach (var error in elem.Errors)
                {
                    result.Add(error.ErrorMessage);
                }
            }

            return result.ToArray();
        }

        public static CommonHttpResponse GetBadRequestWithErrors(this ModelStateDictionary modelState)
        {
            return new CommonHttpResponse
            {
                Success = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Errors = modelState.GetErrors()
            };
        }

        public static CommonHttpResponse<T> GetBadRequestWithErrors<T>(this ModelStateDictionary modelState)
        {
            return new CommonHttpResponse<T>
            {
                Success = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Errors = modelState.GetErrors()
            };
        }
    }
}
