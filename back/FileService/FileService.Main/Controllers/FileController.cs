using Microsoft.AspNetCore.Mvc;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using FileService.Contracts.DataContracts.File;
using FileService.Core.BusinessLogic;
using FileService.Contracts.Interfaces;

namespace FileService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class FileController : Controller, IFileApi
    {
        private readonly FileLogic _fileLogic;
        public FileController(FileLogic fileLogic)
        {
            _fileLogic = fileLogic;
        }

        [HttpPost]
        public async Task<ResponseFileContract> DownloadFileByNameAsync(DownloadFileByNameContract contract)
        {
            return await _fileLogic.DownloadFileByNameAsync(contract);
        }
        [HttpPost]
        public async Task<ResponseFileContract> DownloadFileByIdAsync(DownloadFileByIdContract contract)
        {
            return await _fileLogic.DownloadFileByIdAsync(contract);
        }
        [HttpPost]
        public async Task<CommonHttpResponse<int>> UploadFileAsync(UploadFileContract contract)
        {
            return await _fileLogic.UploadFileAsync(contract);
        }

        [HttpGet]
        public async Task<CommonHttpResponse<string>> GetStaticStringAsync()
        {
            return await _fileLogic.GetStaticStringAsync();
        }
    }
}
