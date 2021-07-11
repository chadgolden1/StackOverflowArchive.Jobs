using Dapper;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace StackOverflowArchive.Jobs.Application.Handlers
{
    public static class CountUsers
    {
        public class Query : IRequest<Response> { }

        public class Response
        {
            public int UserCount { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                return new Response { UserCount = await _userRepository.CountUsers() };
            }
        }

        public interface IUserRepository
        {
            Task<int> CountUsers();
        }

        public class UserRepository : IUserRepository
        {
            private readonly IDbConnection _connection;

            public UserRepository(IDbConnection connection)
            {
                _connection = connection;
            }

            public Task<int> CountUsers() => _connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Users;");
        }
    }
}
