using Microsoft.Azure.WebJobs.Host.Scale;
using Microsoft.Extensions.Options;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveScaleMonitor : IScaleMonitor<KeepAliveMetric>
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
        return Task.FromResult((ScaleMetrics)new KeepAliveMetric() { Timestamp = DateTime.UtcNow });
    }

    public ScaleStatus GetScaleStatus(ScaleStatusContext<KeepAliveMetric> context)
    {
        return GetScaleStatus(context.WorkerCount);
    }

    Task<KeepAliveMetric> IScaleMonitor<KeepAliveMetric>.GetMetricsAsync()
    {
        return Task.FromResult(new KeepAliveMetric() { Timestamp = DateTime.UtcNow });
    }

    public ScaleStatus GetScaleStatus(ScaleStatusContext context)
    {
        return GetScaleStatus(context.WorkerCount);
    }

    private ScaleStatus GetScaleStatus(int contextWorkerCount)
    {
        if (contextWorkerCount < _options.Value.Instances)
        {
            return new ScaleStatus()
            {
                Vote = ScaleVote.ScaleOut
            };
        }
        
        if (contextWorkerCount > _options.Value.Instances)
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

internal class KeepAliveMetric : ScaleMetrics
{
}