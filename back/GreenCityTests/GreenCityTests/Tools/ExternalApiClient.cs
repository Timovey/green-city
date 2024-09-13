using ExternalApi.Interface.Models.Auth;
using ExternalApi.Interface.Models.Sd.Container;
using GreenCity.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenCityTests.Tools
{
    internal class ExternalApiClient
    {
        public async static Task<CommonHttpResponse<ExtAuthResult>> Login(ExtAuthRequest request)
        {
            return await ApiClient.PostAsync<ExtAuthRequest, ExtAuthResult>("api/Auth/Login", request);
        }

        public async static Task<CommonHttpResponse<int>> CreateContainer(ExtCreateContainerRequest request, string token)
        {
            return await ApiClient.PostAsync<ExtCreateContainerRequest, int>("api/Sd/CreateContainer", request, token);
        }

        public async static Task<CommonHttpResponse<ExtContainerViewModel[]>> GetContainer(ExtGetContainerRequest request, string token)
        {
            return await ApiClient.PostAsync<ExtGetContainerRequest, ExtContainerViewModel[]>("api/Sd/GetContainer", request, token);
        }

        public async static Task<CommonHttpResponse<ExtContainerViewModel[]>> GetFilteredContainer(ExtContainerFilterRequest request, string token)
        {
            return await ApiClient.PostAsync<ExtContainerFilterRequest, ExtContainerViewModel[]>("api/Sd/GetFilteredContainer", request, token);
        }

        public static async Task<CommonHttpResponse> UpdateContainer(ExtUpdateContainerRequest request, string token)
        {
            return await ApiClient.PostAsync("api/Sd/UpdateContainer", request, token);
        }

        public static async Task<CommonHttpResponse> DeleteContainer(ExtDeleteContainerRequest request, string token)
        {
            return await ApiClient.PostAsync("api/Sd/DeleteContainer", request, token);
        }
    }
}
