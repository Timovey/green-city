using ExternalApi.Interface.Models.File;
using GreenCity.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ExternalApiService.Operations.Interface
{
    public interface IFileOperations
    {
        Task<CommonHttpResponse<int>> UploadFileAsync(
          IFormCollection coll
      );

        Task<ExtResponseFileContract> DownloadFileByNameAsync(
            ExtDownloadFileByNameContract contract
        );

        Task<ExtResponseFileContract> DownloadFileByIdAsync(
            ExtDownloadFileByIdContract contract
        );
    }
}
