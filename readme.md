# Comparison of options for flowing traces, requests etc to Azure Application Insights
Each test bench has two services - ServiceOne and ServiceTwo. Each of them are an API with a REST endpoint. ServiceOne calls ServiceTwo which must show up in the Application Insigths Application Map like


![Application Map](/docs/images/ApplicationMap.PNG)

And be able to show distributed traces like a span

![End-to-end transaction](/docs/images/End-to-end-transaction.PNG)

The path compared are:
1. Direct to Applicaiton Insights via native SDK (baseline)
2. Open Tracing via PetaBrdige driver to Application Insights
3. Serilog with Application insights SDK for captureing requests, exceptions and performance counters to Application Insights