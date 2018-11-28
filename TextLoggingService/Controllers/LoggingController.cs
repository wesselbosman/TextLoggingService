using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TextLoggingService.Core.Logging;

namespace TextLoggingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        private readonly ILogWriterService _logWriterService;
        private readonly ILogReaderService _logReaderService;

        public LoggingController(ILogWriterService logWriterService, ILogReaderService logReaderService)
        {
            _logWriterService = logWriterService;
            _logReaderService = logReaderService;
        }

        /// <summary>
        /// Writes a log message to the storage provider
        /// </summary>
        /// <param name="logModel"></param>
        /// <returns>Returns accepted if the message was received and bad request status if validation fials</returns>
        [Route("write"), HttpPost]
        public async Task<IActionResult> Write([FromBody] LogModel logModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _logWriterService.AppendLogMessage(logModel);
            return Accepted();
        }

        /// <summary>
        /// Returns the entire log as text
        /// </summary>
        /// <returns>OK status with the entire log text as content</returns>
        [Route("read"), HttpGet]
        public async Task<IActionResult> Read()
        {
            var entireLog = await _logReaderService.Read();
            return Ok(entireLog);
        }
    }
}