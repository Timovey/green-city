using AutoMapper;
using FileService.Contracts.DataContracts.File;
using FileService.Contracts.ViewModels;
using FileService.Database.Interfaces;
using FileService.Database.Models;
using GreenCity.Db.Services;
using Microsoft.EntityFrameworkCore;

namespace FileService.Database.Implements
{
    public class FileStorage : IFileStorage
    {
        private readonly FileDbContext _context;
        private readonly FillHistoryService _fillHistoryService;
        private readonly IMapper _mapper;

        public FileStorage(FileDbContext context,  IMapper mapper, FillHistoryService fillHistoryService)
        {
            _context = context;
            _mapper = mapper;
            _fillHistoryService = fillHistoryService;
        }

        public async Task<int> CreateFileInStoreAsync(UploadFileContract contract)
        {
            var file = _mapper.Map<FileInStore>(contract);

            await _context.FileInStores.AddAsync(file);
            await _context.SaveChangesAsync();

            await _fillHistoryService.FillHistoryAsync<FileInStore, FileInStoreHistory>(
                new List<FileInStore> { file },
                _context,
                contract.UserId,
                contract.UserName
            );

            return file.Id;
        }

        public async Task<bool> IsFileExistAsync(UploadFileContract contract)
        {
            var data = await _context.FileInStores
             .Where(x => x.IsDeleted == false)
             .Where(x => x.Folder.Equals(contract.Folder) && x.Filename.Equals(contract.Filename))
             .FirstOrDefaultAsync();

            return data != null;
        }

        public async Task<IList<FileViewModel>> GetAllFilesInStoreAsync()
        {
            var data = await _context.FileInStores
              .Where(x => x.IsDeleted == false)
              .OrderByDescending(x => x.Id)
              .AsNoTracking()
              .ToListAsync();

            return data.Select(_mapper.Map<FileViewModel>).ToList();
        }

        public async Task<FileViewModel> GetFileInStoreAsync(DownloadFileByIdContract contract)
        {
            var file = await _context.FileInStores
               .Where(
                   x => x.IsDeleted == false &&
                   x.Id == contract.Id)
               .FirstOrDefaultAsync();

            return _mapper.Map<FileViewModel>(file);
        }
    }
}
