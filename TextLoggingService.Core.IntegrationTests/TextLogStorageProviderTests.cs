using Shouldly;
using System;
using TextLoggingService.Core.StorageProviders;
using Xunit;

namespace TextLoggingService.Core.IntegrationTests
{
    public class TextLogStorageProviderTests
    {
        [Fact]
        public void StorageProviderCanReadAndWrite()
        {
            var textLogStorageProvider = new TextLogStorageProvider();
            textLogStorageProvider.Write(0, DateTime.MinValue, "testLog").Wait();

            var read = textLogStorageProvider.Read().Result;
            read.ShouldContain("testLog");
        }
    }
}
