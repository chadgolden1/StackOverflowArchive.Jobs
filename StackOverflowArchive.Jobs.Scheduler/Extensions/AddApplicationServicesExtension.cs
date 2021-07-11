using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackOverflowArchive.Jobs.Application.Data;
using StackOverflowArchive.Jobs.Application.Handlers;
using System.Data;
using System.Data.SqlClient;

namespace StackOverflowArchive.Jobs.Scheduler.Extensions
{
    public static class AddApplicationServicesExtension
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(config.GetConnectionString("Default")));
            services.AddTransient<IDbConnection>(factory =>
            {
                var connectionString = config.GetConnectionString("Default");
                return new SqlConnection(connectionString);
            });
            services.AddTransient<CountUsers.IUserRepository, CountUsers.UserRepository>();
        }
    }
}
