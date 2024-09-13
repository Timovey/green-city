using Microsoft.Extensions.Logging;

namespace GreenCity.Logging.DbLogging
{
    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly IDbLogging _dbLogging;
        public DbLoggerProvider(IDbLogging dbLogging)
        {
            _dbLogging = dbLogging;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(_dbLogging, categoryName);
        }

        public void Dispose() { }
    }
}
