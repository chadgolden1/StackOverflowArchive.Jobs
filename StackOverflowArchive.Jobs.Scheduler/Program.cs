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

                        //var loggingJobKey = new JobKey("LoggingJob");
                        //q.AddJob<LoggingJob>(options => options.WithIdentity(loggingJobKey));
                        //q.AddTrigger(options =>
                        //{
                        //    options.ForJob(loggingJobKey)
                        //        .WithIdentity("LoggingJobTrigger")
                        //        .WithCronSchedule("0/10 * * * * ?"); // every 10 seconds
                        //});

                        //var countPostJobKey = new JobKey("CountPostJob");
                        //q.AddJob<CountPostsJob>(options => options.WithIdentity(countPostJobKey));
                        //q.AddTrigger(options =>
                        //{
                        //    options.ForJob(countPostJobKey)
                        //        .WithIdentity("CountPostJobTrigger")
                        //        .WithCronSchedule("0/5 * * * * ?"); // every 5 seconds
                        //});

                        //var countUsersJobKey = new JobKey("CountUserJob");
                        //q.AddJob<CountUsersJob>(options => options.WithIdentity(countUsersJobKey));
                        //q.AddTrigger(options =>
                        //{
                        //    options.ForJob(countUsersJobKey)
                        //        .WithIdentity("CountUsersJobTrigger")
                        //        .WithCronSchedule("0/5 * * * * ?"); // every 5 seconds
                        //});
                    });

                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                });
    }
}
