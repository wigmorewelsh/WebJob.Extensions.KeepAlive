using Microsoft.Azure.WebJobs;

public static class KeepAliveWebJobsBuilderExtensions
{
    public static IWebJobsBuilder AddCosmosDBV3(this IWebJobsBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }
            
        builder.AddExtension<KeepAliveConfigProvider>();

        return builder;
    }
}