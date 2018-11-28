using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace TextLoggingService.IntegrationTests
{
    public class LoggingControllerTests
    {
        private readonly TestServer _testServer;

        public LoggingControllerTests()
        {
            _testServer = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("AutomatedTests"));
        }

        [Fact]
        public async Task Post_Write_GivenLogMessage_ShouldReturnAcceptedResponse()
        {
            const string logMessageJson = "{\r\n\t\"id\": 0,\r\n\t\"date\": \"01/11/1990\",\r\n\t\"message\": \"Doth God exact day labour, light denied?\"\r\n}";
            var httpClient = _testServer.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/api/logging/write")
            {
                Content = new StringContent(logMessageJson, Encoding.UTF8, "application/json")
            });

            httpResponseMessage.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        }

        [Fact]
        public async Task Post_Write_GivenLogMessageOver255characters_ShouldReturnBadRequestResponse()
        {
            const string logMessageJson = "{\r\n\t\"id\": 0,\r\n\t\"date\": \"01/11/1990\"," +
                                          "\r\n\t\"message\": \"Doth God exact day labour, light denied? lorem ipsum sit dolor amet some more latin blah blah fishpaste. " +
                                          "Doth God exact day labour, light denied? lorem ipsum sit dolor amet some more latin blah blah fishpaste. " +
                                          "Doth God exact day labour, light denied? lorem ipsum sit dolor amet some more latin blah blah fishpaste.\"\r\n}";
            var httpClient = _testServer.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/api/logging/write")
            {
                Content = new StringContent(logMessageJson, Encoding.UTF8, "application/json")
            });

            httpResponseMessage.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_Read_ShouldReturnOkResponseWithLog()
        {
            var httpClient = _testServer.CreateClient();
            var httpResponseMessage =
                await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/api/logging/read"));

            httpResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK);
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

            responseString.ShouldBe("");
        }
    }
}
