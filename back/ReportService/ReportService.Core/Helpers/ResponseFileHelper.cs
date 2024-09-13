using GreenCity.Http;
using GreenCity.Http.Helpers;
using ReportService.Contracts.DataContracts;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReportService.Core.Helpers
{
    public static class ResponseFileHelper
    {
        public async static Task<CommonHttpResponse<ResponseFileContract>> GetFileResponseAsync(string filePath, string fileName)
        {
            using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[fileStream.Length];
                await fileStream.ReadAsync(buffer, 0, (int)fileStream.Length);
                fileStream.Close();

                //получем расширение файла из названия
                string? lastExt = fileName.Split('.').ToList().LastOrDefault();
                if (string.IsNullOrEmpty(lastExt))
                {
                    return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(
                        HttpStatusCode.InternalServerError,
                        new List<string>() { $"Extension not found in file {fileName}"},
                       $"Ошибка выполнения метода {nameof(GetFileResponseAsync)}");

                }
                var result = new ResponseFileContract
                {
                    Buffer = buffer,
                    Extension = $"application/{lastExt}",
                    Filename = fileName,
                };
                return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
            }
        }
    }
}
