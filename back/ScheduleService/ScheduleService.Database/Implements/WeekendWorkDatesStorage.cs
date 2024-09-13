using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScheduleService.Contracts.DataContracts.WeekendWorkDates;
using ScheduleService.Contracts.ViewModels.WeekendWorkDates;
using ScheduleService.Database.Interfaces;
using ScheduleService.Database.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Database.Implements
{
    public class WeekendWorkDatesStorage : IWeekendWorkDatesStorage
    {
        private readonly ScheduleDbContext _context;
        private readonly IMapper _mapper;

        public WeekendWorkDatesStorage(
            ScheduleDbContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateWeekendWorkDateAsync(CreateWeekendWorkDateContract createContract)
        {
            var newWeekendWorkDate = _mapper.Map<WeekendWorkDate>(createContract);
            await _context.WeekendWorkDays.AddAsync(newWeekendWorkDate);
            await _context.SaveChangesAsync();

            return newWeekendWorkDate.Id;
        }

        public async Task<IList<WeekendWorkDateViewModel>> GetActualWeekendWorkDatesAsync()
        {
            var weekendWorkDates = await _context.WeekendWorkDays.Where(x => x.IsDeleted == false).ToListAsync();

            return weekendWorkDates.Select(_mapper.Map<WeekendWorkDateViewModel>).ToList();
        }
    }
}
