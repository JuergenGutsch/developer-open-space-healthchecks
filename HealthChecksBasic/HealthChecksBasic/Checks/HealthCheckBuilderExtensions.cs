using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace HealthChecksBasic.Checks
{
    public static class HealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddApplicationChecks(
                this IHealthChecksBuilder builder)
        {
            builder.AddCheck("Example", () => {
                return HealthCheckResult.Healthy("Example is OK!");
            }, tags: new[] { "example" })
            .AddCheck("Foo", () =>
                HealthCheckResult.Healthy("Foo is OK!"), tags: new[] { "demo" })
            .AddCheck("Bar", () =>
                HealthCheckResult.Unhealthy("Bar is not OK!"), tags: new[] { "demo", "error" })
            .AddCheck("Baz", () =>
                HealthCheckResult.Degraded("Baz is degraded!"), tags: new[] { "demo", "degraded" })
            .AddCheck<ExampleHealthCheck>("Class based",
                HealthStatus.Unhealthy, tags: new[] { "example" })
            .AddCheck("ping", () =>
            {
                try
                {
                    using (var ping = new Ping())
                    {
                        var reply = ping.Send("asp.net-hacker.rocks");
                        if (reply.Status != IPStatus.Success)
                        {
                            return HealthCheckResult.Unhealthy("Ping is unhealthy");
                        }

                        if (reply.RoundtripTime > 100)
                        {
                            return HealthCheckResult.Degraded("Ping is degraded");
                        }

                        return HealthCheckResult.Healthy("Ping is healthy");
                    }
                }
                catch
                {
                    return HealthCheckResult.Unhealthy("Ping is unhealthy");
                }
            });

            return builder;
        }

    }
}
