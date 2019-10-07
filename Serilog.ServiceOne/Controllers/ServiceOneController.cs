using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;

namespace Serilog.ServiceOne.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceOneController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo
                .ApplicationInsights(TelemetryConfiguration.Active, TelemetryConverter.Traces)
                .CreateLogger();

            Log.Information("Serilog.Starting");

            var client = new HttpClient();

            var response = await client.GetStringAsync("http://localhost:50850/servicetwo");
            //var response = await client.GetStringAsync("http://localhost:5000/servicetwo");


            Log.Information("Serilog.Done");

            return response;
        }
    }
}