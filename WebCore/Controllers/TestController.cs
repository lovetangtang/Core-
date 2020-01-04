using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Consul;
namespace WebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private ILoggerFactory _LoggerFactory = null;
        public TestController(ILoggerFactory loggerFactory)
        {
            _LoggerFactory = loggerFactory;
        }
        /// <summary>
        /// 调用微服务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() {
            string msg = "";
            using (ConsulClient client=new ConsulClient(c=>c.Address=new Uri("http://localhost:8500/")))
            {
                var services = client.Agent.Services().Result.Response;
                var targetServices = services.Where(s => s.Value.Service.Equals("GroupTang")).Select(s => s.Value);

                //平均调度策略：随机+取余
                var target = services.ElementAt(new Random().Next(1, 10000) % targetServices.Count());
                msg = $"当前api:{target.Value.Address}{target.Value.Port}{target.Value.ID}Tang分组";

            }
            return new JsonResult(new { id = 111, Name = "糖糖", msg = msg });
        }
    }
}