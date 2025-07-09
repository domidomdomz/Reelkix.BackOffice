using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
