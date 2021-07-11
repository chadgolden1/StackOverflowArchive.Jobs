using MediatR;
using Microsoft.EntityFrameworkCore;
using StackOverflowArchive.Jobs.Application.Data;
using System.Threading;
using System.Threading.Tasks;

namespace StackOverflowArchive.Jobs.Application.Handlers
{
    public static class CountPosts
    {
        public class Query : IRequest<Response> { }

        public class Response
        {
            public int PostCount { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken) => new Response
            {
                PostCount = await _context.Posts.CountAsync(cancellationToken)
            };
        }
    }
}
