using AutoMapper;
using ExternalApi.Interface.Models.File;
using ExternalApiService.Operations.Interface;
using FileService.Contracts.DataContracts.File;
using FileService.Contracts.Interfaces;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalApiService.Operations
{
    public class FileOperations : IFileOperations
    {
        private readonly IFileApi _fileApi;
        private readonly IMapper _mapper;

        public FileOperations(
            IFileApi fileApi, IMapper mapper
        )
        {
            _fileApi = fileApi;
            _mapper = mapper;
        }
        public async Task<ExtResponseFileContract> DownloadFileByIdAsync(ExtDownloadFileByIdContract contract)
        {
            var fileContract = _mapper.Map<DownloadFileByIdContract>(contract);
            var res = await _fileApi.DownloadFileByIdAsync(fileContract);
            return _mapper.Map<ExtResponseFileContract>(res);
        }

        public async Task<ExtResponseFileContract> DownloadFileByNameAsync(ExtDownloadFileByNameContract contract)
        {
            var fileContract = _mapper.Map<DownloadFileByNameContract>(contract);
            var res =  await _fileApi.DownloadFileByNameAsync(fileContract);
            return _mapper.Map<ExtResponseFileContract>(res);
        }

        public async Task<CommonHttpResponse<int>> UploadFileAsync(IFormCollection coll)
        {
            if (coll == null)
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Пустой запрос");

            
            string folder = coll["folder"];
            var file = coll.Files.FirstOrDefault();

            if (file == null)
            {
                return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Нет файла в запросе");
            }
            var contract = new ExtUploadFileContract() { 
                Filename = file.FileName, 
                Folder = folder,
                FileLength = file.Length,
            };
            byte[] data = new byte[file.Length];
            using(MemoryStream ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                data = ms.ToArray();
            }
            contract.Buffer = data;

            var fileContract = _mapper.Map<UploadFileContract>(contract);

            var result = await _fileApi.UploadFileAsync(fileContract);

            if (result.Success)
            {
                return CommonHttpHelper.BuildSuccessResponse(
                    result.Data,
                    System.Net.HttpStatusCode.OK);
            }

            return CommonHttpHelper.BuildErrorResponse<int>(result.StatusCode, result.Errors.ToList());
        }
    }
}
