using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using OpenTracing.Propagation;
using Petabridge.Tracing.ApplicationInsights;

namespace OpenTracing.ServiceOne.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceOneController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            TelemetryConfiguration config = new TelemetryConfiguration("9cb2b528-bc45-451b-8ad6-97d686d7cb79");
            config.TelemetryInitializers.Add(new Models.AppInsightsTelemetryInitializer());

            var tracer = new ApplicationInsightsTracer(config);
            var client = new HttpClient();

            //using (IScope scope = tracer.BuildSpan("StartingWork").WithSpanKind(SpanKind.CLIENT).StartActive(finishSpanOnDispose: true))
            using (IScope scope = tracer.BuildSpan("testServerKind").StartActive(finishSpanOnDispose: true))
            {
                scope.Span.Log("Starting...");

                var dictionary = new Dictionary<string, string>();
                tracer.Inject(scope.Span.Context, BuiltinFormats.HttpHeaders, new TextMapInjectAdapter(dictionary));
                foreach (var entry in dictionary)
                    client.DefaultRequestHeaders.Add(entry.Key, entry.Value);

                var response = await client.GetStringAsync("http://localhost:54790/servicetwo");

                //scope.Span.SetTag("myTag", true);
                scope.Span.Log("Done :-)");

                tracer.Client.Flush();

                return response;
            }
        }
    }
}

//using (var requestMessage =
//            new HttpRequestMessage(HttpMethod.Get, "https://your.site.com"))
//{
//    requestMessage.Headers.Authorization =
//        new AuthenticationHeaderValue("Bearer", your_token);
//httpClient.SendAsync(requestMessage);
//}