using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DomainModel.Configurations;

namespace WeatherApp.DomainModel.Contracts
{
    public interface IConfigurationService
    {
        WeatherAppConfiguration GetWeatherAppConfiguration();
    }
}
