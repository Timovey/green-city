using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Extensions;
using GreenCity.Http.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GreenCityTests.Tools
{
    internal class ApiClient
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public static async Task<CommonHttpResponse> PostAsync<T1>(
            string reqUrl,
            T1 dataToSend,
            string token = null,
            Encoding encoding = null,
            string mediaType = "application/json"
        )
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Settings.InUseUrl);

            //if (token != null)
            //    httpClient.SetToken(token);

            try
            {
                if (encoding == null) encoding = Encoding.UTF8;
                var json = JsonSerializer.Serialize(dataToSend);
                var data = new StringContent(json, encoding, mediaType);
                var createResult = await httpClient.PostAsync(reqUrl, data);

                if (createResult.IsSuccessStatusCode)
                {
                    var content = await createResult.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<CommonHttpResponse>(content, _jsonOptions);
                    return result;
                }

                return CommonHttpHelper.BuildErrorResponse(createResult.StatusCode);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse(HttpStatusCode.InternalServerError, ex.ToExceptionDetails());
            }
        }

        public static async Task<CommonHttpResponse<T2>> PostAsync<T1, T2>(
            string reqUrl,
            T1 dataToSend,
            string token = null,
            Encoding encoding = null,
            string mediaType = "application/json"
        )
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Settings.InUseUrl);

            //if (token != null)
            //    httpClient.SetToken(token);

            try
            {
                if (encoding == null) encoding = Encoding.UTF8;
                var json = JsonSerializer.Serialize(dataToSend);
                var data = new StringContent(json, encoding, mediaType);
                var createResult = await httpClient.PostAsync(reqUrl, data);

                if (createResult.IsSuccessStatusCode)
                {
                    var content = await createResult.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<CommonHttpResponse<T2>>(content, _jsonOptions);
                    return result;
                }

                return CommonHttpHelper.BuildErrorResponse<T2>(createResult.StatusCode);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<T2>(HttpStatusCode.InternalServerError, ex.ToExceptionDetails());
            }
        }

    }
}
