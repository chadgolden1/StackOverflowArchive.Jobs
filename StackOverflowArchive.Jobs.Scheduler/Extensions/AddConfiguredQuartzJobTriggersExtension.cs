using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;

namespace StackOverflowArchive.Jobs.Scheduler.Extensions
{
    public static class AddConfiguredQuartzJobTriggersExtension
    {
        public static void AddConfiguredQuartzJobTriggers(this IServiceCollectionQuartzConfigurator quartz, IConfiguration config)
        {
            IEnumerable<CronJobsConfig> configuredCronJobs = config.GetSection("CronJobs").Get<IEnumerable<CronJobsConfig>>();
            foreach (CronJobsConfig cronJob in configuredCronJobs)
            {
                var loggingJobKey = new JobKey(cronJob.JobKey);
                quartz.AddJob(Type.GetType(cronJob.JobClass), loggingJobKey);
                quartz.AddTrigger(options =>
                {
                    options.ForJob(loggingJobKey)
                        .WithIdentity(loggingJobKey + "Trigger")
                        .WithCronSchedule(cronJob.CronSchedule);
                });
            }
        }

        public class CronJobsConfig
        {
            public string JobKey { get; set; }
            public string JobClass { get; set; }
            public string CronSchedule { get; set; }
        }
    }
}
