using System.Threading.Tasks;

namespace TextLoggingService.Core.StorageProviders
{
    public class TextLogStorageProvider : ILogStorageProvider
    {
        public Task Write()
        {
            return Task.CompletedTask;
        }

        public Task<string> Read()
        {
            return Task.FromResult("");
        }
    }
}