using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

public class KeepAliveWebJobsStartup : IWebJobsStartup
{
    public void Configure(IWebJobsBuilder builder)
    {
        builder.AddCosmosDBV3();
    }
}