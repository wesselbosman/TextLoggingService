using System;
using System.IO;
using System.Threading.Tasks;

namespace TextLoggingService.Core.StorageProviders
{
    public class TextLogStorageProvider : ILogStorageProvider
    {
        // This works if this class is registered in the IoC container as a singleton
        private static readonly string _tempLogPath = Path.Combine(Path.GetTempPath(), $"textapp-{DateTime.Now:yy-MM-dd}.log");

        public async Task Write(long id, DateTime date, string message)
        {
            using (var fileStream = new FileStream(_tempLogPath, FileMode.Append))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                await streamWriter.WriteAsync($"{date} | {id} | => {message}{Environment.NewLine}");
            }
        }

        public async Task<string> Read()
        {
            var fileStream = File.Exists(_tempLogPath) 
                ? File.OpenRead(_tempLogPath) 
                : (Stream) new MemoryStream(new byte[0]);

            using (fileStream)
            {
                var streamReader = new StreamReader(fileStream);
                using (streamReader)
                {
                   return await streamReader.ReadToEndAsync();
                }
            }
        }
    }
}