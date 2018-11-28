using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [Route("read"), HttpGet]
        public async Task<IActionResult> Read()
        {
            var entireLog = await _logReaderService.Read();
            return Ok(entireLog);
        }
    }
}