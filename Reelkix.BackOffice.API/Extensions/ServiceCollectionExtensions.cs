using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                                    ?? configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
