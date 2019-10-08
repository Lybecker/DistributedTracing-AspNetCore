using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Serilog.ServiceOne.Controllers
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
            _logger.LogInformation("Serilog.Starting");

            var client = new HttpClient();

            var response = await client.GetStringAsync("http://localhost:5001/servicetwo");

            _logger.LogInformation("Serilog.Done");

            return response;
        }
    }
}