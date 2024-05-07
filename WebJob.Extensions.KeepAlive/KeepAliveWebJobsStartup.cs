using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Webjob.Extensions.KeepAlive;

[assembly: WebJobsStartup(typeof(KeepAliveWebJobsStartup))]

namespace Webjob.Extensions.KeepAlive;

public class KeepAliveWebJobsStartup : IWebJobsStartup
{
    public void Configure(IWebJobsBuilder builder)
    {
        builder.AddKeepAlive();
    }
}