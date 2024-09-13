using System.Threading.Tasks;

namespace GreenCity.Logging.DbLogging
{
    public interface IDbLogging
    {
        Task WriteLog(byte logLevel, string message, string category);
    }
}
