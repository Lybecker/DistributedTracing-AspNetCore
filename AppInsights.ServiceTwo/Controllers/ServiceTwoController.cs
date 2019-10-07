using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppInsights.ServiceTwo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceTwoController : ControllerBase
    {
        private readonly ILogger<ServiceTwoController> _logger;

        public ServiceTwoController(ILogger<ServiceTwoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            _logger.LogInformation("AppInsights.Doing my stuff");

            return "testing 123 from AppInsights";
        }
    }
}