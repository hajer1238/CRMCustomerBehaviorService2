CRMCustomerBehavior Project - Week 2: API with Observability Setup
This repository contains the work completed during Week 2, focusing on building a customer event API and integrating observability tools.

🎯 Week 2 Accomplishments
Developed an ASP.NET Core API to record customer behavior events.

Integrated Serilog for structured logging within the application.

Implemented distributed tracing using OpenTelemetry.

Set up Jaeger as the backend for visualizing traces via Docker.

Tested the API using Postman.

🚀 Key Features Implemented
Customer Events API: A /CustomerEvents endpoint handling POST requests.

Logging: Serilog configured to output application logs to the console.

Distributed Tracing: OpenTelemetry SDK integrated for automatic ASP.NET Core request instrumentation, sending traces to Jaeger.

Docker Containerization: Jaeger deployed as a jaegertracing/all-in-one Docker container.

⚠️ Current Tracing Status
Jaeger is running and accessible, showing its internal traces. However, despite correct configuration and extensive troubleshooting (including firewall adjustments and using Docker gateway IP), traces from CRMCustomerBehaviorService are not yet appearing in Jaeger. This suggests a persistent, low-level network communication issue between the .NET application (on Windows) and the Jaeger container (in WSL2/Docker) that requires further direct diagnosis.








