using Microsoft.Azure.WebJobs.Host.Scale;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveScaleMonitor : IScaleMonitor
{
    public Task<ScaleMetrics> GetMetricsAsync()
    {
        return Task.FromResult(new ScaleMetrics() { Timestamp = DateTime.UtcNow });
    }

    public ScaleStatus GetScaleStatus(ScaleStatusContext context)
    {
        if(context.WorkerCount < 10)
        {
            return new ScaleStatus()
            {
                Vote = ScaleVote.ScaleOut
            };
        }
        
        return new ScaleStatus()
        {
            Vote = ScaleVote.None
        };
    }

    public ScaleMonitorDescriptor Descriptor { get; }
}