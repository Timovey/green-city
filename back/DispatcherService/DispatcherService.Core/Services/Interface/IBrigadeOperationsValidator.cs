using DispatcherService.Contracts.DataContracts.Brigade;
using DispatcherService.Database.Interfaces;
using System.Threading.Tasks;

namespace DispatcherService.Core.Services.Interface
{
    public interface IBrigadeOperationsValidator
    {
        /// <summary>
        /// Валидация бригады перед созданием
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="brigadeStorage"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, string Error)> ValidateCreateBrigadeAsync(
            CreateBrigadeContract contract,
            IBrigadeStorage brigadeStorage
        );
    }
}
