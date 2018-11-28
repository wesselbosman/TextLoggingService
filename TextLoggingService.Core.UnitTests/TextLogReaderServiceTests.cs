using NSubstitute;
using Shouldly;
using System.Threading.Tasks;
using TextLoggingService.Core.Logging;
using TextLoggingService.Core.StorageProviders;
using Xunit;

namespace TextLoggingService.Core.UnitTests
{
    public class TextLogReaderServiceTests
    {
        private readonly ILogStorageProvider _logStorageProvider;

        public TextLogReaderServiceTests()
        {
            _logStorageProvider = Substitute.For<ILogStorageProvider>();
        }

        [Fact]
        public async Task Read_WhenStorageProviderReturnsLogfile_ShouldReturnLogfile()
        {
            // Arrange
            var expectedLog = "Doth God exact day labour, light denied?";
            _logStorageProvider.Read().Returns(expectedLog);

            var logReaderService = new TextLogReaderService(_logStorageProvider);

            // Act
            var readResult = await logReaderService.Read();

            // Assert
            readResult.ShouldBe(expectedLog);
        }
    }
}
