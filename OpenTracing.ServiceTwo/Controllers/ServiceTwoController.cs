using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTracing.Propagation;
using OpenTracing.Tag;
using Petabridge.Tracing.ApplicationInsights;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OpenTracing.ServiceTwo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceTwoController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            TelemetryConfiguration config = new TelemetryConfiguration("9cb2b528-bc45-451b-8ad6-97d686d7cb79");
            config.TelemetryInitializers.Add(new Models.AppInsightsTelemetryInitializer());

            var tracer = new ApplicationInsightsTracer(config);

            //tracer.Client.TrackEvent("Life is good (outside span) take 2");
            //tracer.Client.TrackException(new Exception("Maybe not :-(  (outside span)"));

            var request = HttpContext.Request;

            string operationName = @"{request.Method} {request.Path}";

            Dictionary<string, string> ss = request.Headers.ToDictionary(a => a.Key, a => a.Value.ToString());

            ISpanContext parentSpanCtx = tracer.Extract(BuiltinFormats.HttpHeaders, new TextMapExtractAdapter(ss));

            using (IScope scope = tracer.BuildSpan(operationName)
                .AsChildOf(parentSpanCtx)
                .StartActive(finishSpanOnDispose: true))



            //using (IScope scope = tracer.BuildSpan("spanInServiceTwoCLIENT").WithSpanKind(SpanKind.CLIENT).StartActive(finishSpanOnDispose: true))
            {
                scope.Span.Log("I'm doing my thing");
                //tracer.Client.TrackException(new ArgumentException("argument argument argument"));
            }


            tracer.Client.Flush();

            return "testing 123";
        }

       
    }
}