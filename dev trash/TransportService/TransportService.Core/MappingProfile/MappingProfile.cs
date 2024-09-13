using AutoMapper;
using TransportContracts.DataContracts.Transport;
using TransportContracts.ViewModels;
using TransportService.Database.Models;

namespace TransportService.Core.MappingProfile
{
    /// <summary>
    /// Класс конфигурации маппера
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<CreateTransportContract, Transport>();
            //CreateMap<UpdateTransportContract, Transport>();
            //CreateMap<Transport, TransportViewModel>();
        }
    }
}
