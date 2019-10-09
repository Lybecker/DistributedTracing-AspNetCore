using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Serilog.ServiceTwo.Controllers
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
            _logger.LogInformation("In service two");
            _logger.LogInformation("ActivityID: " + System.Diagnostics.Activity.Current.Id);
            _logger.LogInformation("SpanID    : " + System.Diagnostics.Activity.Current.SpanId);
            _logger.LogInformation("Baggage   : " + string.Join(",", System.Diagnostics.Activity.Current.Baggage.Select(p => p.ToString()).ToArray()));
            _logger.LogInformation("ParentID  : " + System.Diagnostics.Activity.Current.ParentId);
            _logger.LogInformation("PareSpanID: " + System.Diagnostics.Activity.Current.ParentSpanId);

            return "Testing from Serilog";
        }
    }
}
