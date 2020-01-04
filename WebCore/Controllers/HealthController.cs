using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //心跳检查
    public class HealthController : ControllerBase
    {
        private ILoggerFactory _LoggerFactory = null;
        public HealthController(ILoggerFactory loggerFactory)
        {
            _LoggerFactory = loggerFactory;
        }
        [HttpGet]
        public IActionResult Check()
        {
            this._LoggerFactory.CreateLogger(typeof(HealthController)).LogWarning("健康检查创建");
            return Ok();
        }
    }
}