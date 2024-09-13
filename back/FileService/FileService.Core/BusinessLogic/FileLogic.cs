using AutoMapper;
using FileService.Database.Interfaces;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http.Helpers;
using GreenCity.Http;
using System.Net;
using FileService.Contracts.DataContracts.File;
using FileService.Core.Helpers;
using FileService.Core.Models;
using Microsoft.Extensions.Options;

namespace FileService.Core.BusinessLogic
{
    public class FileLogic
    {
        private readonly IFileStorage _fileStorage;
        private readonly IMapper _mapper;
        private readonly VirtualDriveSettings _settings;

        public FileLogic(
            IFileStorage fileStorage,
            IMapper mapper,
            IOptions<VirtualDriveSettings> options
        )
        {
            _fileStorage = fileStorage;
            _mapper = mapper;
            _settings = options.Value;
        }
        /// <summary>
        /// метод загрузки файла на сервер и добавление в бд имени файла
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<CommonHttpResponse<int>> UploadFileAsync(UploadFileContract contract)
        {
            try
            {
                if (string.IsNullOrEmpty(contract.Filename))
                {
                    return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Нет имени файла");
                }

                if (contract.Buffer == null || contract.Buffer.Length == 0)
                {
                    return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Нет файла в запросе");
                }

                string path = string.IsNullOrEmpty(contract.Folder) ? contract.Filename : Path.Combine(contract.Folder, contract.Filename);

                if (await _fileStorage.IsFileExistAsync(contract))
                {
                    return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Файл с таким именем уже существует");
                }
                //используем "волшебный" класс
                using (NetworkManager manager = new NetworkManager(_settings))
                {

                    if (contract.FileLength > 0)
                    {
                        //строка подключения к виртуальному диску
                        string virtualPath = Path.GetFullPath(Path.Combine(_settings.IP, _settings.Folder));
                        if (!Directory.Exists(virtualPath))
                        {
                            return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Директории виртуального диска не существует");
                        }
                        if (!string.IsNullOrEmpty(contract.Folder) && !Directory.Exists(Path.Combine(virtualPath, contract.Folder)))
                        {
                            Directory.CreateDirectory(Path.Combine(virtualPath, contract.Folder));
                        }
                        using (var fileStream = new FileStream(Path.Combine(virtualPath, path), FileMode.Create))
                        {
                            await fileStream.WriteAsync(contract.Buffer, 0, (int)contract.FileLength);
                            fileStream.Close();
                        }

                        var result = await _fileStorage.CreateFileInStoreAsync(contract);
                        return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
                    }
                    else
                    {
                        return CommonHttpHelper.BuildErrorResponse<int>(initialError: "Файл нулевой длины");
                    }

                }

            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<int>(
                HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(UploadFileAsync)}");
            }

        }

        //в этом проекте можно установить System.Web.Mvc;
        // но не будет работать метод AddModelError и File - для отправки файла
        //поэтому я создал свою модель, а в контреллере ее обрабатываю
        //Этот метод не обращает к БД, я его на всякий случай оставил
        //Он работает напрямую с директорией
        public async Task<ResponseFileContract> DownloadFileByNameAsync(DownloadFileByNameContract contract)
        {
            var response = new ResponseFileContract();
            response.Errors = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(contract.FileName))
            {
                response.Errors.Add($"Ошибка выполнения метода {nameof(DownloadFileByNameAsync)}",
                    $"Введите имя файла");
                return response;
            }
            try
            {
                //используем "волшебный" класс
                using (NetworkManager manager = new NetworkManager(_settings))
                {
                    //строка подключения к виртуальному диску
                    string virtualPath = Path.GetFullPath(Path.Combine(_settings.IP, _settings.Folder));
                    if (!Directory.Exists(virtualPath))
                    {
                        response.Errors.Add($"Ошибка выполнения метода {nameof(DownloadFileByNameAsync)}",
                            $"Директории виртуального диска не существует");
                        return response;
                    }
                    if (!string.IsNullOrEmpty(contract.Folder) && !Directory.Exists(Path.Combine(virtualPath, contract.Folder)))
                    {
                        response.Errors.Add($"Ошибка выполнения метода {nameof(DownloadFileByNameAsync)}",
                            $"Директории из тела запроса не существует");
                        return response;
                    }
                    string filePath = string.IsNullOrEmpty(contract.Folder) ? contract.FileName : Path.Combine(contract.Folder, contract.FileName);
                    string fullPath = Path.Combine(virtualPath, filePath);
                    using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[fileStream.Length];
                        await fileStream.ReadAsync(buffer, 0, (int)fileStream.Length);
                        fileStream.Close();

                        //получем расширение файла из названия
                        string? lastExt = contract.FileName.Split('.').ToList().LastOrDefault();
                        if (!string.IsNullOrEmpty(lastExt))
                        {
                            response.Buffer = buffer;
                            response.Extension = $"application/{lastExt}";
                            response.Filename = contract.FileName;

                            return response;
                        }
                        else
                        {
                            response.Errors.Add($"Ошибка выполнения метода {nameof(DownloadFileByNameAsync)}",
                                $"Нет такого расширения для файла");
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add("Error", ex.Message);
                return response;
            }

        }

        //в этом проекте можно установить System.Web.Mvc;
        // но не будет работать метод AddModelError и File - для отправки файла
        //поэтому я создал свою модель, а в контреллере ее обрабатываю
        public async Task<ResponseFileContract> DownloadFileByIdAsync(DownloadFileByIdContract contract)
        {
            var response = new ResponseFileContract();
            response.Errors = new Dictionary<string, string>();

            try
            {
                var file = await _fileStorage.GetFileInStoreAsync(contract);

                if (file == null)
                {
                    response.Errors.Add($"Ошибка выполнения метода {nameof(DownloadFileByIdAsync)}",
                        $"Нет файла с таким Id");
                    return response;
                }
                //используем "волшебный" класс
                using (NetworkManager manager = new NetworkManager(_settings))
                {
                    //строка подключения к виртуальному диску
                    string path = Path.GetFullPath(Path.Combine(_settings.IP, _settings.Folder));
                    if (!Directory.Exists(path))
                    {
                        response.Errors.Add($"Ошибка выполнения метода {nameof(DownloadFileByIdAsync)}", $"Директории не существует");
                        return response;
                    }
                    string filePath = file.Folder == null ? file.Filename : Path.Combine(file.Folder, file.Filename);
                    string fullPath = Path.Combine(path, filePath);
                    using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[fileStream.Length];
                        await fileStream.ReadAsync(buffer, 0, (int)fileStream.Length);
                        fileStream.Close();

                        //получем расширение файла из названия
                        string? lastExt = file.Filename.Split('.').ToList().LastOrDefault();
                        if (!string.IsNullOrEmpty(lastExt))
                        {
                            response.Buffer = buffer;
                            response.Extension = $"application/{lastExt}";
                            response.Filename = file.Filename;
                            return response;
                        }
                        else
                        {
                            response.Errors.Add($"Ошибка выполнения метода {nameof(DownloadFileByIdAsync)}",
                                $"Нет такого расширения для файла");
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add($"Ошибка выполнения метода {nameof(DownloadFileByIdAsync)}",
                    ex.Message);
                return response;
            }

        }
        public async Task<CommonHttpResponse<string>> GetStaticStringAsync()
        {
            try
            {
                string path = Path.Combine(_settings.IP, _settings.Folder);

                if(string.IsNullOrEmpty(path)) {
                    return CommonHttpHelper.BuildErrorResponse<string>(initialError: "Ошибка при формировании строки подключения к удаленному диску");
                }
                
                return CommonHttpHelper.BuildSuccessResponse(path, HttpStatusCode.OK); 
            }
            catch(Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<string>(
               HttpStatusCode.InternalServerError,
                   ex.ToExceptionDetails(),
                   $"Ошибка выполнения метода {nameof(GetStaticStringAsync)}");
            }
        }
    }
}
