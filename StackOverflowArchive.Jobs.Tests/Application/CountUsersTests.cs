using Moq;
using StackOverflowArchive.Jobs.Application.Handlers;
using System.Threading.Tasks;
using Xunit;

namespace StackOverflowArchive.Jobs.Tests.Application
{
    public class CountUsersTests
    {
        private readonly Mock<CountUsers.IUserRepository> _userRepositoryMock;
        private readonly CountUsers.Handler _sut;

        public CountUsersTests()
        {
            _userRepositoryMock = new Mock<CountUsers.IUserRepository>();
            _sut = new CountUsers.Handler(_userRepositoryMock.Object);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(10, 10)]

        public async Task ShouldReturnCountNWhenNPostsExistInDb(int numberOfUsersInDb, int expectedNumberOfUsers)
        {
            SetupDbToReturnNUsers(numberOfUsersInDb);

            CountUsers.Response response = await _sut.Handle(new CountUsers.Query(), default);

            Assert.Equal(expectedNumberOfUsers, response.UserCount);
        }

        private void SetupDbToReturnNUsers(int numberOfUsersInDb)
        {
            _userRepositoryMock.Setup(repo => repo.CountUsers()).ReturnsAsync(numberOfUsersInDb);
        }
    }
}
