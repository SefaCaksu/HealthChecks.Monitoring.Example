
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecksUI().AddSqlServerStorage("Server=localhost, 1433;Database=HEALTHCHECK;User ID=SA;Password=Sefa-1234;TrustServerCertificate=True");

var app = builder.Build();

app.UseHealthChecksUI(options => {
    options.UIPath = "/healthui";
    options.AddCustomStylesheet("healthcheck.css");
});

app.Run();

