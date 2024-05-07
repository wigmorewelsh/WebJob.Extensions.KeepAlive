using System.Reflection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Newtonsoft.Json;

namespace Webjob.Extensions.KeepAlive.Trigger;

internal class KeepAliveValueProvider : IValueProvider
{
    private static readonly Type STRING_TYPE = typeof(string);
    
    private readonly object _value;
    private readonly ParameterInfo _parameter;
    private readonly bool _parameterTypeIsString;

    public KeepAliveValueProvider(object value, ParameterInfo parameter)
    {
        _value = value;
        _parameter = parameter;
        _parameterTypeIsString = parameter.ParameterType == STRING_TYPE;
    }

    public async Task<object> GetValueAsync()
    {
        if (_parameterTypeIsString)
        {
            return Task.FromResult((object)JsonConvert.SerializeObject(_value));
        }
        
        return _value;
    }

    public string ToInvokeString() => String.Empty;

    public Type Type => _parameter.ParameterType;
}