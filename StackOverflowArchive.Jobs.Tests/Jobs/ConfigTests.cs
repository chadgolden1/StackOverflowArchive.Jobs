using Microsoft.Extensions.Configuration;
using Quartz;
using StackOverflowArchive.Jobs.Scheduler.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StackOverflowArchive.Jobs.Tests.Jobs
{
    public class ConfigTests
    {
        private const string JobAssemblyName = "StackOverflowArchive.Jobs.Scheduler";

        [Fact]
        public void ConfiguredJobsHaveValidCronExpressions()
        {
            IEnumerable<AddConfiguredQuartzJobTriggersExtension.CronJobsConfig> configuredCronJobs = GetConfiguredCronJobs();
            Assert.True(
                configuredCronJobs.All(c => CronExpression.IsValidExpression(c.CronSchedule)),
                $"Invalid Cron expression for {string.Join(", ", configuredCronJobs.Where(c => !CronExpression.IsValidExpression(c.CronSchedule)).Select(c => c.JobKey))}"
            );
        }

        [Fact]
        public void ConfiguredJobsHaveValidTypes()
        {
            IEnumerable<AddConfiguredQuartzJobTriggersExtension.CronJobsConfig> configuredCronJobs = GetConfiguredCronJobs();
            Assert.True(
                configuredCronJobs.All(c => typeof(IJob).IsAssignableFrom(Type.GetType(c.JobClass + ", " + JobAssemblyName))),
                $"Invalid job type {string.Join(", ", configuredCronJobs.Where(c => !typeof(IJob).IsAssignableFrom(Type.GetType(c.JobClass + ", " + JobAssemblyName))).Select(c => c.JobClass))}"
            );
        }

        private static IEnumerable<AddConfiguredQuartzJobTriggersExtension.CronJobsConfig> GetConfiguredCronJobs()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            IEnumerable<AddConfiguredQuartzJobTriggersExtension.CronJobsConfig> configuredCronJobs = config
                .GetSection("CronJobs")
                .Get<IEnumerable<AddConfiguredQuartzJobTriggersExtension.CronJobsConfig>>();
            return configuredCronJobs;
        }
    }
}
