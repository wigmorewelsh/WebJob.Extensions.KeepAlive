using Microsoft.Azure.WebJobs.Host.Triggers;
using Microsoft.Extensions.Options;

namespace Webjob.Extensions.KeepAlive.Trigger;

public class KeepAliveTriggerAttributeBindingProvider : ITriggerBindingProvider
{
    private readonly IOptions<KeepAliveOptions> _options;

    public KeepAliveTriggerAttributeBindingProvider(IOptions<KeepAliveOptions> options)
    {
        _options = options;
    }

    public async Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
    {
        return new KeepAliveTriggerBinding(context.Parameter, _options);
    }
}