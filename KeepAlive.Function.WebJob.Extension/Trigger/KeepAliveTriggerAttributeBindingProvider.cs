using System.Reflection;
using Microsoft.Azure.WebJobs.Host.Triggers;
using Microsoft.Extensions.Options;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveTriggerAttributeBindingProvider : ITriggerBindingProvider
{
    private readonly IOptions<KeepAliveOptions> _options;

    public KeepAliveTriggerAttributeBindingProvider(IOptions<KeepAliveOptions> options)
    {
        _options = options;
    }

    public async Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
    {
        KeepAliveTriggerAttribute triggerAttribute = context.Parameter.GetCustomAttribute<KeepAliveTriggerAttribute>(inherit: false);
        if (triggerAttribute is null)
        {
            return null;
        }
        
        return new KeepAliveTriggerBinding(context.Parameter, _options);
    }
}