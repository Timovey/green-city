using System;
using System.Threading.Tasks;

namespace GreenCity.Logging.DbLogging
{
    public class DbLogging : IDbLogging
    {
        private readonly LogDbContext _logContext;

        public DbLogging(LogDbContext logContext)
        {
            _logContext = logContext;
        }

        public async Task WriteLog(byte logLevel, string message, string category)
        {
            await _logContext.Logs.AddAsync(
                new LogDbModel
                {
                    LogDate = DateTime.UtcNow,
                    LogLevel = logLevel,
                    Message = message,
                    CategoryName = category
                });

            await _logContext.SaveChangesAsync();
        }
    }
}
