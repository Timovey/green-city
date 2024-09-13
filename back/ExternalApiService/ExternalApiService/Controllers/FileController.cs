using AuthContracts.Attributres;
using AuthContracts.Constants;
using ExternalApi.Interface.Models.File;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExternalApiService.Controllers
{
    [AuthRoles(AuthRolesConstants.ADMIN)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ModelStateValidationActionFilter]
    public class FileController : Controller
    {
        private readonly IFileOperations _fileOperations;

        public FileController(
            IFileOperations fileOperations
        )
        {
            _fileOperations = fileOperations;
        }

        #region File

        [HttpPost]
        public async Task<CommonHttpResponse<int>> UploadFileAsync(IFormCollection coll)
        {
            return await _fileOperations.UploadFileAsync(coll);
        }

        [HttpPost]
        public async Task<IActionResult> DownloadFileByNameAsync(ExtDownloadFileByNameContract contract)
        {
            var response = await _fileOperations.DownloadFileByNameAsync(contract);
            if (response == null)
            {
                ModelState.AddModelError($"Ошибка выполнения метода {nameof(DownloadFileByNameAsync)}",
                    $"Ошибка при выполнении запроса");
                return BadRequest(ModelState.Values);
            }
            if (response.Errors.Count > 0)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return BadRequest(ModelState.Values);
            }
            else
            {
                return File(response.Buffer, response.Extension, response.Filename);
            }

        }

        [HttpPost]
        public async Task<IActionResult> DownloadFileByIdAsync(ExtDownloadFileByIdContract contract)
        {
            var response = await _fileOperations.DownloadFileByIdAsync(contract);
            if (response == null)
            {
                ModelState.AddModelError($"Ошибка выполнения метода {nameof(DownloadFileByIdAsync)}",
                    $"Ошибка при выполнении запроса");
                return BadRequest(ModelState.Values);
            }
            if (response.Errors.Count > 0)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return BadRequest(ModelState.Values);
            }
            else
            {
                return File(response.Buffer, response.Extension, response.Filename);
            }
        }

        #endregion

    }
}
