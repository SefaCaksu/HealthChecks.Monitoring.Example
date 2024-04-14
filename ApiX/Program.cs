using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Uygulamanın sağlığı
builder.Services.AddHealthChecks()
            //Bağımlılıkların Sağlığı
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

app.UseHealthChecks("/health", new HealthCheckOptions()
{
    //İstenilen bilgileri yazma
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}) ;

app.Run();

