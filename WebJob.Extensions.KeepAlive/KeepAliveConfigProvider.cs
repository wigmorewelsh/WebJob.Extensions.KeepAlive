using Azure.Functions.Worker.Extensions.KeepAlive;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Webjob.Extensions.KeepAlive.Collector;
using Webjob.Extensions.KeepAlive.Trigger;

namespace Webjob.Extensions.KeepAlive;

[Extension("KeepAlive")]
public class KeepAliveConfigProvider : IExtensionConfigProvider
{
    public void Initialize(ExtensionConfigContext context)
    {
        var rule = context.AddBindingRule<KeepAliveAttribute>();
        // rule.AddValidator(ValidateConnection);
        rule.BindToCollector<KeepAliveBindingOpenType>(typeof(KeepAliveBindingConverter<>), this);
        
        var triggerAttributeBindingRule = context.AddBindingRule<KeepAliveTriggerAttribute>();
        triggerAttributeBindingRule.BindToTrigger(new KeepAliveTriggerAttributeBindingProvider());
    }
    
    internal KeepAliveBindingContext CreateContext(KeepAliveAttribute attribute)
    {
        return new KeepAliveBindingContext
        {
            Instances = attribute.Instances
        };
    }
    
    private class KeepAliveBindingOpenType : OpenType.Poco
    {
        public override bool IsMatch(Type type, OpenTypeMatchContext context)
        {
            if (type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return false;
            }

            if (type.FullName == "System.Object")
            {
                return true;
            }

            return base.IsMatch(type, context);
        }
    }
}