using Microsoft.Azure.WebJobs.Host.Triggers;

namespace Webjob.Extensions.KeepAlive.Trigger;

public class KeepAliveTriggerAttributeBindingProvider : ITriggerBindingProvider
{
    public async Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
    {
        return new KeepAliveTriggerBinding();
    }
}