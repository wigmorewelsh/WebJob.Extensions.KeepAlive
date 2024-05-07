using System.Reflection;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveValueProvider : IValueProvider
{
    private readonly object _value;
    private readonly ParameterInfo _parameter;

    public KeepAliveValueProvider(object value, ParameterInfo parameter)
    {
        _value = value;
        _parameter = parameter;
    }

    public async Task<object> GetValueAsync()
    {
        return _value;
    }

    public string ToInvokeString() => String.Empty;

    public Type Type => _parameter.ParameterType;
}