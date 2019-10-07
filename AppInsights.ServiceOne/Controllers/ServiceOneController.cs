using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppInsights.ServiceOne.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceOneController : ControllerBase
    {
        private readonly ILogger<ServiceOneController> _logger;

        public ServiceOneController(ILogger<ServiceOneController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            _logger.LogInformation("AppInsights.Starting");

            var client = new HttpClient();

            var response = await client.GetStringAsync("http://localhost:60596/servicetwo");

            //using (_logger.BeginScope("AppInsight.ScopeStuff"))
            //{
            //    _logger.LogInformation("Hello, AppInsights Scope!");
            //}
            
            _logger.LogInformation("AppInsights.Done");

            return response;
        }
    }
}