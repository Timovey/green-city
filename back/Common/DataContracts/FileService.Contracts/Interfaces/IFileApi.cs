using FileService.Contracts.DataContracts.File;
using GreenCity.Http;
using Refit;

namespace FileService.Contracts.Interfaces
{
    public interface IFileApi
    {

        [Get("/File/GetStaticString")]
        Task<CommonHttpResponse<string>> GetStaticStringAsync();

        [Post("/File/UploadFile")]
        Task<CommonHttpResponse<int>> UploadFileAsync(
           [Body] UploadFileContract contract
       );

        [Post("/File/DownloadFileByName")]
        Task<ResponseFileContract> DownloadFileByNameAsync(
            [Body] DownloadFileByNameContract contract
        );

        [Post("/File/DownloadFileById")]
        Task<ResponseFileContract> DownloadFileByIdAsync(
            [Body] DownloadFileByIdContract contract
        );
    }
}
