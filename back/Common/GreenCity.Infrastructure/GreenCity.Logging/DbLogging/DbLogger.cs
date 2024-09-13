using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace GreenCity.Logging.DbLogging
{
    public class DbLogger : ILogger, IDisposable
    {
        private readonly IDbLogging _dbLogging;
        private readonly string _categoryName;
        static object _locker = new object();

        public DbLogger(IDbLogging dbLogging, string categoryName)
        {
            _dbLogging = dbLogging;
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose() { }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId,
                    TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            lock (_locker)
            {
                var sb = new StringBuilder(formatter(state, exception) + Environment.NewLine);
                sb.AppendLine(exception?.InnerException?.Message);
                sb.AppendLine(exception?.StackTrace);
                _dbLogging.WriteLog((byte)logLevel, sb.ToString(), _categoryName).GetAwaiter().GetResult();
            }
        }
    }
}
