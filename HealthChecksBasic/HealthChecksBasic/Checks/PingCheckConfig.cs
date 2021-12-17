using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecksBasic.Checks
{
    public class PingCheckConfig
    {
        public HealthStatus DegradedStatus { get; set; }
        public HealthStatus FailureStatus { get; set; }
    }
}