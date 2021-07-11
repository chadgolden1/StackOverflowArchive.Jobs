using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using StackOverflowArchive.Jobs.Application.Handlers;
using System.Threading.Tasks;

namespace StackOverflowArchive.Jobs.Scheduler.Jobs
{
    public class CountPostsJob : IJob
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CountPostsJob(IMediator mediator, ILogger<CountPostsJob> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var response = await _mediator.Send(new CountPosts.Query());
            _logger.LogInformation("Number of posts " + response.PostCount);
        }
    }
}
