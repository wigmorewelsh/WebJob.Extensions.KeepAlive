using Microsoft.Azure.WebJobs.Host.Scale;
using Microsoft.Extensions.Options;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveScaleMonitor : IScaleMonitor
{
    private readonly string _functionId;
    private readonly IOptions<KeepAliveOptions> _options;
    
    public ScaleMonitorDescriptor Descriptor { get; }

    public KeepAliveScaleMonitor(string functionId, IOptions<KeepAliveOptions> options)
    {
        _functionId = functionId;
        _options = options;
        
        Descriptor = new ScaleMonitorDescriptor($"{_functionId}-{KeepAliveConstants.KeepaliveTrigger}", functionId);
    }

    public Task<ScaleMetrics> GetMetricsAsync()
    {
        return Task.FromResult(new ScaleMetrics() { Timestamp = DateTime.UtcNow });
    }

    public ScaleStatus GetScaleStatus(ScaleStatusContext context)
    {
        if (context.WorkerCount < _options.Value.Instances)
        {
            return new ScaleStatus()
            {
                Vote = ScaleVote.ScaleOut
            };
        }
        
        if (context.WorkerCount > _options.Value.Instances)
        {
            return new ScaleStatus()
            {
                Vote = ScaleVote.ScaleIn
            };
        }
        
        return new ScaleStatus()
        {
            Vote = ScaleVote.None
        };
    }

}