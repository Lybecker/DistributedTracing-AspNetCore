using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;

namespace Serilog.ServiceTwo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceTwoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            TelemetryConfiguration config = new TelemetryConfiguration("9cb2b528-bc45-451b-8ad6-97d686d7cb79");
            config.TelemetryInitializers.Add(new Models.AppInsightsTelemetryInitializer());

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo
                .ApplicationInsights(config, TelemetryConverter.Traces)
                .CreateLogger();

            Log.Information("In service two");

            return "Testing from Serilog";
        }
    }
}
