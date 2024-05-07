using Microsoft.Azure.WebJobs.Description;

namespace Webjob.Extensions.KeepAlive.Collector;

[AttributeUsage(AttributeTargets.Parameter)]
[Binding]
public sealed class KeepAliveAttribute : Attribute
{
    /// <summary>
    /// Constructs a new instance.
    /// </summary>
    public KeepAliveAttribute()
    {
    }
    
    /// <summary>
    /// Number of instances to keep alive
    /// </summary>
    [AppSetting]
    public int Instances { get; set; }
}