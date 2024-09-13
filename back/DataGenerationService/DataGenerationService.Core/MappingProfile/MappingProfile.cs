using AutoMapper;
using DataGenerationService.Contracts.DataContracts.Sd;
using GreenCity.CommonUtils.CommonModels;
using SDContracts.DataContracts.Container;
using SDContracts.DataContracts.Contract;
using SDContracts.DataContracts.ContractClient;
using SDContracts.DataContracts.Stand;
using SDContracts.SpecificModels.ContainerStand;
using SDService.Database.Models.DbModels;

namespace DataGenerationService.Core.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ReadContractClientContract, CreateContractClientContract>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(x => x.CreatedUserNumber));
        CreateMap<ReadStandContract, CreateStandContract>()
            .ForMember(dest => dest.Coordinates, opt => opt.MapFrom(m => new StandCoordinatesModel
            {
                Coordinates = new List<Coordinates> { new() { Latitude = m.Latitude, Longitude = m.Longitude } },
                CoordinatesComment = m.CoordinatesComment
            }))
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(x => x.CreatedUserNumber));
        CreateMap<ReadContractContract, CreateContractContract>()
            .ForMember(dest => dest.ContractNumber, opt => opt.MapFrom(x => x.Number))
            .ForMember(dest => dest.ContractClientId, opt => opt.MapFrom(x => x.ContractClientNumber))
            .ForMember(dest => dest.AttachedContainers,
                opt => opt.MapFrom(x => new List<CreateContainer2ScheduleContract>()))
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(x => x.CreatedUserNumber));

        CreateMap<ReadContainerContract, CreateContainerContract>()
            .ForMember(dest => dest.ContainerStandId, opt => opt.MapFrom(x => x.ContainerStandNumber))
            .ForMember(dest => dest.ContainerTypeId, opt => opt.MapFrom(x => x.ContainerTypeNumber))
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(x => x.CreatedUserNumber));
        CreateMap<ReadContainerTypeContract, ContainerType>();

        CreateMap<CreateContainerContract, Container>()
            .ForMember(dest => dest.CreatedUserId,
                opt => opt.MapFrom(x => x.UserId))
            .ForMember(dest => dest.CreatedUserName,
                opt => opt.MapFrom(x => x.UserName));

        CreateMap<CreateContractClientContract, ContractClient>()
            .ForMember(dest => dest.CreatedUserId,
                opt => opt.MapFrom(x => x.UserId))
            .ForMember(dest => dest.CreatedUserName,
                opt => opt.MapFrom(x => x.UserName));

        CreateMap<ContractClient, ContractClientHistory>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());

        CreateMap<CreateStandContract, Stand>()
            .ForMember(dest => dest.CreatedUserId,
                opt => opt.MapFrom(x => x.UserId))
            .ForMember(dest => dest.CreatedUserName,
                opt => opt.MapFrom(x => x.UserName));

        CreateMap<Stand, StandHistory>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());

        CreateMap<CreateContractContract, Contract>()
            .ForMember(dest => dest.CreatedUserId,
                opt => opt.MapFrom(x => x.UserId))
            .ForMember(dest => dest.CreatedUserName,
                opt => opt.MapFrom(x => x.UserName));

        CreateMap<Contract, ContractHistory>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());

        CreateMap<Container, ContainerHistory>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());
    }
}