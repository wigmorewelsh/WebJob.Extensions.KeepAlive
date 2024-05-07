using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;

namespace Webjob.Extensions.KeepAlive;

public static class KeepAliveWebJobsBuilderExtensions
{
    public static IWebJobsBuilder AddKeepAlive(this IWebJobsBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.AddExtension<KeepAliveConfigProvider>()
            .ConfigureOptions<KeepAliveOptions>((config, path, options) =>
            {
                config.GetSection(path).Bind(options);
            });

        return builder;
    }
}