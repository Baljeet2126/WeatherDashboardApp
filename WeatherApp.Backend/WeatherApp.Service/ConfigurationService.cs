using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DomainModel.Configurations;
using WeatherApp.DomainModel.Contracts;

namespace WeatherApp.Service
{
    public class ConfigurationService : IConfigurationService
    {

        private readonly IConfiguration _configuration;
        public ConfigurationService(IConfiguration configuration) {

            _configuration = configuration;
        }
        public WeatherAppConfiguration GetWeatherAppConfiguration()
        {
            var configuration = _configuration.GetSection("WeatherDashboard").Get<WeatherAppConfiguration>();
            return configuration ?? new WeatherAppConfiguration();
        }
    }
}
