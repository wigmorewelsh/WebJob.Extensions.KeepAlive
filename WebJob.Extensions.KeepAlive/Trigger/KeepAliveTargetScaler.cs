using Microsoft.Azure.WebJobs.Host.Scale;
using Microsoft.Extensions.Options;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveTargetScaler : ITargetScaler
{
    private readonly IOptions<KeepAliveOptions> _options;

    public KeepAliveTargetScaler(IOptions<KeepAliveOptions> options)
    {
        _options = options;
    }

    public Task<TargetScalerResult> GetScaleResultAsync(TargetScalerContext context)
    {
        return Task.FromResult(new TargetScalerResult()
        {
            TargetWorkerCount = _options.Value.Instances
        });
    }

    public TargetScalerDescriptor TargetScalerDescriptor { get; }
}