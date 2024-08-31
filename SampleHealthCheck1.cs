using Microsoft.Extensions.Diagnostics.HealthChecks;

public class SampleHealthCheck1 : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = false;

        if (isHealthy)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("A healthy result."));
        }

        return Task.FromResult(
            new HealthCheckResult(
                context.Registration.FailureStatus, "An unhealthy result.",new Exception("This is exception"),new Dictionary<string, object> { { "SomeData", "value1" }, { "OtherData", "value2" } }));
    }
}