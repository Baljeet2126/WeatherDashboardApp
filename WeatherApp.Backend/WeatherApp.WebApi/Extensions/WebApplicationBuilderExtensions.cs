using System.Text.Json.Serialization;
using WeatherApp.DomainModel.Configurations;

namespace WeatherApp.WebApi.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        internal static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy => policy.WithOrigins("http://localhost:4200") // Angular dev server
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // This converts enum values to strings in JSON responses
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger();

            var databaseConfig = builder.Configuration.GetSection(DatabaseConfiguration.SectionName).Get<DatabaseConfiguration>();
            _ = builder.Services.AddDatabase(databaseConfig);
            builder.Services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddServices();
            builder.Services.AddHttpClient();
            return builder;
        }
    }
}
