using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;
using WeatherApp.DataAccess.Context;
using WeatherApp.DataAccess.Repositories;
using WeatherApp.DomainModel.Configurations;
using WeatherApp.DomainModel.Contracts;
using WeatherApp.Service;

namespace WeatherApp.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddDatabase(this IServiceCollection services, DatabaseConfiguration databaseConfig)
        {
            // Get the current directory
            string baseDirectory = AppContext.BaseDirectory;
            string solutionRoot = Path.Combine(baseDirectory, "..", "..", "..", "..");

            var dbFilePath = databaseConfig.DatabaseFile;
            string relativePathToDatabase = Path.Combine(solutionRoot, dbFilePath);

            // Get the absolute path
            string absolutePath = Path.GetFullPath(relativePathToDatabase);

            var sqliteConnectionString = $"Data Source={absolutePath};";

            services.AddDbContext<DataContext>(options =>
                        options.UseSqlite(sqliteConnectionString));

            return services;
        }

        internal static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Weather Dashboard",
                    Version = "1.0",
                    Description = "Weather Dashboard API",
                });

            });

            return services;
        }

        internal static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IWeatherAppRepository, WeatherAppRepository>();
            services.AddTransient<IWeatherApiClient, WeatherApiClient>();
            services.AddTransient<IWeatherAppService, WeatherAppService>();
            services.AddTransient<IMemoryCache, MemoryCache>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            return services;
        }
    }
}
