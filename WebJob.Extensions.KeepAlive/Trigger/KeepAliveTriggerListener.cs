using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Scale;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveTriggerListener : IListener, IScaleMonitorProvider, ITargetScalerProvider
{
    public IScaleMonitor GetMonitor()
    {
        return new KeepAliveScaleMonitor();
    }

    public ITargetScaler GetTargetScaler()
    {
        return new KeepAliveTargetScaler();
    }

    public void Dispose()
    {
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Cancel()
    {
    }
}