using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Scale;
using Microsoft.Extensions.Options;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveTriggerListener : IListener, IScaleMonitorProvider, ITargetScalerProvider
{
    private readonly string _functionId;
    private readonly ITriggeredFunctionExecutor _contextExecutor;
    private readonly IOptions<KeepAliveOptions> _options;
    private Task? _worker;
    private CancellationTokenSource? _linkedToken;

    public KeepAliveTriggerListener(string functionId, 
        ITriggeredFunctionExecutor contextExecutor,
        IOptions<KeepAliveOptions> options)
    {
        _functionId = functionId;
        _contextExecutor = contextExecutor;
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
        _linkedToken = new CancellationTokenSource();
        _worker = Task.Run(async () => { await Worker(); });
        return Task.CompletedTask;
    }

    private async Task Worker()
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(15));
        while (await timer.WaitForNextTickAsync(_linkedToken.Token))
        {
            _contextExecutor.TryExecuteAsync(new TriggeredFunctionData { TriggerValue = new KeepAliveValue() }, _linkedToken.Token);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_linkedToken == null || _worker == null) return;
        _linkedToken?.Cancel();
        if(_worker is {} worker)
            await worker;
    }

    public void Cancel()
    {
        _linkedToken?.Cancel();
    }
}