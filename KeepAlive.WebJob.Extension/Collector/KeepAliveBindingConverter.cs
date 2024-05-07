using Microsoft.Azure.WebJobs;

namespace Webjob.Extensions.KeepAlive.Collector;

internal class KeepAliveBindingConverter<T> : IConverter<KeepAliveAttribute, IAsyncCollector<T>>
{
    private readonly KeepAliveConfigProvider configProvider;

    public KeepAliveBindingConverter(KeepAliveConfigProvider configProvider)
    {
        this.configProvider = configProvider;
    }

    public IAsyncCollector<T> Convert(KeepAliveAttribute attribute)
    {
        var context = this.configProvider.CreateContext(attribute);
        return new KeepAliveCollector<T>(context);
    }
}