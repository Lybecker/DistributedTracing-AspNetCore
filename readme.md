# Comparison of options for flowing traces, requests etc to Azure Application Insights
Each test bench has two services - ServiceOne and ServiceTwo. Each of them are an API with a REST endpoint. ServiceOne calls ServiceTwo which must show up in the Application Insigths Application Map like


![Application Map](/docs/images/ApplicationMap.PNG)

And be able to show distributed traces like a span

![End-to-end transaction](/docs/images/End-to-end-transaction.PNG)

The path compared are:
1. Direct to Applicaiton Insights via native SDK (baseline)
2. Open Tracing via PetaBrdige driver to Application Insights
3. Serilog with Application insights SDK for captureing requests, exceptions and performance counters to Application Insights

# Notes
To propragate the log/trace context over process boundraies for protocols not default supported, take a look at how it is implemented in [DiagnosticsHandler](https://github.com/dotnet/corefx/blob/72f5cff116fdfa71f44090281e091b0dcbc31f8f/src/System.Net.Http/src/System/Net/Http/DiagnosticsHandler.cs#L189) for HttpClient does it. Also see the [HttpClient Diagnostic Instrumentation Users Guide](https://github.com/dotnet/corefx/blob/c082d21361608e3cc2ea3cddd90c2a0306828002/src/System.Net.Http/src/HttpDiagnosticsGuide.md).

It makes use of the [System.Diagnostics.Activity](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.activity?view=netcore-3.0) class to store the distributed tracing context.