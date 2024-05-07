using Microsoft.Azure.WebJobs;

public class KeepAliveCollector<T> : IAsyncCollector<T>
{
    public KeepAliveCollector(KeepAliveBindingContext context)
    {
    }

    public Task AddAsync(T item, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.CompletedTask;
    }

    public Task FlushAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.CompletedTask;
    }
}