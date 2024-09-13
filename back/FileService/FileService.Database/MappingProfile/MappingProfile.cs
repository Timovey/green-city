using AutoMapper;
using FileService.Contracts.DataContracts.File;
using FileService.Contracts.ViewModels;
using FileService.Database.Models;

namespace FileService.Database.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //----------------------------- Files
            CreateMap<UploadFileContract, FileInStore>();
            CreateMap<FileInStore, FileInStoreHistory>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());
            CreateMap<FileInStore, FileViewModel>()
                .ForMember(dest => dest.FileId,
                    opt => opt.MapFrom(x => x.Id));
        }
    }
}
