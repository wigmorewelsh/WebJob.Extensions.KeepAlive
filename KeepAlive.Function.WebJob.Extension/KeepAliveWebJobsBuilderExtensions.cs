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
                IConfigurationSection section = config.GetSection(path);

                options.Instances = section.GetValue<int>("Instances", 10);
            });

        return builder;
    }
}