using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register health checks to DI.
builder.Services.AddHealthChecks()
    .AddCheck<SampleHealthCheck>("basic_check")
    .AddCheck<SampleHealthCheck1>("advance_check", HealthStatus.Degraded);

//Background service that sets the app started status after 15 seconds.
builder.Services.AddHostedService<StartupBackgroundService>();
builder.Services.AddSingleton<StartupHealthCheck>();

//Readiness probe, with tags added for filtering.
builder.Services.AddHealthChecks()
    .AddCheck<StartupHealthCheck>(
        "startup",
        tags: ["ready"]);

var app = builder.Build();

//Health endpoint for services that depend on this API.
//Return health data in json format with all details.
app.MapHealthChecks("health", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

//Health check probs for k8s
//Liveness probe, runs all checks and returns healthy if all checks are healthy
app.MapHealthChecks("health/live", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
//Readiness probe, for demo purpose it wait for 15 seconds after app starts and then returns healthy.
//It also filters checks based on tags.
app.MapHealthChecks("health/ready", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    Predicate = healthCheck => healthCheck.Tags.Contains("ready")
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();