using System;
using System.Threading.Tasks;
using NSubstitute;
using TextLoggingService.Core.Logging;
using TextLoggingService.Core.StorageProviders;
using Xunit;

namespace TextLoggingService.Core.UnitTests
{
    public class TextLogWriterServiceTests
    {
        private readonly ILogStorageProvider _logStorageProvider;

        public TextLogWriterServiceTests()
        {
            _logStorageProvider = Substitute.For<ILogStorageProvider>();
        }

        [Fact]
        public async Task AppendLogMessage_GivenLogModel_ShouldWriteToStorageProvider()
        {
            // Arrange
            var expectedMessage = "Doth God exact day labour, light denied?";
            var textLogWriterService = new TextLogWriterService(_logStorageProvider);
            var logModel = new LogModel(0L, DateTime.MinValue, expectedMessage);

            // Act
            await textLogWriterService.AppendLogMessage(logModel);

            // Assert
            await _logStorageProvider.Received().Write(Arg.Any<long>(), Arg.Any<DateTime>(), Arg.Any<string>());
        }
    }
}
