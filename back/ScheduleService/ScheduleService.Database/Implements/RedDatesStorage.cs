using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScheduleService.Contracts.DataContracts.RedDates;
using ScheduleService.Contracts.ViewModels.RedDates;
using ScheduleService.Database.Interfaces;
using ScheduleService.Database.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Database.Implements
{
    public class RedDatesStorage : IRedDatesStorage
    {
        private readonly ScheduleDbContext _context;
        private readonly IMapper _mapper;

        public RedDatesStorage(
            ScheduleDbContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateRedDateAsync(CreateRedDateContract createContract)
        {
            var newRedDate = _mapper.Map<RedDate>(createContract);
            await _context.RedDates.AddAsync(newRedDate);
            await _context.SaveChangesAsync();

            return newRedDate.Id;
        }

        public async Task<IList<RedDateViewModel>> GetActualRedDatesAsync()
        {
            var redDates = await _context.RedDates.Where(x => x.IsDeleted == false).ToListAsync();

            return redDates.Select(_mapper.Map<RedDateViewModel>).ToList();
        }
    }
}
