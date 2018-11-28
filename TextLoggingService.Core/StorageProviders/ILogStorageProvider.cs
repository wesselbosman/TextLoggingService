using System;
using System.Threading.Tasks;

namespace TextLoggingService.Core.StorageProviders
{
    public interface ILogStorageProvider
    {
        Task Write(long id, DateTime date, string message);
        Task<string> Read();
    }
}