using FileService.Contracts.DataContracts.File;
using FileService.Contracts.ViewModels;

namespace FileService.Database.Interfaces
{
    public interface IFileStorage
    {
        Task<int> CreateFileInStoreAsync(UploadFileContract contract);

        Task<FileViewModel> GetFileInStoreAsync(DownloadFileByIdContract contract);

        Task<bool> IsFileExistAsync(UploadFileContract contract);

        Task<IList<FileViewModel>> GetAllFilesInStoreAsync();
    }
}
