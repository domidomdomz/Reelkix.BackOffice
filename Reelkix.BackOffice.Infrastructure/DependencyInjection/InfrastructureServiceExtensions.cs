using Amazon.S3;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reelkix.BackOffice.Application.Common.Interfaces.Storage;
using Reelkix.BackOffice.Infrastructure.Storage;

namespace Reelkix.BackOffice.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
        {
            // Register infrastructure services here
            // Example: services.AddSingleton<IS3Service, S3Service>();
            // Add other necessary services like logging, configuration, etc.
            // Example: services.AddLogging();

            // Config options
            services.Configure<StorageOptions>(config.GetSection("Storage"));

            // S3 or Local stub
            if (env.IsDevelopment())
            {
                services.AddSingleton<IS3Service, LocalImageService>();
            }
            else
            {
                services.AddAWSService<IAmazonS3>();
                services.AddSingleton<IS3Service, S3Service>();
            }

            return services;
        }
    }
}
