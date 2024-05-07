using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Scale;
using Microsoft.Extensions.Options;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveTriggerListener : IListener, IScaleMonitorProvider, ITargetScalerProvider
{
    private readonly string _functionId;
    private readonly IOptions<KeepAliveOptions> _options;

    public KeepAliveTriggerListener(string functionId, IOptions<KeepAliveOptions> options)
    {
        _functionId = functionId;
        _options = options;
    }

    public IScaleMonitor GetMonitor()
    {
        return new KeepAliveScaleMonitor(_functionId, _options);
    }

    public ITargetScaler GetTargetScaler()
    {
        return new KeepAliveTargetScaler(_functionId, _options);
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