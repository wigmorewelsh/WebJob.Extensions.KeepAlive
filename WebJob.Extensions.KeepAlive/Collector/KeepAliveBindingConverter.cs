using Microsoft.Azure.WebJobs;

public class KeepAliveBindingConverter<T> : IConverter<KeepAliveAttribute, IAsyncCollector<T>>
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