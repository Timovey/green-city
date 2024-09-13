using AutoMapper;
using ExternalApi.Interface.Models.File;
using FileService.Contracts.DataContracts.File;
using System;

namespace ExternalApiService.MappingProfiles
{
    internal class FileMappingProfile : Profile
    {
        public FileMappingProfile()
        {
            CreateMap<ExtDownloadFileByNameContract, DownloadFileByNameContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ExtDownloadFileByIdContract, DownloadFileByIdContract>()
                .ForMember(x => x.RequestId, x => x.MapFrom(m => Guid.NewGuid()));
            CreateMap<ResponseFileContract, ExtResponseFileContract> ();
            CreateMap<ExtResponseFileContract, ResponseFileContract>();
            CreateMap<ExtUploadFileContract, UploadFileContract>();
        }

    }
}
