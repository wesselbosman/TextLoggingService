using System.Threading.Tasks;
using TextLoggingService.Core.StorageProviders;

namespace TextLoggingService.Core.Logging
{
    public class TextLogReaderService : ILogReaderService
    {
        private readonly ILogStorageProvider _logStorageProvider;

        public TextLogReaderService(ILogStorageProvider logStorageProvider)
        {
            _logStorageProvider = logStorageProvider;
        }

        public async Task<string> Read()
        {
            return await _logStorageProvider.Read();
        }
    }
}