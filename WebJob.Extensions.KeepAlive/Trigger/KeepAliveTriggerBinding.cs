using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;

namespace Webjob.Extensions.KeepAlive.Trigger;

public class KeepAliveTriggerBinding : ITriggerBinding
{
    public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
    {
        return Task.FromResult<ITriggerData>(new TriggerData(null, new Dictionary<string, object>()));
    }

    public async Task<IListener> CreateListenerAsync(ListenerFactoryContext context)
    {
        return new KeepAliveTriggerListener();
    }

    public ParameterDescriptor ToParameterDescriptor()
    {
        return new ParameterDescriptor()
        {
            Name = "KeepAliveTrigger",
            Type = "KeepAliveTrigger"
        };
    }

    public Type TriggerValueType => typeof(KeepAliveValue);
    public IReadOnlyDictionary<string, Type> BindingDataContract { get; } = new Dictionary<string, Type>();
}