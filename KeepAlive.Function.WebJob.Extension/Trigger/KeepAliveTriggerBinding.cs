using System.Reflection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;
using Microsoft.Extensions.Options;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveTriggerBinding : ITriggerBinding
{
    private readonly ParameterInfo _parameter;
    private readonly IOptions<KeepAliveOptions> _options;

    public KeepAliveTriggerBinding(ParameterInfo parameter, IOptions<KeepAliveOptions> options)
    {
        _parameter = parameter;
        _options = options;
    }

    public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
    {
        var keepAliveValueProvider = new KeepAliveValueProvider(value, _parameter);
        return Task.FromResult<ITriggerData>(new TriggerData(keepAliveValueProvider, new Dictionary<string, object>()));
    }

    public async Task<IListener> CreateListenerAsync(ListenerFactoryContext context)
    {
        return new KeepAliveTriggerListener(context.Descriptor.Id, context.Executor, _options);
    }

    public ParameterDescriptor ToParameterDescriptor()
    {
        return new ParameterDescriptor()
        {
            Name = _parameter.Name,
            Type = KeepAliveConstants.KeepaliveTrigger
        };
    }

    public Type TriggerValueType => typeof(KeepAliveValue);
    public IReadOnlyDictionary<string, Type> BindingDataContract { get; } = new Dictionary<string, Type>();
}