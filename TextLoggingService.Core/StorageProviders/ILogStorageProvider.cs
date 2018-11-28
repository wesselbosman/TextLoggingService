using System.Threading.Tasks;

namespace TextLoggingService.Core.StorageProviders
{
    public interface ILogStorageProvider
    {
        Task Write();
        Task<string> Read();
    }

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