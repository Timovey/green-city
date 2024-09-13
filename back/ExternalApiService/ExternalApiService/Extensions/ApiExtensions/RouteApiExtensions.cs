using GreenCity.Http;
using GreenCity.Http.Helpers;
using LogistService.Contracts.DataContracts.Route;
using LogistService.Contracts.Interfaces;
using LogistService.Contracts.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExternalApiService.Extensions.ApiExtensions
{
    internal static class RouteApiExtensions
    {
        /// <summary>
        /// Получить рейс по ИД
        /// </summary>
        /// <typeparam name="TResponseModel"> Тип в commonRespone для создания ошибочного ответа </typeparam>
        /// <param name="routeApi"></param>
        /// <param name="routeId"></param>
        /// <returns></returns>
        internal static async Task<(bool IsSuccess, CommonHttpResponse<TResponseModel> ErrorResponse, RouteViewModel Data)>
            ValidateGetRouteByIdAsync<TResponseModel>(this IRouteApi routeApi, int routeId)
            where TResponseModel : class
        {
            var routeInfoResult = await routeApi.GetRoutesAsync(new GetRouteContract { Ids = new List<int> { routeId } });

            if (!routeInfoResult.Success)
            {
                return (
                    IsSuccess: false,
                    ErrorResponse: CommonHttpHelper.BuildErrorResponse<TResponseModel>(
                        routeInfoResult.StatusCode,
                        routeInfoResult.Errors.GetErrorsOrDefault()
                    ),
                    Data: default
                );
            }

            if (routeInfoResult.Data == default || routeInfoResult.Data.Count > 1)
            {
                return (
                    IsSuccess: false,
                    ErrorResponse: CommonHttpHelper.BuildErrorResponse<TResponseModel>(
                        HttpStatusCode.NotFound,
                        initialError: "Рейс не найден"
                    ),
                    Data: default
                );
            }

            return (true, default, routeInfoResult.Data.Single());
        }
    }
}
