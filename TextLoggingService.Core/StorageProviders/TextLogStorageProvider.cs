using System;
using System.IO;
using System.Threading.Tasks;

namespace TextLoggingService.Core.StorageProviders
{
    public class TextLogStorageProvider : ILogStorageProvider
    {
        // This works if this class is registered in the IoC container as a singleton
        private string _tempLogPath = string.Empty;

        public async Task Write(long id, DateTime date, string message)
        {
            var tempFileName = GetLogFileName();
            var fileStream = File.OpenWrite(tempFileName);

            using (fileStream)
            {
                var streamWriter = new StreamWriter(fileStream) {NewLine = Environment.NewLine};
                using (streamWriter)
                {
                    await streamWriter.WriteAsync($"{date}|{id}=>{message}");
                }
            }
        }

        public async Task<string> Read()
        {
            var tempFileName = GetLogFileName();
            var fileStream = File.OpenRead(tempFileName);

            using (fileStream)
            {
                var streamReader = new StreamReader(fileStream);
                using (streamReader)
                {
                   return await streamReader.ReadToEndAsync();
                }
            }
        }

        private string GetLogFileName()
        {
            // Using tempfiles to avoid linux\windows filepath issues when deploying with Container engine, ideally would be set with config
            // with a max filesize and naming conventions. I think that would out of the scope for this exercise.
            var tempFileName = string.IsNullOrWhiteSpace(_tempLogPath) ? Path.GetTempFileName() : _tempLogPath;
            _tempLogPath = tempFileName;
            return tempFileName;
        }
    }
}