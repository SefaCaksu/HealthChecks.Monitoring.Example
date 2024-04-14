using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Health check, add services to the container. 
builder.Services.AddHealthChecks()
     .AddSqlServer(
                  connectionString: "Server=localhost, 1433;Database=OrderAPIDB-OUTBOX;User ID=SA;Password=Sefa-1234;TrustServerCertificate=True",
                  healthQuery: "Select 1",
                  name: "ApiX MsSQL",
                  failureStatus: HealthStatus.Degraded | HealthStatus.Unhealthy,
                  tags: new string[] { "MsSql", "MainDatabase" }
                  )
            .AddMongoDb(
                  mongodbConnectionString: "mongodb://localhost:27017",
                  name: "ApiX MongoDB",
                  failureStatus: HealthStatus.Degraded | HealthStatus.Unhealthy,
                  tags: new string[] { "MongoDB" }
                  );

var app = builder.Build();

//Health check, Configure http request pipeline.
app.UseHealthChecks("/health", new HealthCheckOptions()
{
    //İstenilen bilgileri yazma
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

