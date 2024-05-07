using Microsoft.Azure.WebJobs.Host.Scale;
using Microsoft.Extensions.Options;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveScaleMonitor : IScaleMonitor
{
    private readonly IOptions<KeepAliveOptions> _options;

    public KeepAliveScaleMonitor(IOptions<KeepAliveOptions> options)
    {
        _options = options;
    }

    public Task<ScaleMetrics> GetMetricsAsync()
    {
        return Task.FromResult(new ScaleMetrics() { Timestamp = DateTime.UtcNow });
    }

    public ScaleStatus GetScaleStatus(ScaleStatusContext context)
    {
        if(context.WorkerCount < _options.Value.Instances)
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