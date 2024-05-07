using Microsoft.Azure.WebJobs;

namespace Webjob.Extensions.KeepAlive;

public static class KeepAliveWebJobsBuilderExtensions
{
    public static IWebJobsBuilder AddKeepAlive(this IWebJobsBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.AddExtension<KeepAliveConfigProvider>();

        return builder;
    }
}