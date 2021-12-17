using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace HealthChecksBasic.Checks
{
    public class ExampleHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send("asp.net-hacker.rocks");
                    if (reply.Status != IPStatus.Success)
                    {
                        return Task.FromResult(
                            new HealthCheckResult(context.Registration.FailureStatus,
                                "Ping is unhealthy"));
                    }

                    if (reply.RoundtripTime > 100)
                    {
                        return Task.FromResult(HealthCheckResult.Degraded("Ping is degraded"));
                    }

                    return Task.FromResult(HealthCheckResult.Healthy("Ping is healthy"));
                }
            }
            catch
            {
                return Task.FromResult(
                    new HealthCheckResult(context.Registration.FailureStatus, 
                        "Ping is unhealthy"));
            }



            //if (healthCheckResultHealth)
            //{
            //    return Task.FromResult(
            //        HealthCheckResult.Healthy("A health result."));
            //}

            //return Task.FromResult(
            //    new HealthCheckResult(context.Registration.FailureStatus,
            //    "An unhealthy result."));

        }
    }
}
