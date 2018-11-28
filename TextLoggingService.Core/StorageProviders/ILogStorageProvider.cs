using System.Threading.Tasks;

namespace TextLoggingService.Core.StorageProviders
{
    public interface ILogStorageProvider
    {
        Task Write();
        Task<string> Read();
    }
}