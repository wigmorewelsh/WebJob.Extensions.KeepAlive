using Microsoft.Azure.WebJobs.Host.Scale;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveTargetScaler : ITargetScaler
{
    public Task<TargetScalerResult> GetScaleResultAsync(TargetScalerContext context)
    {
        return Task.FromResult(new TargetScalerResult()
        {
            TargetWorkerCount = 10
        });
    }

    public TargetScalerDescriptor TargetScalerDescriptor { get; }
}