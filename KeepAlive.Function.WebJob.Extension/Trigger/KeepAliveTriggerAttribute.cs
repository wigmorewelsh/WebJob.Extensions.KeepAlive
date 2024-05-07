using Microsoft.Azure.WebJobs.Description;

namespace Webjob.Extensions.KeepAlive.Trigger;

[Binding]
[AttributeUsage(AttributeTargets.Parameter)]
public sealed class KeepAliveTriggerAttribute : Attribute 
{
}