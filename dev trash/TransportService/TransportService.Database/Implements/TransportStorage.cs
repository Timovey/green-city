using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Transport;
using TransportContracts.Filters;
using TransportContracts.ViewModels;
using TransportService.Database.Interfaces;
using TransportService.Database.Models;

namespace TransportService.Database.Implements
{
    public class TransportStorage : ITransportStorage
    {
        private readonly TransportDbContext _context;
        private readonly IMapper _mapper;

        public TransportStorage(TransportDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateTransport(CreateTransportContract createContract)
        {
            var transport = _mapper.Map<Transport>(createContract);

            await _context.AddAsync(transport);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteTransport(int transportId)
        {
            var elem = await _context.Transports.FirstOrDefaultAsync(x => x.Id == transportId);
            if (elem == null)
            {
                return false;
            }

            _context.Transports.Remove(elem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<TransportViewModel>> GetFilteredList(TransportFilterModel filter)
        {
            IQueryable<Transport> query = _context.Transports;
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{filter.Name}%"));
            }

            if (filter.OrganizationId.HasValue)
            {
                query = query.Where(x => x.OrganizationId == filter.OrganizationId.Value);
            }

            if (!string.IsNullOrEmpty(filter.RegNumber))
            {
                query = query.Where(x => EF.Functions.Like(x.RegNumber, $"%{filter.RegNumber}%"));
            }

            if (!string.IsNullOrEmpty(filter.TransportType))
            {
                query = query.Where(x => EF.Functions.Like(x.TransportType, $"%{filter.TransportType}%"));
            }

            if (!string.IsNullOrEmpty(filter.BodyColor))
            {
                query = query.Where(x => EF.Functions.Like(x.BodyColor, $"%{filter.BodyColor}%"));
            }

            if (!string.IsNullOrEmpty(filter.TankFence))
            {
                query = query.Where(x => EF.Functions.Like(x.TankFence, $"%{filter.TankFence}%"));
            }

            if (filter.CargoVolume.HasValue)
            {
                query = query.Where(x => x.OrganizationId == filter.CargoVolume.Value);
            }

            query = query.Skip(filter.Page * filter.ItemsOnPage).Take(filter.ItemsOnPage).OrderBy(x => x.Id);

            return query.Select(_mapper.Map<TransportViewModel>).ToList();
        }

        public async Task<TransportViewModel> GetTransport(int transportId)
        {
            var elem = await _context.Transports.FirstOrDefaultAsync(x => x.Id == transportId);
            if (elem == null)
            {
                return null;
            }

            return _mapper.Map<TransportViewModel>(elem);
        }

        public async Task<bool> UpdateTransport(UpdateTransportContract updateContract)
        {
            var transport = _mapper.Map<Transport>(updateContract);

            var elem = await _context.Transports.FirstOrDefaultAsync(x => x.Id == transport.Id);
            if (elem == null)
            {
                return false;
            }

            _mapper.Map(elem, transport);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
