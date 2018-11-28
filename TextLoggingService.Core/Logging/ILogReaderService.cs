using System.Threading.Tasks;

namespace TextLoggingService.Core.Logging
{
    public interface ILogReaderService
    {
        Task<string> Read();
    }
}