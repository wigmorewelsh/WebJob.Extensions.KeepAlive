using Microsoft.Azure.WebJobs.Host.Triggers;

public class KeepAliveTriggerAttributeBindingProvider : ITriggerBindingProvider
{
    public async Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
    {
        return new KeepAliveTriggerBinding();
    }
}