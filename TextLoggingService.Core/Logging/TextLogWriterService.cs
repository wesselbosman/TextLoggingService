using System.Threading.Tasks;
using TextLoggingService.Core.StorageProviders;

namespace TextLoggingService.Core.Logging
{
    public class TextLogWriterService : ILogWriterService
    {
        private readonly ILogStorageProvider _logStorageProvider;

        public TextLogWriterService(ILogStorageProvider logStorageProvider)
        {
            _logStorageProvider = logStorageProvider;
        }

        public async Task AppendLogMessage(LogModel logModel)
        {
            await _logStorageProvider.Write(logModel.Id, logModel.Date, logModel.Message);
        }
    }
}