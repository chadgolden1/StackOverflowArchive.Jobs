using Microsoft.EntityFrameworkCore;
using StackOverflowArchive.Jobs.Application.Data;
using StackOverflowArchive.Jobs.Application.Handlers;
using StackOverflowArchive.Jobs.Application.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StackOverflowArchive.Jobs.Tests.Application
{
    public class CountPostsTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly CountPosts.Handler _sut;

        public CountPostsTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(ApplicationDbContext))
                .Options;

            _context = new ApplicationDbContext(contextOptions);

            _sut = new CountPosts.Handler(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(10, 10)]

        public async Task ShouldReturnCountNWhenNPostsExistInDb(int numberOfPostsInDb, int expectedNumberOfPosts)
        {
            await AddNPostsToDb(numberOfPostsInDb);

            CountPosts.Response response = await _sut.Handle(new CountPosts.Query(), default);

            Assert.Equal(expectedNumberOfPosts, response.PostCount);
        }

        private async Task AddNPostsToDb(int numberOfPostsToAdd)
        {
            foreach (int i in Enumerable.Range(1, numberOfPostsToAdd))
            {
                _context.Posts.Add(new Post { Id = i });
            }
            await _context.SaveChangesAsync(default);
        }
    }
}
