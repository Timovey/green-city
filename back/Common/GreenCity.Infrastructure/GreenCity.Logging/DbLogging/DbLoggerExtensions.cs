using Microsoft.Extensions.Logging;

namespace GreenCity.Logging.DbLogging
{
    public static class DbLoggerExtensions
    {
        public static ILoggingBuilder AddDatabase(this ILoggingBuilder builder, IDbLogging dbLogging)
        {
            builder.AddProvider(new DbLoggerProvider(dbLogging));
            return builder;
        }
    }
}
