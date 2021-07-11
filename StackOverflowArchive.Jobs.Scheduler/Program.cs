using MediatR;
using Microsoft.Extensions.Hosting;
using Quartz;
using StackOverflowArchive.Jobs.Application.Handlers;
using StackOverflowArchive.Jobs.Scheduler.Extensions;

namespace StackOverflowArchive.Jobs.Scheduler
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplicationServices(hostContext.Configuration);
                    services.AddMediatR(typeof(CountPosts));
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionScopedJobFactory();
                        q.AddConfiguredQuartzJobTriggers(hostContext.Configuration);
                    });

                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                });
    }
}
