using System.Threading.Tasks;

namespace TextLoggingService.Core.Logging
{
    public interface ILogWriterService
    {
        Task AppendLogMessage(LogModel logModel);
    }
}